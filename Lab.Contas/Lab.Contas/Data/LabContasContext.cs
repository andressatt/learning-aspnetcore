using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab.Contas.Models;

namespace Lab.Contas.Data
{
    public class LabContasContext : DbContext
    {
        public LabContasContext (DbContextOptions<LabContasContext> options)
            : base(options)
        {
        }

        public DbSet<Lab.Contas.Models.ContaPagar> ContaPagar { get; set; } = default!;
    }
}
