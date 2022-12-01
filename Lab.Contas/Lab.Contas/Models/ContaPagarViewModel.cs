namespace Lab.Contas.Models
{
    public class ContaPagarViewModel
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public IList<ContaPagar> ListaContasPagarMes { get; set; } = new List<ContaPagar>();
        public decimal TotalPagar { get; set; }
        public decimal TotalPago { get; set; }
        public decimal TotalNaoPago { get; set; }
        public decimal DiferencaMes { get; set; }
        public string ContasPagarParaGrafico { get; set; } = default!;
    }
}
