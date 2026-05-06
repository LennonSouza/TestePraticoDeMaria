namespace App.Application.Commands
{
    public class MudarStatusOsCommand
    {
        public int OsId { get; set; }
        public int Versao { get; set; }
        public int NovoStatus { get; set; }
        public string Observacao { get; set; }
    }
}