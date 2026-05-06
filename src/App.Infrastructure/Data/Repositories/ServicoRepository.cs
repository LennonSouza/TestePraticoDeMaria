using App.Domain.Entities;
using Npgsql;
using System.Collections.Generic;

namespace App.Infrastructure.Data.Repositories
{
    public class ServicoRepository
    {
        private readonly UnitOfWork _uow;

        public ServicoRepository(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void Inserir(Servico servico)
        {
            const string sql = @"
                INSERT INTO servicos (nome, valor_base, percentual_imposto, ativo)
                VALUES (@nome, @valorBase, @percentualImposto, @ativo)";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@nome", servico.Nome);
                cmd.Parameters.AddWithValue("@valorBase", servico.ValorBase);
                cmd.Parameters.AddWithValue("@percentualImposto", servico.PercentualImposto);
                cmd.Parameters.AddWithValue("@ativo", servico.Ativo);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Servico servico)
        {
            const string sql = @"
                UPDATE servicos
                SET nome               = @nome,
                    valor_base         = @valorBase,
                    percentual_imposto = @percentualImposto,
                    ativo              = @ativo
                WHERE id = @id";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@nome", servico.Nome);
                cmd.Parameters.AddWithValue("@valorBase", servico.ValorBase);
                cmd.Parameters.AddWithValue("@percentualImposto", servico.PercentualImposto);
                cmd.Parameters.AddWithValue("@ativo", servico.Ativo);
                cmd.Parameters.AddWithValue("@id", servico.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Servico> Listar(bool? ativo, int pagina, int tamanhoPagina)
        {
            var sql = @"
                SELECT id, nome, valor_base, percentual_imposto, ativo
                FROM servicos
                WHERE 1=1";

            if (ativo.HasValue)
                sql += " AND ativo = @ativo";

            sql += " ORDER BY nome LIMIT @limite OFFSET @offset";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                if (ativo.HasValue)
                    cmd.Parameters.AddWithValue("@ativo", ativo.Value);

                cmd.Parameters.AddWithValue("@limite", tamanhoPagina);
                cmd.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                var lista = new List<Servico>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(MapearServico(reader));
                }
               
                return lista;
            }
        }

        public Servico ObterPorId(int id)
        {
            const string sql = @"
                SELECT id, nome, valor_base, percentual_imposto, ativo
                FROM servicos WHERE id = @id";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? MapearServico(reader) : null;
                }
            }
        }

        private static Servico MapearServico(NpgsqlDataReader reader)
        {
            return Servico.Reconstituir(
                id: reader.GetInt32(0),
                nome: reader.GetString(1),
                valorBase: reader.GetDecimal(2),
                percentualImposto: reader.GetDecimal(3),
                ativo: reader.GetBoolean(4)
            );
        }
    }
}