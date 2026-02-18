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

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _dbContext.Clientes.ToListAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientesPorId(int id)
        {
            return Ok();
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
                return CreatedAtAction(nameof(GetClientesPorId), new { id = cliente.ClienteId }, cliente);  
            }
            return BadRequest();
        }
    }
}
