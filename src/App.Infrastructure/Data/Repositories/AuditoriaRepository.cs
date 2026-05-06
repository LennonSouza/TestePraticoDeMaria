using App.Domain.Entities;
using Npgsql;

namespace App.Infrastructure.Data.Repositories
{
    public class AuditoriaRepository
    {
        private readonly UnitOfWork _uow;

        public AuditoriaRepository(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void Inserir(Auditoria auditoria)
        {
            const string sql = @"
                INSERT INTO auditorias (entidade, id_registro, operacao, data_hora, usuario, snapshot_json)
                VALUES (@entidade, @idRegistro, @operacao, @dataHora, @usuario, @snapshotJson)";

            using (var cmd = new NpgsqlCommand(sql, _uow.Connection, _uow.Transaction))
            {
                cmd.Parameters.AddWithValue("@entidade", auditoria.Entidade);
                cmd.Parameters.AddWithValue("@idRegistro", auditoria.IdRegistro);
                cmd.Parameters.AddWithValue("@operacao", auditoria.Operacao);
                cmd.Parameters.AddWithValue("@dataHora", auditoria.DataHora);
                cmd.Parameters.AddWithValue("@usuario", auditoria.Usuario);
                cmd.Parameters.AddWithValue("@snapshotJson", auditoria.SnapshotJson);

                cmd.ExecuteNonQuery();
            }
        }
    }
}