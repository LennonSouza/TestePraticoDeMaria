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
    public class ClienteService
    {
        private readonly ConnectionFactory _factory;
        private readonly FileLogger _logger;
        private readonly string _usuarioAtual;

        public ClienteService(ConnectionFactory factory, FileLogger logger, string usuarioAtual)
        {
            _factory = factory;
            _logger = logger;
            _usuarioAtual = usuarioAtual;
        }

        public void Cadastrar(CadastrarClienteCommand cmd)
        {
            try
            {
                var cliente = new Cliente(cmd.Nome, cmd.Documento, (TipoPessoa)cmd.Tipo);
                cliente.AtualizarDados(cmd.Nome, cmd.Email, cmd.Telefone, null);

                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new ClienteRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        repo.Inserir(cliente);

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "Cliente",
                            idRegistro: cliente.Id,
                            operacao: "INSERT",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(cliente)
                        ));

                        uow.Commit();
                        _logger.Info($"Cliente '{cmd.Nome}' cadastrado por {_usuarioAtual}.");
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
                _logger.Erro("Erro de validação ao cadastrar cliente.", ex);
                throw;
            }
            catch (App.Infrastructure.Exceptions.DocumentoDuplicadoException ex)
            {
                _logger.Erro("Documento duplicado ao cadastrar cliente.", ex);
                throw new App.Application.Exceptions.DocumentoDuplicadoException();
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro inesperado ao cadastrar cliente.", ex);
                throw;
            }
        }

        public void Atualizar(AtualizarClienteCommand cmd)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new ClienteRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        var cliente = repo.ObterPorId(cmd.Id);
                        if (cliente == null)
                            throw new InvalidOperationException("Cliente não encontrado.");

                        cliente.AtualizarDados(cmd.Nome, cmd.Email, cmd.Telefone, cmd.Ativo);
                        repo.Atualizar(cliente);

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "Cliente",
                            idRegistro: cliente.Id,
                            operacao: "UPDATE",
                            usuario: _usuarioAtual,
                            snapshotJson: JsonConvert.SerializeObject(cliente)
                        ));

                        uow.Commit();
                        _logger.Info($"Cliente id={cmd.Id} atualizado por {_usuarioAtual}.");
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
                _logger.Erro("Erro de negócio ao atualizar cliente.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro inesperado ao atualizar cliente.", ex);
                throw;
            }
        }

        public bool PossuiOsVinculada(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    return new ClienteRepository(uow).PossuiOsVinculada(id);
                }
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro ao verificar OS vinculada ao cliente id={id}.", ex);
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    try
                    {
                        var repo = new ClienteRepository(uow);
                        var auditoriaRepo = new AuditoriaRepository(uow);

                        var cliente = repo.ObterPorId(id);
                        if (cliente == null)
                            throw new InvalidOperationException("Cliente não encontrado.");

                        if (repo.PossuiOsVinculada(id))
                            throw new InvalidOperationException(
                                "Não é possível excluir cliente com OS vinculada.");

                        auditoriaRepo.Inserir(new Auditoria(
                            entidade: "Cliente",
                            idRegistro: id,
                            operacao: "DELETE",
                            usuario: _usuarioAtual,
                            snapshotJson: Newtonsoft.Json.JsonConvert.SerializeObject(cliente)
                        ));

                        repo.Excluir(id);

                        uow.Commit();
                        _logger.Info($"Cliente id={id} excluído por {_usuarioAtual}.");
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
                _logger.Erro("Erro de negócio ao excluir cliente.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro inesperado ao excluir cliente id={id}.", ex);
                throw;
            }
        }

        public IEnumerable<ClienteDto> Listar(string nome, string documento, bool? ativo,
            int pagina = 1, int tamanhoPagina = 20)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    return new ClienteRepository(uow)
                        .Listar(nome, documento, ativo, pagina, tamanhoPagina)
                        .Select(Mapear);
                }
            }
            catch (Exception ex)
            {
                _logger.Erro("Erro ao listar clientes.", ex);
                throw;
            }
        }

        public ClienteDto ObterPorId(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(_factory))
                {
                    var cliente = new ClienteRepository(uow).ObterPorId(id);
                    return cliente == null ? null : Mapear(cliente);
                }
            }
            catch (Exception ex)
            {
                _logger.Erro($"Erro ao obter cliente id={id}.", ex);
                throw;
            }
        }

        private static ClienteDto Mapear(Cliente c) => new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Documento = c.Documento,
            Tipo = c.Tipo.ToString(),
            Email = c.Email,
            Telefone = c.Telefone,
            DataCadastro = c.DataCadastro,
            Ativo = c.Ativo
        };
    }
}