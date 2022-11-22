using System.Data.Entity;

namespace Lab.MVC.Models
{
    public class LojaContext : DbContext
    {
        public LojaContext(string conexao) : base(conexao)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}