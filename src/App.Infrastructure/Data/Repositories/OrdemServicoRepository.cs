using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;

namespace App.Infrastructure.Data.Repositories
{
    public class OrdemServicoRepository
    {
        private readonly UnitOfWork _uow;

        public OrdemServicoRepository(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void Inserir(OrdemServico os)
        {
            const string sql = @"
                INSERT INTO ordens_servico
                    (cliente_id, data_abertura, status, observacao, valor_total, versao)
                VALUES
                    (@clienteId, @dataAbertura, @status, @observacao, @valorTotal, 0)
                RETURNING id";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@clienteId", os.ClienteId);
                cmd.Parameters.AddWithValue("@dataAbertura", os.DataAbertura);
                cmd.Parameters.AddWithValue("@status", (int)os.Status);
                cmd.Parameters.AddWithValue("@observacao", (object)os.Observacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@valorTotal", os.ValorTotal);

                var id = (int)cmd.ExecuteScalar();
                os.DefinirId(id);
            } 
        }

        public void Atualizar(OrdemServico os)
        {
            const string sql = @"
                UPDATE ordens_servico
                SET status         = @status,
                    observacao     = @observacao,
                    valor_total    = @valorTotal,
                    data_conclusao = @dataConclusao,
                    versao         = versao + 1
                WHERE id = @id AND versao = @versao";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@status", (int)os.Status);
                cmd.Parameters.AddWithValue("@observacao", (object)os.Observacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@valorTotal", os.ValorTotal);
                cmd.Parameters.AddWithValue("@dataConclusao", (object)os.DataConclusao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", os.Id);
                cmd.Parameters.AddWithValue("@versao", os.Versao);

                var linhasAfetadas = cmd.ExecuteNonQuery();

                // Controle de concorrência otimista — requisito 4.2
                if (linhasAfetadas == 0)
                    throw new ConcorrenciaException("Esta OS foi alterada por outro usuário. Recarregue e tente novamente.");
            } 
        }

        public void InserirItem(OrdemServicoItem item, int ordemServicoId)
        {
            const string sql = @"
                INSERT INTO ordens_servico_itens
                    (ordem_servico_id, servico_id, quantidade, valor_unitario,
                     percentual_imposto_aplicado, valor_total_item)
                VALUES
                    (@osId, @servicoId, @quantidade, @valorUnitario,
                     @percentualImposto, @valorTotalItem)";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@osId", ordemServicoId);
                cmd.Parameters.AddWithValue("@servicoId", item.ServicoId);
                cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmd.Parameters.AddWithValue("@valorUnitario", item.ValorUnitario);
                cmd.Parameters.AddWithValue("@percentualImposto", item.PercentualImpostoAplicado);
                cmd.Parameters.AddWithValue("@valorTotalItem", item.ValorTotalItem);

                cmd.ExecuteNonQuery();
            } 
        }

        public void RemoverItens(int ordemServicoId)
        {
            const string sql = "DELETE FROM ordens_servico_itens WHERE ordem_servico_id = @osId";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@osId", ordemServicoId);
                cmd.ExecuteNonQuery();
            }
        }

        public void InserirHistoricoStatus(HistoricoStatus historico)
        {
            const string sql = @"
                INSERT INTO historico_status
                    (ordem_servico_id, status_anterior, status_novo, data_hora, usuario, observacao)
                VALUES
                    (@osId, @statusAnterior, @statusNovo, @dataHora, @usuario, @observacao)";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@osId", historico.OrdemServicoId);
                cmd.Parameters.AddWithValue("@statusAnterior", (int)historico.StatusAnterior);
                cmd.Parameters.AddWithValue("@statusNovo", (int)historico.StatusNovo);
                cmd.Parameters.AddWithValue("@dataHora", historico.DataHora);
                cmd.Parameters.AddWithValue("@usuario", historico.Usuario);
                cmd.Parameters.AddWithValue("@observacao", (object)historico.Observacao ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<OrdemServico> Listar(DateTime? dataInicio, DateTime? dataFim,
    int? clienteId, StatusOrdemServico? status, int pagina, int tamanhoPagina)
        {
            var sql = @"
        SELECT os.id, os.cliente_id, c.nome, os.data_abertura, os.data_conclusao,
               os.status, os.observacao, os.valor_total, os.versao
        FROM ordens_servico os
        INNER JOIN clientes c ON c.id = os.cliente_id
        WHERE 1=1";

            if (dataInicio.HasValue) sql += " AND os.data_abertura >= @dataInicio";
            if (dataFim.HasValue) sql += " AND os.data_abertura <= @dataFim";
            if (clienteId.HasValue) sql += " AND os.cliente_id = @clienteId";
            if (status.HasValue) sql += " AND os.status = @status";

            sql += " ORDER BY os.data_abertura DESC LIMIT @limite OFFSET @offset";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                if (dataInicio.HasValue) cmd.Parameters.AddWithValue("@dataInicio", dataInicio.Value);
                if (dataFim.HasValue) cmd.Parameters.AddWithValue("@dataFim", dataFim.Value);
                if (clienteId.HasValue) cmd.Parameters.AddWithValue("@clienteId", clienteId.Value);
                if (status.HasValue) cmd.Parameters.AddWithValue("@status", (int)status.Value);

                cmd.Parameters.AddWithValue("@limite", tamanhoPagina);
                cmd.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                var lista = new List<OrdemServico>();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) lista.Add(MapearOs(reader));

                return lista;
            }
        }

        public OrdemServico ObterPorId(int id)
        {
            const string sql = @"
        SELECT os.id, os.cliente_id, c.nome, os.data_abertura, os.data_conclusao,
               os.status, os.observacao, os.valor_total, os.versao
        FROM ordens_servico os
        INNER JOIN clientes c ON c.id = os.cliente_id
        WHERE os.id = @id";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    var os = MapearOs(reader);
                    reader.Close();

                    os.CarregarItens(ObterItens(id));
                    return os;
                }
            }
        }

        private IEnumerable<OrdemServicoItem> ObterItens(int ordemServicoId)
        {
            const string sql = @"
        SELECT 
            i.id, i.ordem_servico_id, i.servico_id, i.quantidade,
            i.valor_unitario, i.percentual_imposto_aplicado, i.valor_total_item,
            s.nome AS servico_nome
        FROM ordens_servico_itens i
        INNER JOIN servicos s ON s.id = i.servico_id
        WHERE i.ordem_servico_id = @osId";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@osId", ordemServicoId);

                var itens = new List<OrdemServicoItem>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        itens.Add(OrdemServicoItem.Reconstituir(
                            id: reader.GetInt32(0),
                            ordemServicoId: reader.GetInt32(1),
                            servicoId: reader.GetInt32(2),
                            quantidade: reader.GetInt32(3),
                            valorUnitario: reader.GetDecimal(4),
                            percentualImpostoAplicado: reader.GetDecimal(5),
                            valorTotalItem: reader.GetDecimal(6),
                            servicoNome: reader.GetString(7)
                        ));
                }
                return itens;
            }
        }

        private static OrdemServico MapearOs(NpgsqlDataReader r)
        {
            return OrdemServico.Reconstituir(
                id: r.GetInt32(0),
                clienteId: r.GetInt32(1),
                clienteNome: r.GetString(2),      // novo campo
                dataAbertura: r.GetDateTime(3),
                dataConclusao: r.IsDBNull(4) ? (DateTime?)null : r.GetDateTime(4),
                status: (StatusOrdemServico)r.GetInt16(5),
                observacao: r.IsDBNull(6) ? null : r.GetString(6),
                valorTotal: r.GetDecimal(7),
                versao: r.GetInt32(8)
            );
        }
    }
}