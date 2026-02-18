using System;
using System.ComponentModel.DataAnnotations;

namespace BarberAPI.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Favor digitar um email válido")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
    }

}
