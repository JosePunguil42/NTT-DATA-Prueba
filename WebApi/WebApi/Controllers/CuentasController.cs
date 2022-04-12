using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DC;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controlador con acciones para cuentas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ApiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Contexto DB</param>
        public CuentasController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todas las cuentas
        /// </summary>
        /// <returns>Objeto repuesta</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> GetCuentas()
        {
            Respuesta respuesta = new();
            try
            {
                respuesta.Objeto = await _context.Cuentas.ToListAsync();
                respuesta.Ejecutado = true;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Obtener cuenta por id
        /// </summary>
        /// <param name="id">identificador cuenta</param>
        /// <returns>Respuesta</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Respuesta>> GetCuenta(string id)
        {
            Respuesta respuesta = new();
            try
            {
                var cuenta = await _context.Cuentas.FindAsync(id);
                respuesta.Objeto = cuenta;
                respuesta.Ejecutado = true;
                if (cuenta == null)
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Editar cuenta
        /// </summary>
        /// <param name="id">identificador</param>
        /// <param name="cuenta">objeto cuenta</param>
        /// <returns>Respuesta</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(string id, Cuenta cuenta)
        {
            if (id != cuenta.CuNumeroCuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Crear cuenta
        /// </summary>
        /// <param name="cuenta">objeto cuenta</param>
        /// <returns>respuesta</returns>
        [HttpPost]
        public async Task<Respuesta> PostCuenta(Cuenta cuenta)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                if (ExisteCuenta(cuenta.CuNumeroCuenta))
                {
                    respuesta.Mensaje = "La cuenta ya existe";
                }
                else
                {
                    _context.Cuentas.Add(cuenta);
                    await _context.SaveChangesAsync();
                    respuesta.Ejecutado = true;
                }
            }
            catch (DbUpdateException e)
            {
                respuesta.Mensaje = "Error: " + e.StackTrace;
            }
            respuesta.Objeto = CreatedAtAction("GetPCuenta", new { id = cuenta.CuNumeroCuenta }, cuenta);
            return respuesta;
        }

        /// <summary>
        /// Validar si existe la cuenta
        /// </summary>
        /// <param name="id">identificador</param>
        /// <returns></returns>
        private bool ExisteCuenta(string id)
        {
            return _context.Cuentas.Any(e => e.CuNumeroCuenta == id);
        }

        /// <summary>
        /// Eliminar cuenta por id
        /// </summary>
        /// <param name="id">identificador</param>
        /// <returns>Respuesta</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(string id)
        {
            try
            {
                var cuenta = await _context.Cuentas.FindAsync(id);
                if (cuenta == null)
                {
                    return NotFound();
                }
                _context.Cuentas.Remove(cuenta);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Validar cuenta
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Respuesta</returns>
        private bool CuentaExists(string id)
        {
            return _context.Cuentas.Any(e => e.CuNumeroCuenta == id);
        }
    }
}
