using BarberAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberAPI.DTO
{
    public class AgendamentoDTO
    {
        public int AgendamentoID { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        public string Observacoes { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        public virtual ICollection<Servico> Servicos { get; set; }
    }
}
