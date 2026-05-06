using App.Domain.Enums;
using App.Infrastructure.Data;
using Npgsql;
using System;
using System.Collections.Generic;

namespace App.Infrastructure.Data.Repositories
{
    public class RelatorioRepository
    {
        private readonly UnitOfWork _uow;

        public RelatorioRepository(UnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<RelatorioLinha> Consultar(
            DateTime? dataInicio,
            DateTime? dataFim,
            int? clienteId,
            int? status)
        {
            var sql = @"
                SELECT
                    os.id,
                    os.cliente_id,
                    c.nome                          AS cliente_nome,
                    os.data_abertura,
                    os.data_conclusao,
                    os.status,
                    os.valor_total,
                    COALESCE(
                        SUM(
                            (i.quantidade * i.valor_unitario)
                            * (i.percentual_imposto_aplicado / 100.0)
                        ), 0
                    )                               AS total_impostos
                FROM ordens_servico os
                INNER JOIN clientes c ON c.id = os.cliente_id
                LEFT  JOIN ordens_servico_itens i ON i.ordem_servico_id = os.id
                WHERE 1=1";

            if (dataInicio.HasValue) sql += " AND os.data_abertura >= @dataInicio";
            if (dataFim.HasValue) sql += " AND os.data_abertura <= @dataFim";
            if (clienteId.HasValue) sql += " AND os.cliente_id = @clienteId";
            if (status.HasValue) sql += " AND os.status = @status";

            sql += @"
                GROUP BY os.id, os.cliente_id, c.nome,
                         os.data_abertura, os.data_conclusao,
                         os.status, os.valor_total
                ORDER BY c.nome, os.data_abertura";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                if (dataInicio.HasValue)
                    cmd.Parameters.AddWithValue("@dataInicio", dataInicio.Value);
                if (dataFim.HasValue)
                    cmd.Parameters.AddWithValue("@dataFim", dataFim.Value);
                if (clienteId.HasValue)
                    cmd.Parameters.AddWithValue("@clienteId", clienteId.Value);
                if (status.HasValue)
                    cmd.Parameters.AddWithValue("@status", status.Value);

                var lista = new List<RelatorioLinha>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new RelatorioLinha
                        {
                            OsId = reader.GetInt32(0),
                            ClienteId = reader.GetInt32(1),
                            ClienteNome = reader.GetString(2),
                            DataAbertura = reader.GetDateTime(3),
                            DataConclusao = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Status = (StatusOrdemServico)reader.GetInt16(5),
                            ValorTotal = reader.GetDecimal(6),
                            TotalImpostos = reader.GetDecimal(7)
                        });
                    }
                }
                return lista;
            }
        }
    }

    public class RelatorioLinha
    {
        public int OsId { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusOrdemServico Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalImpostos { get; set; }
    }
}