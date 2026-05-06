using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain.Entities
{
    public class OrdemServico
    {
        public int Id { get; private set; }
        public int ClienteId { get; private set; }

        public DateTime DataAbertura { get; private set; }
        public DateTime? DataConclusao { get; private set; }

        public StatusOrdemServico Status { get; private set; }
        public string Observacao { get; private set; }

        public decimal ValorTotal { get; private set; }
        public int Versao { get; private set; }

        private readonly List<OrdemServicoItem> _itens = new List<OrdemServicoItem>();
        public IReadOnlyCollection<OrdemServicoItem> Itens => _itens;

        public OrdemServico(int clienteId, string observacao)
        {
            ClienteId = clienteId;
            Observacao = observacao;
            DataAbertura = DateTime.Now;
            Status = StatusOrdemServico.Aberta;
        }

        public void DefinirId(int id) => Id = id;

        public void CarregarItens(IEnumerable<OrdemServicoItem> itens)
        {
            _itens.Clear();
            _itens.AddRange(itens);
        }

        public void DefinirVersao(int versao) => Versao = versao;

        public void AdicionarItem(OrdemServicoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            ValidarEdicaoItens();

            _itens.Add(item);
            RecalcularTotal();
        }

        public void RemoverItem(OrdemServicoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            ValidarEdicaoItens();

            _itens.Remove(item);
            RecalcularTotal();
        }

        private void RecalcularTotal() => ValorTotal = _itens.Sum(i => i.ValorTotalItem);

        private void ValidarEdicaoItens()
        {
            if (Status == StatusOrdemServico.Concluida)
                throw new InvalidOperationException("Esta OS já está concluída.");
            if (Status == StatusOrdemServico.Cancelada)
                throw new InvalidOperationException("Não é possível concluir uma OS cancelada.");

        }

        public void Concluir()
        {
            ValidarEdicaoItens();

            // Mesmo que no item possa ter valor unitario = 0, não deixa sair sem pelo menos 1 item com valor. (Não fere oque foi pedido)
            if (ValorTotal == 0)
                throw new InvalidOperationException("Não é possível concluir OS com valor total igual a zero");

            Status = StatusOrdemServico.Concluida;
            DataConclusao = DateTime.Now;
        }

        public void Cancelar()
        {
            if (Status == StatusOrdemServico.Concluida)
                throw new InvalidOperationException("Não é possível cancelar uma OS já concluída.");

            if (Status == StatusOrdemServico.Cancelada)
                throw new InvalidOperationException("Esta OS já está cancelada.");

            Status = StatusOrdemServico.Cancelada;
        }

        public void IniciarAndamento()
        {
            if (Status != StatusOrdemServico.Aberta)
                throw new InvalidOperationException("Apenas OS com status Aberta pode ser iniciada.");

            Status = StatusOrdemServico.EmAndamento;
        }

        // Construtor de reconstituição
        private OrdemServico() { }

        public static OrdemServico Reconstituir(int id, int clienteId, DateTime dataAbertura, DateTime? dataConclusao, StatusOrdemServico status, string observacao, decimal valorTotal, int versao)
        {
            return new OrdemServico
            {
                Id = id,
                ClienteId = clienteId,
                DataAbertura = dataAbertura,
                DataConclusao = dataConclusao,
                Status = status,
                Observacao = observacao,
                ValorTotal = valorTotal,
                Versao = versao
            };
        }
    }
}