namespace App.Application.Commands
{
    public class AtualizarServicoCommand
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorBase { get; set; }
        public decimal PercentualImposto { get; set; }
        public bool Ativo { get; set; }
    }
}