using BarberAPI.Data;
using BarberAPI.DTO;
using BarberAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase 
    {
        public readonly ApplicationDbContext _dbContext;

        public ClienteController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]

        public async Task<IActionResult> CriarClientes([FromBody] ClienteDTO clienteDTO)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = clienteDTO.Nome;
                cliente.Telefone = clienteDTO.Telefone;
                cliente.Email = clienteDTO.Email;
                cliente.DataNascimento = clienteDTO.DataNascimento;

                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetClientesPorId),new { id = cliente.ClienteId }, cliente);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _dbContext.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClientesPorId(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            if(id != clienteDTO.ClienteId)
            {
                return BadRequest();
            }
       
            if (ModelState.IsValid)
            {
                var clienteExistente = await _dbContext.Clientes.FindAsync(clienteDTO.ClienteId);
                if (clienteExistente == null)
                {
                    return NotFound();
                }
                clienteExistente.Nome = clienteDTO.Nome;
                clienteExistente.Telefone = clienteDTO.Telefone;
                clienteExistente.Email = clienteDTO.Email;
                clienteExistente.DataNascimento = clienteDTO.DataNascimento;
                _dbContext.Clientes.Update(clienteExistente);
                await _dbContext.SaveChangesAsync();
                return Ok(clienteExistente);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarCliente(int id) 
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
