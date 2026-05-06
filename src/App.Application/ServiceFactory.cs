using App.Application.Services;
using App.Infrastructure.Data;
using App.Infrastructure.Logging;

namespace App.Application
{
    public class ServiceFactory
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly FileLogger _logger;

        public string UsuarioAtual { get; }

        public ServiceFactory(string connectionString, string logPath, string usuarioAtual)
        {
            _connectionFactory = new ConnectionFactory(connectionString);
            _logger = new FileLogger(logPath);
            UsuarioAtual = usuarioAtual;

            Clientes = new ClienteService(_connectionFactory, _logger, usuarioAtual);
            Servicos = new ServicoService(_connectionFactory, _logger, usuarioAtual);
            OrdensServico = new OrdemServicoService(_connectionFactory, _logger, usuarioAtual);
            Relatorios = new RelatorioService(_connectionFactory, _logger);
        }

        public ClienteService Clientes { get; }
        public ServicoService Servicos { get; }
        public OrdemServicoService OrdensServico { get; }
        public RelatorioService Relatorios { get; }
    }
}