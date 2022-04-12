using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DC;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ClientesController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todos los clientes
        /// </summary>
        /// <returns>respuesta</returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta>> GetClientes()
        {
            Respuesta respuesta = new();
            try
            {
                respuesta.Objeto = await _context.Clientes.ToListAsync();
                respuesta.Ejecutado = true;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Obtener cliente por id
        /// </summary>
        /// <param name="id">identificador</param>
        /// <returns>Respuesta</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Respuesta>> GetCliente(int id)
        {
            Respuesta respuesta = new();
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                respuesta.Objeto = cliente;
                respuesta.Ejecutado = true;
                if (cliente == null)
                {
                    respuesta.Mensaje = "No existe Cliente";
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        /// <summary>
        /// Editar cliente
        /// </summary>
        /// <param name="id">identificador</param>
        /// <param name="cliente">CLiente</param>
        /// <returns>Respuesta</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClIdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Crear cliente
        /// </summary>
        /// <param name="cliente">Cliente</param>
        /// <returns>Respuesta</returns>
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return CreatedAtAction("GetCliente", new { id = cliente.ClIdCliente }, cliente);
        }

        /// <summary>
        /// Eliminar cliente
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Respuesta</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Validar si cliente existe
        /// </summary>
        /// <param name="id">identificador</param>
        /// <returns>Respuesta</returns>
        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClIdCliente == id);
        }
    }
}
