using System;

namespace App.Domain.Entities
{
    public class Auditoria
    {
        private string _entidade;
        private string _operacao;
        private string _usuario;

        public int Id { get; private set; }
        public string Entidade
        {
            get => _entidade;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Entidade é obrigatória.");
                _entidade = value;
            }
        }
        public int IdRegistro { get; private set; }
        public string Operacao
        {
            get => _operacao;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Operação é obrigatória.");
                _operacao = value;
            }
        }
        public DateTime DataHora { get; private set; }
        public string Usuario
        {
            get => _usuario;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Usuário é obrigatório.");
                _usuario = value;
            }
        }
        public string SnapshotJson { get; private set; }

        public Auditoria(string entidade, int idRegistro, string operacao, string usuario, string snapshotJson)
        {
            Entidade = entidade;
            IdRegistro = idRegistro;
            Operacao = operacao;
            Usuario = usuario;
            SnapshotJson = snapshotJson;
            DataHora = DateTime.Now;
        }
    }
}