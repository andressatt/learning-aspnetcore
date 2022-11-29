using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
