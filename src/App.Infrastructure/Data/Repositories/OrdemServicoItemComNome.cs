using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Infrastructure.Data.Repositories
{
    public class OrdemServicoItemComNome
    {
        public OrdemServicoItem Item { get; set; }
        public string ServicoNome { get; set; }

        public List<OrdemServicoItemComNome> ItensComNome { get; set; }
    }
}
