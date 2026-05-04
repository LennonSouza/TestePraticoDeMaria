using App.Domain.Enums;
using System;

namespace App.Domain.Entities
{
    public class HistoricoStatus
    {
        private string _usuario;

        public int Id { get; private set; }
        public int OrdemServicoId { get; private set; }
        public StatusOrdemServico StatusAnterior { get; private set; }
        public StatusOrdemServico StatusNovo { get; private set; }
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
        public string Observacao { get; private set; }

        public HistoricoStatus(int ordemServicoId, StatusOrdemServico statusAnterior, StatusOrdemServico statusNovo, string usuario, string observacao = null)
        {
      

            OrdemServicoId = ordemServicoId;
            StatusAnterior = statusAnterior;
            StatusNovo = statusNovo;
            Usuario = usuario;
            Observacao = observacao;
            DataHora = DateTime.Now;
        }
    }
}