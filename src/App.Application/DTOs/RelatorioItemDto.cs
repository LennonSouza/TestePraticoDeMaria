using System;

namespace App.Application.DTOs
{
    public class RelatorioItemDto
    {
        public int OsId { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalImpostos { get; set; }
    }
}