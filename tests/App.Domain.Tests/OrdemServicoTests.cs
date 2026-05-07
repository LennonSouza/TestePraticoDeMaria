using App.Domain.Entities;
using App.Domain.Enums;

namespace App.Domain.Tests.Entities
{
    public class OrdemServicoTests
    {
        private static OrdemServicoItem CriarItem(decimal valorUnitario = 100m, int quantidade = 1, decimal imposto = 0m)
            => new OrdemServicoItem(servicoId: 1, quantidade: quantidade, valorUnitario: valorUnitario, percentualImposto: imposto);

        // ---------------------------------------------------------------
        // Construtor
        // ---------------------------------------------------------------

        [Fact]
        public void Construtor_DeveInicializarComStatusAberta()
        {
            var os = new OrdemServico(clienteId: 1, observacao: "Teste");

            Assert.Equal(StatusOrdemServico.Aberta, os.Status);
        }

        [Fact]
        public void Construtor_DeveInicializarComValorTotalZero()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);

            Assert.Equal(0m, os.ValorTotal);
        }

        [Fact]
        public void Construtor_DeveInicializarSemItens()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);

            Assert.Empty(os.Itens);
        }

        // ---------------------------------------------------------------
        // AdicionarItem
        // ---------------------------------------------------------------

        [Fact]
        public void AdicionarItem_DeveRecalcularValorTotal()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            var item = CriarItem(valorUnitario: 300m, quantidade: 2);

            os.AdicionarItem(item);

            Assert.Equal(600m, os.ValorTotal);
        }

        [Fact]
        public void AdicionarItem_DoisItens_DeveSomarTotais()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);

            os.AdicionarItem(CriarItem(valorUnitario: 100m, quantidade: 1));
            os.AdicionarItem(CriarItem(valorUnitario: 200m, quantidade: 2));

            Assert.Equal(500m, os.ValorTotal);
        }

        [Fact]
        public void AdicionarItem_Nulo_DeveLancarArgumentNullException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);

            Assert.Throws<ArgumentNullException>(() => os.AdicionarItem(null));
        }

        [Fact]
        public void AdicionarItem_StatusConcluida_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.AdicionarItem(CriarItem(valorUnitario: 100m));
            os.IniciarAndamento();
            os.Concluir();

            Assert.Throws<InvalidOperationException>(() =>
                os.AdicionarItem(CriarItem(valorUnitario: 50m)));
        }

        [Fact]
        public void AdicionarItem_StatusCancelada_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.Cancelar();

            Assert.Throws<InvalidOperationException>(() =>
                os.AdicionarItem(CriarItem(valorUnitario: 50m)));
        }

        // ---------------------------------------------------------------
        // RemoverItem
        // ---------------------------------------------------------------

        [Fact]
        public void RemoverItem_DeveRecalcularValorTotal()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            var item1 = CriarItem(valorUnitario: 100m);
            var item2 = CriarItem(valorUnitario: 200m);

            os.AdicionarItem(item1);
            os.AdicionarItem(item2);
            os.RemoverItem(item1);

            Assert.Equal(200m, os.ValorTotal);
        }

        [Fact]
        public void RemoverItem_StatusConcluida_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            var item = CriarItem(valorUnitario: 100m);
            os.AdicionarItem(item);
            os.IniciarAndamento();
            os.Concluir();

            Assert.Throws<InvalidOperationException>(() => os.RemoverItem(item));
        }

        // ---------------------------------------------------------------
        // IniciarAndamento
        // ---------------------------------------------------------------

        [Fact]
        public void IniciarAndamento_DeStatusAberta_DeveAlterarStatus()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.IniciarAndamento();

            Assert.Equal(StatusOrdemServico.EmAndamento, os.Status);
        }

        [Fact]
        public void IniciarAndamento_DeStatusEmAndamento_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.IniciarAndamento();

            Assert.Throws<InvalidOperationException>(() => os.IniciarAndamento());
        }

        [Fact]
        public void IniciarAndamento_DeStatusCancelada_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.Cancelar();

            Assert.Throws<InvalidOperationException>(() => os.IniciarAndamento());
        }

        // ---------------------------------------------------------------
        // Concluir
        // ---------------------------------------------------------------

        [Fact]
        public void Concluir_ComValorTotalZero_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);

            Assert.Throws<InvalidOperationException>(() => os.Concluir());
        }

        [Fact]
        public void Concluir_ComItens_DeveAlterarStatusEDefinirDataConclusao()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.AdicionarItem(CriarItem(valorUnitario: 500m));
            os.IniciarAndamento();
            os.Concluir();

            Assert.Equal(StatusOrdemServico.Concluida, os.Status);
            Assert.NotNull(os.DataConclusao);
        }

        [Fact]
        public void Concluir_JaConcluida_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.AdicionarItem(CriarItem(valorUnitario: 100m));
            os.IniciarAndamento();
            os.Concluir();

            Assert.Throws<InvalidOperationException>(() => os.Concluir());
        }

        [Fact]
        public void Concluir_Cancelada_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.Cancelar();

            Assert.Throws<InvalidOperationException>(() => os.Concluir());
        }

        // ---------------------------------------------------------------
        // Cancelar
        // ---------------------------------------------------------------

        [Fact]
        public void Cancelar_DeStatusAberta_DeveAlterarStatus()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.Cancelar();

            Assert.Equal(StatusOrdemServico.Cancelada, os.Status);
        }

        [Fact]
        public void Cancelar_DeStatusEmAndamento_DeveAlterarStatus()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.IniciarAndamento();
            os.Cancelar();

            Assert.Equal(StatusOrdemServico.Cancelada, os.Status);
        }

        [Fact]
        public void Cancelar_JaCancelada_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.Cancelar();

            Assert.Throws<InvalidOperationException>(() => os.Cancelar());
        }

        [Fact]
        public void Cancelar_JaConcluida_DeveLancarInvalidOperationException()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.AdicionarItem(CriarItem(valorUnitario: 100m));
            os.IniciarAndamento();
            os.Concluir();

            Assert.Throws<InvalidOperationException>(() => os.Cancelar());
        }

        // ---------------------------------------------------------------
        // CarregarItens
        // ---------------------------------------------------------------

        [Fact]
        public void CarregarItens_DeveRecalcularValorTotal()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);

            var itens = new[]
            {
                CriarItem(valorUnitario: 100m, quantidade: 2),
                CriarItem(valorUnitario: 50m,  quantidade: 3)
            };

            os.CarregarItens(itens);

            Assert.Equal(350m, os.ValorTotal);
            Assert.Equal(2, os.Itens.Count);
        }

        [Fact]
        public void CarregarItens_DeveSubstituirItensAnteriores()
        {
            var os = new OrdemServico(clienteId: 1, observacao: null);
            os.AdicionarItem(CriarItem(valorUnitario: 999m));

            os.CarregarItens(new[] { CriarItem(valorUnitario: 100m) });

            Assert.Single(os.Itens);
            Assert.Equal(100m, os.ValorTotal);
        }

        // ---------------------------------------------------------------
        // Reconstituir
        // ---------------------------------------------------------------

        [Fact]
        public void Reconstituir_DeveRestaurarTodosOsCampos()
        {
            var abertura = new DateTime(2026, 5, 1);
            var conclusao = new DateTime(2026, 5, 3);

            var os = OrdemServico.Reconstituir(
                id: 100,
                clienteId: 5,
                dataAbertura: abertura,
                dataConclusao: conclusao,
                status: StatusOrdemServico.Concluida,
                observacao: "Obs teste",
                valorTotal: 1500m,
                versao: 3
            );

            Assert.Equal(100, os.Id);
            Assert.Equal(5, os.ClienteId);
            Assert.Equal(abertura, os.DataAbertura);
            Assert.Equal(conclusao, os.DataConclusao);
            Assert.Equal(StatusOrdemServico.Concluida, os.Status);
            Assert.Equal("Obs teste", os.Observacao);
            Assert.Equal(1500m, os.ValorTotal);
            Assert.Equal(3, os.Versao);
        }
    }
}