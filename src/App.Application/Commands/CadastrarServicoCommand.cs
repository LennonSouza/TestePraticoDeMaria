namespace App.Application.Commands
{
    public class CadastrarServicoCommand
    {
        public string Nome { get; set; }
        public decimal ValorBase { get; set; }
        public decimal PercentualImposto { get; set; }
    }
}