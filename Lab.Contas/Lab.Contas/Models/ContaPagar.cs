using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Lab.Contas.Models.Util;

namespace Lab.Contas.Models
{
    public class ContaPagar
    {
        public int Id { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public TipoContaPagar Tipo { get; set; } = default!;

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Currency, ErrorMessage = "O valor deve ser numérico.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Vencimento { get; set; }

        [DisplayName("Observações")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public StatusContaPagar Status { get; set; }
    }
}
