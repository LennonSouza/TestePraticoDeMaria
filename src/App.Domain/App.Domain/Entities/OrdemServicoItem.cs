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
    }
}