using AgendaMedica.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaMedica.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options): base(options)
        { }

        public DbSet<Agendamento> Agendamentos { get; set; } = null!;
    }
}
