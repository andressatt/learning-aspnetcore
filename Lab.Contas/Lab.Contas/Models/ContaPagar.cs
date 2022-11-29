using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Lab.Contas.Models.Util;

namespace Lab.Contas.Models
{
    public class ContaPagar
    {
        public int Id { get; set; }

        public TipoConta Tipo { get; set; }

        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [DataType(DataType.Date)]
        public DateTime Vencimento { get; set; }

        [DisplayName("Observações")]
        public string? Observacoes { get; set; }
        
        public StatusContaPagar Status { get; set; }
    }
}
