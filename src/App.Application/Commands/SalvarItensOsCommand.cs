using System.Collections.Generic;

namespace App.Application.Commands
{
    public class SalvarItensOsCommand
    {
        public int OsId { get; set; }
        public int Versao { get; set; }
        public string Observacao { get; set; }
        public List<ItemOsCommand> Itens { get; set; } = new List<ItemOsCommand>();
    }

    public class ItemOsCommand
    {
        public int ServicoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PercentualImposto { get; set; }
    }
}