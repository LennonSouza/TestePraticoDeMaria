using App.Domain.Entities;

namespace App.Domain.Tests.Entities
{
    public class OrdemServicoItemTests
    {
        // ---------------------------------------------------------------
        // Construtor e calculo de total
        // ---------------------------------------------------------------

        [Fact]
        public void Construtor_QuantidadeZero_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new OrdemServicoItem(servicoId: 1, quantidade: 0, valorUnitario: 100m, percentualImposto: 10m));
        }

        [Fact]
        public void Construtor_QuantidadeNegativa_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new OrdemServicoItem(servicoId: 1, quantidade: -1, valorUnitario: 100m, percentualImposto: 10m));
        }

        [Fact]
        public void Construtor_SemImposto_DeveCalcularTotalCorreto()
        {
            // 3 x R$100 sem imposto = R$300
            var item = new OrdemServicoItem(
                servicoId: 1,
                quantidade: 3,
                valorUnitario: 100m,
                percentualImposto: 0m
            );

            Assert.Equal(300m, item.ValorTotalItem);
        }

        [Fact]
        public void Construtor_ComImposto_DeveCalcularTotalCorreto()
        {
            // 2 x R$800 + 8% imposto
            // subtotal = 1600
            // imposto  = 1600 * 0.08 = 128
            // total    = 1728
            var item = new OrdemServicoItem(
                servicoId: 1,
                quantidade: 2,
                valorUnitario: 800m,
                percentualImposto: 8m
            );

            Assert.Equal(1728m, item.ValorTotalItem);
        }

        [Fact]
        public void Construtor_ValorUnitarioZero_DevePermitir()
        {
            // Item gratuito e valido (ex: brinde, 1 leva 1 gratis)
            var item = new OrdemServicoItem(
                servicoId: 1,
                quantidade: 1,
                valorUnitario: 0m,
                percentualImposto: 0m
            );

            Assert.Equal(0m, item.ValorTotalItem);
        }

        [Fact]
        public void Construtor_DadosValidos_DeveInicializarCorretamente()
        {
            var item = new OrdemServicoItem(
                servicoId: 5,
                quantidade: 4,
                valorUnitario: 120m,
                percentualImposto: 5m
            );

            Assert.Equal(5, item.ServicoId);
            Assert.Equal(4, item.Quantidade);
            Assert.Equal(120m, item.ValorUnitario);
            Assert.Equal(5m, item.PercentualImpostoAplicado);
        }

        [Theory]
        [InlineData(1, 100, 0, 100)]
        [InlineData(2, 100, 0, 200)]
        [InlineData(1, 100, 10, 110)]
        [InlineData(2, 100, 10, 220)]
        [InlineData(3, 250, 8, 810)]
        [InlineData(4, 120, 5, 504)]
        public void Construtor_CalculoDeTotal_DeveEstarCorreto(
            int quantidade, decimal valorUnitario, decimal percentual, decimal totalEsperado)
        {
            var item = new OrdemServicoItem(1, quantidade, valorUnitario, percentual);

            Assert.Equal(totalEsperado, item.ValorTotalItem);
        }

        // ---------------------------------------------------------------
        // Reconstituir
        // ---------------------------------------------------------------

        [Fact]
        public void Reconstituir_DeveRestaurarTodosOsCampos()
        {
            var item = OrdemServicoItem.Reconstituir(
                id: 99,
                ordemServicoId: 10,
                servicoId: 3,
                quantidade: 2,
                valorUnitario: 500m,
                percentualImpostoAplicado: 8m,
                valorTotalItem: 1080m
            );

            Assert.Equal(99, item.Id);
            Assert.Equal(10, item.OrdemServicoId);
            Assert.Equal(3, item.ServicoId);
            Assert.Equal(2, item.Quantidade);
            Assert.Equal(500m, item.ValorUnitario);
            Assert.Equal(8m, item.PercentualImpostoAplicado);
            Assert.Equal(1080m, item.ValorTotalItem);
        }
    }
}