using App.Application.DTOs;
using App.Domain.Enums;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;
using App.Infrastructure.Logging;
using System;
using System.Linq;

namespace App.Application.Services
{
    public class RelatorioService
    {
        private readonly ConnectionFactory _factory;
        private readonly FileLogger _logger;

        public RelatorioService(ConnectionFactory factory, FileLogger logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public RelatorioDto GerarRelatorio(
            DateTime? dataInicio,
            DateTime? dataFim,
            int? clienteId,
            int? status)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    var repo = new RelatorioRepository(uow);
                    var linhas = repo.Consultar(dataInicio, dataFim, clienteId, status);

                    var grupos = linhas
                        .GroupBy(l => new { l.ClienteId, l.ClienteNome })
                        .Select(g => new RelatorioGrupoDto
                        {
                            ClienteId = g.Key.ClienteId,
                            ClienteNome = g.Key.ClienteNome,
                            QuantidadeOs = g.Count(),
                            TotalCliente = g.Sum(l => l.ValorTotal),
                            TotalImpostos = g.Sum(l => l.TotalImpostos),
                            Itens = g.Select(l => new RelatorioItemDto
                            {
                                OsId = l.OsId,
                                DataAbertura = l.DataAbertura,
                                DataConclusao = l.DataConclusao,
                                Status = FormatarStatus(l.Status),
                                ValorTotal = l.ValorTotal,
                                TotalImpostos = l.TotalImpostos
                            }).ToList()
                        }).ToList();

                    return new RelatorioDto
                    {
                        DataInicio = dataInicio,
                        DataFim = dataFim,
                        Grupos = grupos
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro ao gerar relatório.", ex);
                throw;
            }
        }

        private static string FormatarStatus(StatusOrdemServico status)
        {
            switch (status)
            {
                case StatusOrdemServico.Aberta: return "Aberta";
                case StatusOrdemServico.EmAndamento: return "Em andamento";
                case StatusOrdemServico.Concluida: return "Concluída";
                case StatusOrdemServico.Cancelada: return "Cancelada";
                default: return status.ToString();
            }
        }
    }
}