using App.Application;
using App.UI.Forms;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace App.UI
{
    static class Program
    {
        public static ServiceFactory Services { get; private set; }

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Captura exceções não tratadas para ver o erro real
            System.Windows.Forms.Application.ThreadException += (s, e) =>
                MessageBox.Show(e.Exception.ToString(), "Erro detalhado");

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                MessageBox.Show(e.ExceptionObject.ToString(), "Erro não tratado");

            try
            {
                var connectionString = ConfigurationManager
                    .ConnectionStrings["PostgresConnection"]
                    .ConnectionString;

                var logPath = ConfigurationManager.AppSettings["LogPath"]
                    ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "app.log");

                var usuario = ConfigurationManager.AppSettings["UsuarioAtual"] ?? "sistema";

                Services = new ServiceFactory(connectionString, logPath, usuario);

                System.Windows.Forms.Application.Run(new FormPrincipal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao iniciar a aplicação:\n{ex.Message}",
                    "Erro de inicialização",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}