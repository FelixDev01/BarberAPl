using System;
using System.ComponentModel.DataAnnotations;

namespace BarberAPI.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoID { get; set; }

        [Required]
        public DateTime DataHora { get; set; }
        
        public string Observacoes { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public int ClienteID { get; set; }

        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }

        public virtual ICollection<Servico> Servicos { get; set; }
    }

}
