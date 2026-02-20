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
    public class ServicoController : ControllerBase
    {

        public readonly ApplicationDbContext _dbContext;

        public ServicoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CriarServico(ServicoDTO servicoDTO)
        {
            Servico servico = new Servico();
            servico.NomeServico = servicoDTO.NomeServico;
            servico.Descricao = servicoDTO.Descricao;
            servico.DuracaoMin = servicoDTO.DuracaoMin;
            servico.Preco = servicoDTO.Preco;
            
            _dbContext.Add(servico);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServicoPorId), new { id = servico.Id }, servico);
        }

        [HttpGet]
        public async Task<IActionResult> GetServicos()
        {
            var servicos = await _dbContext.Servicos.ToListAsync();
            return Ok(servicos);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetServicoPorId(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            return Ok(servico);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarServico(int id, ServicoDTO servicoDTO)
        {
            if (id != servicoDTO.Id)
            {
                return BadRequest();
            }
    
            var servicoExistente = await _dbContext.Servicos.FindAsync(id);

            if (servicoExistente == null)
            {
                return NotFound();
            }

            servicoExistente.NomeServico = servicoDTO.NomeServico;
            servicoExistente.Descricao = servicoDTO.Descricao;
            servicoExistente.DuracaoMin = servicoDTO.DuracaoMin;
            servicoExistente.Preco = servicoDTO.Preco;
            await _dbContext.SaveChangesAsync();
            return Ok(servicoExistente);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarServico(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            _dbContext.Servicos.Remove(servico);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }

}
