using App.Application.Commands;
using App.Application.DTOs;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;
using App.Infrastructure.Exceptions;
using App.Infrastructure.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Application.Services
{
    public class OrdemServicoService
    {
        private readonly ConnectionFactory _factory;
        private readonly FileLogger _logger;
        private readonly string _usuarioAtual;

        public OrdemServicoService(ConnectionFactory factory, FileLogger logger, string usuarioAtual)
        {
            _factory = factory;
            _logger = logger;
            _usuarioAtual = usuarioAtual;
        }

        public void Abrir(int clienteId, string observacao)
        {
            try
            {
                var os = new OrdemServico(clienteId, observacao);

                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new OrdemServicoRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        repo.Inserir(os);

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "OrdemServico",
                            idRegistro: os.Id,
                            operacao: "INSERT",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(os)
                        ));

                        uow.Commit();
                        _logger.Info($"OS aberta para clienteId={clienteId} por {_usuarioAtual}.");
                    }
                    catch
                    {
                        uow.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro ao abrir OS.", ex);
                throw;
            }
        }

        public void SalvarItens(SalvarItensOsCommand cmd)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new OrdemServicoRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        var os = repo.ObterPorId(cmd.OsId);
                        if (os == null)
                            throw new InvalidOperationException("OS não encontrada.");

                        if (os.Os.Versao != cmd.Versao)
                            throw new ConcorrenciaException("Esta OS foi alterada por outro usuário. Recarregue e tente novamente.");

                        repo.RemoverItens(cmd.OsId);

                        // Limpa itens em memória e readiciona via domínio
                        os.Os.CarregarItens(Enumerable.Empty<OrdemServicoItem>());

                        foreach (var itemCmd in cmd.Itens)
                        {
                            var item = new OrdemServicoItem(
                                itemCmd.ServicoId,
                                itemCmd.Quantidade,
                                itemCmd.ValorUnitario,
                                itemCmd.PercentualImposto
                            );
                            os.Os.AdicionarItem(item);
                        }

                        foreach (var item in os.Os.Itens)
                            repo.InserirItem(item, cmd.OsId);

                        repo.Atualizar(os.Os);

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "OrdemServico",
                            idRegistro: os.Os.Id,
                            operacao: "UPDATE",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(os)
                        ));

                        uow.Commit();
                        _logger.Info($"Itens da OS id={cmd.OsId} salvos por {_usuarioAtual}.");
                    }
                    catch
                    {
                        uow.Rollback();
                        throw;
                    }
                }
            }
            catch (App.Infrastructure.Exceptions.ConcorrenciaException ex)
            {
                _logger.Erro($"Conflito de concorrência na OS id={cmd.OsId}.", ex);
                throw new App.Application.Exceptions.ConcorrenciaException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Erro($"Erro de negócio ao salvar itens da OS id={cmd.OsId}.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro inesperado ao salvar itens da OS id={cmd.OsId}.", ex);
                throw;
            }
        }

        public void MudarStatus(MudarStatusOsCommand cmd)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new OrdemServicoRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        var os = repo.ObterPorId(cmd.OsId);
                        if (os == null)
                            throw new InvalidOperationException("OS não encontrada.");

                        if (os.Os.Versao != cmd.Versao)
                            throw new ConcorrenciaException("Esta OS foi alterada por outro usuário. Recarregue e tente novamente.");

                        var statusAnterior = os.Os.Status;
                        var novoStatus = (StatusOrdemServico)cmd.NovoStatus;

                        switch (novoStatus)
                        {
                            case StatusOrdemServico.EmAndamento:
                                os.Os.IniciarAndamento();
                                break;
                            case StatusOrdemServico.Concluida:
                                os.Os.Concluir();
                                break;
                            case StatusOrdemServico.Cancelada:
                                os.Os.Cancelar();
                                break;
                            default:
                                throw new InvalidOperationException("Transição de status inválida.");
                        }

                        repo.Atualizar(os.Os);

                        repo.InserirHistoricoStatus(new HistoricoStatus(
                            ordemServicoId: cmd.OsId,
                            statusAnterior: statusAnterior,
                            statusNovo: novoStatus,
                            usuario: _usuarioAtual,
                            observacao: cmd.Observacao
                        ));

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "OrdemServico",
                            idRegistro: os.Os.Id,
                            operacao: "UPDATE",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(os)
                        ));

                        uow.Commit();
                        _logger.Info($"OS id={cmd.OsId} mudou para {novoStatus} por {_usuarioAtual}.");
                    }
                    catch
                    {
                        uow.Rollback();
                        throw;
                    }
                }
            }
            catch (ConcorrenciaException ex)
            {
                _logger.Erro($"Conflito de concorrência ao mudar status da OS id={cmd.OsId}.", ex);
                throw new App.Application.Exceptions.ConcorrenciaException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Erro($"Erro de negócio ao mudar status da OS id={cmd.OsId}.", ex);
                throw new App.Application.Exceptions.ConcorrenciaException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro inesperado ao mudar status da OS id={cmd.OsId}.", ex);
                throw;
            }
        }

        public IEnumerable<OrdemServicoDto> Listar(DateTime? dataInicio, DateTime? dataFim, int? clienteId, int? status, int pagina = 1, int tamanhoPagina = 20)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    StatusOrdemServico? statusEnum = status.HasValue
                        ? (StatusOrdemServico?)status.Value : null;

                    return new OrdemServicoRepository(uow)
                        .Listar(dataInicio, dataFim, clienteId, statusEnum, pagina, tamanhoPagina)
                        .Select(x => MapearSemItens(x.Os, x.ClienteNome))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro ao listar ordens de serviço.", ex);
                throw;
            }
        }

        public OrdemServicoDto ObterPorId(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    var repo = new OrdemServicoRepository(uow);

                    var resultado = repo.ObterPorId(id);
                    if (resultado == null) return null;

                    var itensComNome = repo.ObterItensComNome(id).ToList();

                    var dto = MapearSemItens(resultado.Os, resultado.ClienteNome);

                    dto.Itens = itensComNome.Select(x => new OrdemServicoItemDto
                    {
                        Id = x.Item.Id,
                        ServicoId = x.Item.ServicoId,
                        ServicoNome = x.ServicoNome,
                        Quantidade = x.Item.Quantidade,
                        ValorUnitario = x.Item.ValorUnitario,
                        PercentualImpostoAplicado = x.Item.PercentualImpostoAplicado,
                        ValorTotalItem = x.Item.ValorTotalItem
                    }).ToList();

                    return dto;
                }
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro ao obter OS id={id}.", ex);
                throw;
            }
        }

        // Listagem — sem itens (carregados apenas ao abrir a OS)
        private static OrdemServicoDto MapearSemItens(OrdemServico os, string clienteNome) => new OrdemServicoDto
        {
            Id = os.Id,
            ClienteId = os.ClienteId,
            ClienteNome = clienteNome,
            DataAbertura = os.DataAbertura,
            DataConclusao = os.DataConclusao,
            Status = os.Status.ToString(),
            Observacao = os.Observacao,
            ValorTotal = os.ValorTotal,
            Versao = os.Versao
        };
    }
}