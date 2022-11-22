using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.MVC.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }

        [MaxLength(-1)]
        public byte[] Foto { get; set; }

        [NotMapped]
        public string NumeroRevisaoProg { get; set; }
    }
}