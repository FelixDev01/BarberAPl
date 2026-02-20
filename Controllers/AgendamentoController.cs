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
    public class AgendamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AgendamentoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAgendamento (AgendamentoDTO agendamentoDTO)
        {
          
            Agendamento agendamento = new Agendamento();
            agendamento.ClienteID = agendamentoDTO.Cliente.ClienteId;
            agendamento.Observacoes = agendamentoDTO.Observacoes;
            agendamento.DataHora = agendamentoDTO.DataHora;
            agendamento.Status = agendamentoDTO.Status;  
            agendamento.Servicos = new List<Servico>();
            agendamento.Cliente = _dbContext.Clientes.Find(agendamento.ClienteID);      
            
            foreach(var item in agendamentoDTO.Servicos)
                {
                    var servico = _dbContext.Servicos.Find(item.Id);
                    if (servico == null)
                    {
                        return BadRequest();
                    }

                    agendamento.Servicos.Add(servico);
            }
            _dbContext.Agendamentos.Add(agendamento);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAgendamentosPorId), new { id = agendamento.AgendamentoID }, agendamento);

        }

        [HttpGet]
        public async Task<IActionResult> GetAgendamentos()
        {
            var agendamentos = await _dbContext.Agendamentos
                                                .Include( c => c.Cliente )
                                                .Include( s => s.Servicos )
                                                .ToListAsync();
            return Ok(agendamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAgendamentosPorId(int id)
        {
            var agendamento = await _dbContext.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return Ok(agendamento);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarAgendamento(int id, AgendamentoDTO agendamentoDTO)
        {
            var agendamento = await _dbContext.Agendamentos
                                        .Include(c => c.Cliente)
                                        .Include(s => s.Servicos)       
                                        .FirstOrDefaultAsync(a => a.AgendamentoID == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.ClienteID = agendamentoDTO.Cliente.ClienteId;
            agendamento.Observacoes = agendamentoDTO.Observacoes;
            agendamento.DataHora = agendamentoDTO.DataHora;
            agendamento.Status = agendamentoDTO.Status;

            agendamento.Servicos.Clear();


            foreach (var item in agendamentoDTO.Servicos)
            {
                var servico = _dbContext.Servicos.Find(item.Id);
                if (servico == null)
                {
                    return BadRequest();
                }

                agendamento.Servicos.Add(servico);
            }
            await _dbContext.SaveChangesAsync();
            return Ok(agendamento);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarAgendamento(int id)
        {
            var agendamento = await _dbContext.Agendamentos
                .Include(c => c.Cliente)
                .Include(s => s.Servicos)
                .FirstOrDefaultAsync(a => a.AgendamentoID == id);

            if (agendamento == null)
            {
                return NotFound();
            }
            _dbContext.Agendamentos.Remove(agendamento);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
