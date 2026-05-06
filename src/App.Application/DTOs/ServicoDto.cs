namespace App.Application.DTOs
{
    public class ServicoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorBase { get; set; }
        public decimal PercentualImposto { get; set; }
        public bool Ativo { get; set; }
    }
}