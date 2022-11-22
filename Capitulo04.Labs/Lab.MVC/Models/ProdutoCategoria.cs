using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab.MVC.Models
{
    public class ProdutoCategoria
    {
        [Key]
        [Column(Order = 1)]
        public int ProdutoId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CategoriaId { get; set; }
    }
}