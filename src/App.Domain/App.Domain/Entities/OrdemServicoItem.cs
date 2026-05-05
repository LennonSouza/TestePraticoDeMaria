using System;

namespace App.Domain.Entities
{
    public class OrdemServicoItem
    {
        private int _quantidade;

        public int Id { get; private set; }
        public int OrdemServicoId { get; private set; }
        public int ServicoId { get; private set; }

        public int Quantidade
        {
            get => _quantidade;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantidade deve ser maior que zero");

                _quantidade = value;
            }
        }

        public decimal ValorUnitario { get; private set; } // Pode ser 0, pode ser um item gratis (com 1 item leva outro)

        public decimal PercentualImpostoAplicado { get; private set; }

        public decimal ValorTotalItem { get; private set; }

        public OrdemServicoItem(int servicoId, int quantidade, decimal valorUnitario, decimal percentualImposto)
        {
            ServicoId = servicoId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            PercentualImpostoAplicado = percentualImposto;
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            var subtotal = Quantidade * ValorUnitario;
            ValorTotalItem = subtotal + subtotal * (PercentualImpostoAplicado / 100m);
        }

        // Construtor de reconstituição
        private OrdemServicoItem() { }

        public static OrdemServicoItem Reconstituir(int id, int ordemServicoId, int servicoId, int quantidade, decimal valorUnitario, decimal percentualImpostoAplicado, decimal valorTotalItem)
        {
            var item = new OrdemServicoItem
            {
                Id = id,
                OrdemServicoId = ordemServicoId,
                ServicoId = servicoId,
                ValorUnitario = valorUnitario,
                PercentualImpostoAplicado = percentualImpostoAplicado,
                ValorTotalItem = valorTotalItem
            };
            // Passa pelo setter para validar quantidade mesmo na reconstituição
            item.Quantidade = quantidade;
            return item;
        }
    }
}