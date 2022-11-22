using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab.MVC.Models
{
    public class Categoria
    {
        public Categoria()
        {
            this.Produtos = new List<Produto>();
        }

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        public string Descricao { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}