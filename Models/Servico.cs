using System;

namespace BarberAPI.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public string NomeServico { get; set; }

        public string Descricao { get; set; }

        public int DuracaoMin { get; set; }

        public decimal Preco { get; set; }
    }

}
