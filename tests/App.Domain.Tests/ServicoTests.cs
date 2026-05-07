using App.Domain.Entities;

namespace App.Domain.Tests.Entities
{
    public class ServicoTests
    {
        // ---------------------------------------------------------------
        // Construtor
        // ---------------------------------------------------------------

        [Fact]
        public void Construtor_NomeVazio_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Servico("", 100m, 10m));
        }

        [Fact]
        public void Construtor_ValorBaseZero_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Servico("Consultoria", 0m, 10m));
        }

        [Fact]
        public void Construtor_ValorBaseNegativo_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Servico("Consultoria", -1m, 10m));
        }

        [Fact]
        public void Construtor_PercentualImpostoNegativo_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Servico("Consultoria", 100m, -1m));
        }

        [Fact]
        public void Construtor_PercentualImpostoAcimaDecem_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Servico("Consultoria", 100m, 100.01m));
        }

        [Fact]
        public void Construtor_PercentualImpostoZero_DevePermitir()
        {
            var servico = new Servico("Treinamento", 400m, 0m);

            Assert.Equal(0m, servico.PercentualImposto);
        }

        [Fact]
        public void Construtor_PercentualImposto100_DevePermitir()
        {
            var servico = new Servico("Servico Especial", 100m, 100m);

            Assert.Equal(100m, servico.PercentualImposto);
        }

        [Fact]
        public void Construtor_DadosValidos_DeveInicializarCorretamente()
        {
            var servico = new Servico("Consultoria", 250m, 8m);

            Assert.Equal("Consultoria", servico.Nome);
            Assert.Equal(250m, servico.ValorBase);
            Assert.Equal(8m, servico.PercentualImposto);
            Assert.True(servico.Ativo);
        }

        // ---------------------------------------------------------------
        // Atualizar
        // ---------------------------------------------------------------

        [Fact]
        public void Atualizar_ValorBaseZero_DeveLancarArgumentException()
        {
            var servico = new Servico("Consultoria", 250m, 8m);

            Assert.Throws<ArgumentException>(() =>
                servico.Atualizar("Consultoria", 0m, 8m, true));
        }

        [Fact]
        public void Atualizar_DadosValidos_DeveAtualizar()
        {
            var servico = new Servico("Consultoria", 250m, 8m);

            servico.Atualizar("Consultoria Premium", 350m, 10m, false);

            Assert.Equal("Consultoria Premium", servico.Nome);
            Assert.Equal(350m, servico.ValorBase);
            Assert.Equal(10m, servico.PercentualImposto);
            Assert.False(servico.Ativo);
        }

        [Fact]
        public void Atualizar_AtivoNulo_NaoDeveAlterarAtivo()
        {
            var servico = new Servico("Consultoria", 250m, 8m);
            Assert.True(servico.Ativo);

            servico.Atualizar("Consultoria", 250m, 8m, null);

            Assert.True(servico.Ativo);
        }

        // ---------------------------------------------------------------
        // Ativar / Desativar
        // ---------------------------------------------------------------

        [Fact]
        public void Desativar_DeveDefinirAtivoComoFalse()
        {
            var servico = new Servico("Consultoria", 250m, 8m);
            servico.Desativar();

            Assert.False(servico.Ativo);
        }

        [Fact]
        public void Ativar_DeveDefinirAtivoComoTrue()
        {
            var servico = new Servico("Consultoria", 250m, 8m);
            servico.Desativar();
            servico.Ativar();

            Assert.True(servico.Ativo);
        }

        // ---------------------------------------------------------------
        // Reconstituir
        // ---------------------------------------------------------------

        [Fact]
        public void Reconstituir_DeveRestaurarTodosOsCampos()
        {
            var servico = Servico.Reconstituir(
                id: 10,
                nome: "Suporte Remoto",
                valorBase: 120m,
                percentualImposto: 5m,
                ativo: false
            );

            Assert.Equal(10, servico.Id);
            Assert.Equal("Suporte Remoto", servico.Nome);
            Assert.Equal(120m, servico.ValorBase);
            Assert.Equal(5m, servico.PercentualImposto);
            Assert.False(servico.Ativo);
        }
    }
}