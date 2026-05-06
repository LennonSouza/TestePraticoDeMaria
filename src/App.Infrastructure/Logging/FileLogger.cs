using System;
using System.IO;

namespace App.Infrastructure.Logging
{
    public class FileLogger
    {
        private readonly string _logPath;
        private static readonly object _lock = new object();

        public FileLogger(string logPath)
        {
            _logPath = logPath;
            var dir = Path.GetDirectoryName(_logPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public void Info(string mensagem) => Gravar("INFO", mensagem);
        public void Erro(string mensagem, Exception ex = null) => Gravar("ERRO", mensagem, ex);
        public void Aviso(string mensagem) => Gravar("AVISO", mensagem);

        private void Gravar(string nivel, string mensagem, Exception ex = null)
        {
            var linha = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] {mensagem}";
            if (ex != null)
                linha += $"{Environment.NewLine}  Exception: {ex.GetType().Name}: {ex.Message}"
                       + $"{Environment.NewLine}  StackTrace: {ex.StackTrace}";

            lock (_lock) File.AppendAllText(_logPath, linha + Environment.NewLine);
        }
    }
}