using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Application.DTOs
{
    public class RelatorioDto
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string FiltroStatus { get; set; }
        public string FiltroCliente { get; set; }
        public List<RelatorioGrupoDto> Grupos { get; set; } = new List<RelatorioGrupoDto>();

        public decimal TotalGeral => Grupos.Sum(g => g.TotalCliente);
        public decimal TotalImpostos => Grupos.Sum(g => g.TotalImpostos);
        public int QuantidadeTotalOs => Grupos.Sum(g => g.QuantidadeOs);
    }
}