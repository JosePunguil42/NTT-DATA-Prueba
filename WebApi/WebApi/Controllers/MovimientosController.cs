using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DC;
using WebApi.Logica;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly ApiContext _context;

        public MovimientosController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todos los movimientos
        /// </summary>
        /// <returns>Respuesta</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> GetMovimientos()
        {
            Respuesta respuesta = new();
            try
            {
                respuesta.Objeto = await _context.Movimientos.ToListAsync();
                respuesta.Ejecutado = true;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Obtener movimiento por id
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Respuesta</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Respuesta>> GetMovimiento(int id)
        {
            Respuesta respuesta = new();
            try
            {
                var movimiento = await _context.Movimientos.FindAsync(id);
                respuesta.Objeto = movimiento;
                respuesta.Ejecutado = true;
                if (movimiento == null)
                {
                    respuesta.Mensaje = "No existe el movimiento";
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Editar movimiento
        /// </summary>
        /// <param name="id">identificador</param>
        /// <param name="movimiento">Movimiento</param>
        /// <returns>Respuesta</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.MoIdMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Insertar movimiento
        /// </summary>
        /// <param name="movimiento">Movimiento</param>
        /// <returns>Respuesta</returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostMovimiento(Movimiento movimiento)
        {
            Respuesta respuesta = new();
            try
            {
                BIMovimientos bIMovimientos = new(_context);
                movimiento.MoFecha = DateTime.Now;
                List<Movimiento> lstUtlimoMovimiento = await _context.Movimientos.Where(x => x.MoNumeroCuenta == movimiento.MoNumeroCuenta).OrderByDescending(x => x.MoFecha).ToListAsync();
                Movimiento ultimoMovimiento = lstUtlimoMovimiento.FirstOrDefault();
                ultimoMovimiento = bIMovimientos.AsignarDatosInicialesMovimientos(ultimoMovimiento, movimiento.MoSaldoInicial);
                respuesta = await bIMovimientos.AsignarValoresDebitoCredito(ultimoMovimiento.MoSaldoDisponible, movimiento);
                respuesta.Objeto = CreatedAtAction("GetPMovimiento", new { id = movimiento.MoIdMovimiento }, movimiento);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Eliminar movimiento
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Respuesta</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            try
            {
                var movimiento = await _context.Movimientos.FindAsync(id);
                if (movimiento == null)
                {
                    return NotFound();
                }

                _context.Movimientos.Remove(movimiento);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Validar si movimiento existe
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Respuesta</returns>
        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MoIdMovimiento == id);
        }

        /// <summary>
        /// Cupo Diarios
        /// </summary>
        /// <param name="numeroCuenta">cuenta</param>
        /// <param name="monto">monto</param>
        /// <returns>respuesta</returns>
        [HttpPost("{numeroCuenta}/{monto}")]
        public async Task<Respuesta> CupoDiario(string numeroCuenta, decimal monto)
        {
            BIMovimientos bIMovimientos = new(_context);
            return await bIMovimientos.ValidarCupoDiario(numeroCuenta, monto);
        }

        /// <summary>
        /// Reporte de movimientos por Identificacion y fecha de inicio, fin
        /// </summary>
        /// <param name="identificacion">indentificador</param>
        /// <param name="fechaInicio">Fecha inicio</param>
        /// <param name="fechaFin">Fecha fin</param>
        /// <returns>respuesta</returns>
        [HttpGet("{identificacion}/{fechaInicio}/{fechaFin}")]
        public async Task<Respuesta> Movimientos(string identificacion, DateTime fechaInicio, DateTime fechaFin)
        {
            List<Movimiento> lstPMovimiento = new();
            Respuesta respuesta = new();
            try
            {
                respuesta.Objeto = await _context.Movimientos.Select(x => new ClienteMovimientos
                {
                    Fecha = x.MoFecha,
                    Nombre = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.ClNombre,
                    NumeroCuenta = x.MoNumeroCuentaNavigation.CuNumeroCuenta,
                    Tipo = x.MoNumeroCuentaNavigation.CuTipo,
                    SaldoInicial = x.MoSaldoInicial,
                    Estado = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.ClEstado,
                    Movimiento = x.MoMovimiento,
                    SaldoDisponible = x.MoSaldoDisponible,
                    Identificacion = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.ClIdentificacion,

                }).Where(s => s.Identificacion == identificacion && s.Fecha >= fechaInicio && s.Fecha <= fechaFin).ToListAsync();
                respuesta.Ejecutado = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = "Error: " + e.StackTrace;
            }
            return respuesta;
        }
    }
}
