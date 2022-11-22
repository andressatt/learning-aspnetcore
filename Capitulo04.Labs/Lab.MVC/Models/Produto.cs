using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.MVC.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Column(name: "CodigoDoProduto")]
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int? Estoque { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal PrecoPromocao { get; protected set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }


        public int IncrementarEstoque(int quantidade)
        {
            this.Estoque += quantidade;
            return this.Estoque ?? 0;
        }
    }
}