using System;
using System.ComponentModel.DataAnnotations;

namespace BarberAPI.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [Display(Name = "Nome do Serviço")]
        public string NomeServico { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Range(10, 999, ErrorMessage = "A duração mínima deve ser de 10 minutos até 999 minutos.")]
        public int DuracaoMin { get; set; }

        [Range(0.01, 999.00, ErrorMessage = "O preço deve ficar entre 0.01 até 999.00.")]
        public decimal Preco { get; set; }
    }

}
