namespace App.Application.DTOs
{
    public class OrdemServicoItemDto
    {
        public int Id { get; set; }
        public int ServicoId { get; set; }
        public string ServicoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PercentualImpostoAplicado { get; set; }
        public decimal ValorTotalItem { get; set; }
    }
}