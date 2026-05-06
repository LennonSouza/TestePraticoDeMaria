using System.Collections.Generic;

namespace App.Application.DTOs
{
    public class RelatorioGrupoDto
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public List<RelatorioItemDto> Itens { get; set; } = new List<RelatorioItemDto>();
        public decimal TotalCliente { get; set; }
        public decimal TotalImpostos { get; set; }
        public int QuantidadeOs { get; set; }
    }
}