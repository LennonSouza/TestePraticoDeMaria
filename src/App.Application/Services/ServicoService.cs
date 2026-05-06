using App.Application.Commands;
using App.Application.DTOs;
using App.Domain.Entities;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;
using App.Infrastructure.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Application.Services
{
    public class ServicoService
    {
        private readonly ConnectionFactory _factory;
        private readonly FileLogger _logger;
        private readonly string _usuarioAtual;

        public ServicoService(ConnectionFactory factory, FileLogger logger, string usuarioAtual)
        {
            _factory = factory;
            _logger = logger;
            _usuarioAtual = usuarioAtual;
        }

        public void Cadastrar(CadastrarServicoCommand cmd)
        {
            try
            {
                var servico = new Servico(cmd.Nome, cmd.ValorBase, cmd.PercentualImposto);

                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new ServicoRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        repo.Inserir(servico);

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "Servico",
                            idRegistro: servico.Id,
                            operacao: "INSERT",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(servico)
                        ));

                        uow.Commit();
                        _logger.Info($"Serviço '{cmd.Nome}' cadastrado por {_usuarioAtual}.");
                    }
                    catch
                    {
                        uow.Rollback();
                        throw;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                _logger.Erro("Erro de validação ao cadastrar serviço.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro inesperado ao cadastrar serviço.", ex);
                throw;
            }
        }

        public void Atualizar(AtualizarServicoCommand cmd)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new ServicoRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        var servico = repo.ObterPorId(cmd.Id);
                        if (servico == null)
                            throw new InvalidOperationException("Serviço não encontrado.");

                        servico.Atualizar(cmd.Nome, cmd.ValorBase, cmd.PercentualImposto, cmd.Ativo);
                        repo.Atualizar(servico);

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "Servico",
                            idRegistro: servico.Id,
                            operacao: "UPDATE",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(servico)
                        ));

                        uow.Commit();
                        _logger.Info($"Serviço id={cmd.Id} atualizado por {_usuarioAtual}.");
                    }
                    catch
                    {
                        uow.Rollback();
                        throw;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.Erro("Erro de negócio ao atualizar serviço.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro inesperado ao atualizar serviço.", ex);
                throw;
            }
        }

        public IEnumerable<ServicoDto> Listar(bool? ativo, int pagina = 1, int tamanhoPagina = 20)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    return new ServicoRepository(uow)
                        .Listar(ativo, pagina, tamanhoPagina)
                        .Select(Mapear);
                }
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro ao listar serviços.", ex);
                throw;
            }
        }

        public ServicoDto ObterPorId(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    var servico = new ServicoRepository(uow).ObterPorId(id);
                    return servico == null ? null : Mapear(servico);
                }
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro ao obter serviço id={id}.", ex);
                throw;
            }
        }

        private static ServicoDto Mapear(Servico s) => new ServicoDto
        {
            Id = s.Id,
            Nome = s.Nome,
            ValorBase = s.ValorBase,
            PercentualImposto = s.PercentualImposto,
            Ativo = s.Ativo
        };
    }
}