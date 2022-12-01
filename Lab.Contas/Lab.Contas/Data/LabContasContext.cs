using Microsoft.EntityFrameworkCore;
using Lab.Contas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab.Contas.Data
{
    public class LabContasContext : IdentityDbContext
    {
        public LabContasContext (DbContextOptions<LabContasContext> options)
            : base(options)
        {
        }

        public DbSet<TipoContaPagar> TipoContaPagar { get; set; } = default!;
        public DbSet<ContaPagar> ContaPagar { get; set; } = default!;
    }
}
