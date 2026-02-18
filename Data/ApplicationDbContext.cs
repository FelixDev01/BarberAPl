using BarberAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
         
    }
}
