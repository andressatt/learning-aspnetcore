using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab.Contas.Models
{
    public class TipoContaPagar
    {
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }
    }
}
