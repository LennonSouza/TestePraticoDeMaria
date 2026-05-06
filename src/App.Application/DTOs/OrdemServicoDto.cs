using System;
using System.Collections.Generic;

namespace App.Application.DTOs
{
    public class OrdemServicoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public decimal ValorTotal { get; set; }
        public int Versao { get; set; }
        public List<OrdemServicoItemDto> Itens { get; set; } = new List<OrdemServicoItemDto>();
    }
}