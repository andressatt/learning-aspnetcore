using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Data
{
    public class WebAppCoreMVCContext : DbContext
    {
        public WebAppCoreMVCContext (DbContextOptions<WebAppCoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppCoreMVC.Models.Movie> Movie { get; set; } = default!;
    }
}
