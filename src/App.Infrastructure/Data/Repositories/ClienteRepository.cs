using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;

namespace App.Infrastructure.Data.Repositories
{
    public class ClienteRepository
    {
        private readonly UnitOfWork _uow;

        public ClienteRepository(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void Inserir(Cliente cliente)
        {
            const string sql = @"
                INSERT INTO clientes (nome, documento, tipo, email, telefone, data_cadastro, ativo)
                VALUES (@nome, @documento, @tipo, @email, @telefone, @dataCadastro, @ativo)
                RETURNING id";

            try
            {
                using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
                {
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                    cmd.Parameters.AddWithValue("@tipo", (int)cliente.Tipo);
                    cmd.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@telefone", (object)cliente.Telefone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dataCadastro", cliente.DataCadastro);
                    cmd.Parameters.AddWithValue("@ativo", cliente.Ativo);

                    // Corrigido: atribui o ID gerado de volta à entidade
                    var id = (int)cmd.ExecuteScalar();
                    cliente.DefinirId(id);
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                throw new DocumentoDuplicadoException();
            }

        }

        public void Atualizar(Cliente cliente)
        {
            const string sql = @"
                UPDATE clientes
                SET nome     = @nome,
                    email    = @email,
                    telefone = @telefone,
                    ativo    = @ativo
                WHERE id = @id";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefone", (object)cliente.Telefone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ativo", cliente.Ativo);
                cmd.Parameters.AddWithValue("@id", cliente.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Cliente> Listar(string nome, string documento, bool? ativo, int pagina, int tamanhoPagina)
        {
            var sql = @"
                SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo
                FROM clientes
                WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(nome))
                sql += " AND nome ILIKE @nome";
            if (!string.IsNullOrWhiteSpace(documento))
                sql += " AND documento = @documento";
            if (ativo.HasValue)
                sql += " AND ativo = @ativo";

            sql += " ORDER BY nome LIMIT @limite OFFSET @offset";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                if (!string.IsNullOrWhiteSpace(nome))
                    cmd.Parameters.AddWithValue("@nome", $"%{nome}%");
                if (!string.IsNullOrWhiteSpace(documento))
                    cmd.Parameters.AddWithValue("@documento", documento);
                if (ativo.HasValue)
                    cmd.Parameters.AddWithValue("@ativo", ativo.Value);

                cmd.Parameters.AddWithValue("@limite", tamanhoPagina);
                cmd.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                var lista = new List<Cliente>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(MapearCliente(reader));
                }

                return lista;
            }
        }

        public Cliente ObterPorId(int id)
        {
            const string sql = @"
                SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo
                FROM clientes
                WHERE id = @id";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? MapearCliente(reader) : null;
                }
            }
        }

        public bool PossuiOsVinculada(int clienteId)
        {
            const string sql = "SELECT 1 FROM ordens_servico WHERE cliente_id = @clienteId LIMIT 1";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@clienteId", clienteId);

                return cmd.ExecuteScalar() != null;
            }
        }

        private static Cliente MapearCliente(NpgsqlDataReader reader)
        {
            return Cliente.Reconstituir(
                id: reader.GetInt32(0),
                nome: reader.GetString(1),
                documento: reader.GetString(2),
                tipo: (TipoPessoa)reader.GetInt16(3),
                email: reader.IsDBNull(4) ? null : reader.GetString(4),
                telefone: reader.IsDBNull(5) ? null : reader.GetString(5),
                dataCadastro: reader.GetDateTime(6),
                ativo: reader.GetBoolean(7)
            );
        }
    }
}