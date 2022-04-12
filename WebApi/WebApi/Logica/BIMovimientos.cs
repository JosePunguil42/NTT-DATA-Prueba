using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DC;
using WebApi.Models;

namespace WebApi.Logica
{
    public class BIMovimientos
    {
        private const decimal MontoMaximo = 1000;
        private const string TipoTransaccionCredito = "Credito";
        private const string TipoTransaccionDebito = "Debito";
        private readonly ApiContext _context;

        public BIMovimientos()
        {
        }

        public BIMovimientos(ApiContext context)
        {
            _context = context;
        }

        public Movimiento AsignarDatosInicialesMovimientos(Movimiento ultimoMovimiento, decimal MoSaldoInicial)
        {
            if (ultimoMovimiento == null) //No tiene movimientos ingresados
            {
                ultimoMovimiento = new();
                ultimoMovimiento.MoSaldoInicial = MoSaldoInicial;
                ultimoMovimiento.MoSaldoDisponible = MoSaldoInicial;
            }
            return ultimoMovimiento;
        }

        public async Task<decimal> ObtenerSaldoDisponible(decimal MoSaldoDisponible, decimal MoMovimiento)
        {
            return await Task.FromResult(MoSaldoDisponible + MoMovimiento);
        }

        public async Task<Respuesta> AsignarValoresDebitoCredito(decimal saldoDisponible, Movimiento movimiento)
        {
            Respuesta respuesta = new();
            //Si valor es mayor a cero es crédito
            if (movimiento.MoMovimiento > 0)
            {
                movimiento.MoSaldoInicial = saldoDisponible;
                movimiento.MoSaldoDisponible = await ObtenerSaldoDisponible(saldoDisponible, movimiento.MoMovimiento);
                movimiento.MoTipoMovimiento = TipoTransaccionCredito;
                respuesta.Ejecutado = true;
            }
            else // caso contrario en débito
            {
                if (saldoDisponible <= 0)
                {
                    respuesta.Mensaje = "Saldo no disponible";
                }
                else
                {
                    respuesta = await ValidarCupoDiario(movimiento.MoNumeroCuenta, movimiento.MoMovimiento);
                    if (respuesta.Ejecutado)
                    {
                        movimiento.MoSaldoInicial = saldoDisponible;
                        movimiento.MoSaldoDisponible = await ObtenerSaldoDisponible(saldoDisponible, movimiento.MoMovimiento);
                        movimiento.MoTipoMovimiento = TipoTransaccionDebito;
                        respuesta.Ejecutado = true;
                    }
                }

            }
            if (respuesta.Ejecutado)
            {
                _context.Movimientos.Add(movimiento);
                await _context.SaveChangesAsync();
            }
            return respuesta;
        }

        public async Task<Respuesta> ValidarCupoDiario(string numeroCuenta, decimal monto)
        {
            Respuesta respuesta = new();
            DateTime InicioDeDia = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null);
            DateTime FinalDeDia = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy") + " 23:59:59", "dd-MM-yyyy HH:mm:ss", null);
            List<Movimiento> lstMovimientos = await _context.Movimientos.Where(x => x.MoNumeroCuenta == numeroCuenta && x.MoFecha >= InicioDeDia && x.MoFecha <= FinalDeDia).ToListAsync();
            decimal total = 0;
            foreach (Movimiento oPMovimiento in lstMovimientos)
            {
                if (string.Equals(oPMovimiento.MoTipoMovimiento, TipoTransaccionDebito))
                {
                    total += oPMovimiento.MoMovimiento;
                }
            }
            decimal cupoDiario = total + monto;
            respuesta.Objeto = total;
            respuesta.Ejecutado = true;
            if (await CupoDebitoExecedido(Math.Abs(cupoDiario), MontoMaximo))
            {
                respuesta.Mensaje = "Cupo diario excedido";
                respuesta.Ejecutado = false;
            }
            return respuesta;
        }

        public async Task<bool> CupoDebitoExecedido(decimal total, decimal monto)
        {
            decimal cupoDiario = total + monto;
            if (Math.Abs(cupoDiario) > MontoMaximo)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
