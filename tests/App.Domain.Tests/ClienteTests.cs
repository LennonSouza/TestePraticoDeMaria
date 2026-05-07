using App.Domain.Entities;
using App.Domain.Enums;

namespace App.Domain.Tests.Entities
{
    public class ClienteTests
    {
        // ---------------------------------------------------------------
        // Construtor
        // ---------------------------------------------------------------

        [Fact]
        public void Construtor_NomeVazio_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Cliente("", "123.456.789-00", TipoPessoa.Fisica));
        }

        [Fact]
        public void Construtor_NomeNulo_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Cliente(null, "123.456.789-00", TipoPessoa.Fisica));
        }

        [Fact]
        public void Construtor_DocumentoVazio_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Cliente("Joao Silva", "", TipoPessoa.Fisica));
        }

        [Fact]
        public void Construtor_DocumentoNulo_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Cliente("Joao Silva", null, TipoPessoa.Fisica));
        }

        [Fact]
        public void Construtor_DadosValidos_DeveInicializarCorretamente()
        {
            var cliente = new Cliente("Joao Silva", "123.456.789-00", TipoPessoa.Fisica);

            Assert.Equal("Joao Silva", cliente.Nome);
            Assert.Equal("123.456.789-00", cliente.Documento);
            Assert.Equal(TipoPessoa.Fisica, cliente.Tipo);
            Assert.True(cliente.Ativo);
            Assert.True(cliente.DataCadastro <= DateTime.Now);
        }

        // ---------------------------------------------------------------
        // AtualizarDados
        // ---------------------------------------------------------------

        [Fact]
        public void AtualizarDados_NomeVazio_DeveLancarArgumentException()
        {
            var cliente = new Cliente("Joao Silva", "123.456.789-00", TipoPessoa.Fisica);

            Assert.Throws<ArgumentException>(() =>
                cliente.AtualizarDados("", "email@test.com", "11999999999", true));
        }

        [Fact]
        public void AtualizarDados_DadosValidos_DeveAtualizar()
        {
            var cliente = new Cliente("Joao Silva", "123.456.789-00", TipoPessoa.Fisica);

            cliente.AtualizarDados("Joao Santos", "novo@email.com", "11888888888", false);

            Assert.Equal("Joao Santos", cliente.Nome);
            Assert.Equal("novo@email.com", cliente.Email);
            Assert.Equal("11888888888", cliente.Telefone);
            Assert.False(cliente.Ativo);
        }

        [Fact]
        public void AtualizarDados_AtivoNulo_NaoDeveAlterarAtivo()
        {
            var cliente = new Cliente("Joao Silva", "123.456.789-00", TipoPessoa.Fisica);
            Assert.True(cliente.Ativo);

            cliente.AtualizarDados("Joao Silva", null, null, null);

            Assert.True(cliente.Ativo);
        }

        // ---------------------------------------------------------------
        // Ativar / Desativar
        // ---------------------------------------------------------------

        [Fact]
        public void Desativar_DeveDefinirAtivoComoFalse()
        {
            var cliente = new Cliente("Joao Silva", "123.456.789-00", TipoPessoa.Fisica);
            cliente.Desativar();

            Assert.False(cliente.Ativo);
        }

        [Fact]
        public void Ativar_DeveDefinirAtivoComoTrue()
        {
            var cliente = new Cliente("Joao Silva", "123.456.789-00", TipoPessoa.Fisica);
            cliente.Desativar();
            cliente.Ativar();

            Assert.True(cliente.Ativo);
        }

        // ---------------------------------------------------------------
        // Reconstituir
        // ---------------------------------------------------------------

        [Fact]
        public void Reconstituir_DeveRestaurarTodosOsCampos()
        {
            var data = new DateTime(2026, 1, 15, 10, 30, 0);

            var cliente = Cliente.Reconstituir(
                id: 42,
                nome: "Empresa XYZ",
                documento: "12.345.678/0001-99",
                tipo: TipoPessoa.Juridica,
                email: "contato@xyz.com",
                telefone: "11333333333",
                dataCadastro: data,
                ativo: true
            );

            Assert.Equal(42, cliente.Id);
            Assert.Equal("Empresa XYZ", cliente.Nome);
            Assert.Equal("12.345.678/0001-99", cliente.Documento);
            Assert.Equal(TipoPessoa.Juridica, cliente.Tipo);
            Assert.Equal("contato@xyz.com", cliente.Email);
            Assert.Equal("11333333333", cliente.Telefone);
            Assert.Equal(data, cliente.DataCadastro);
            Assert.True(cliente.Ativo);
        }
    }
}