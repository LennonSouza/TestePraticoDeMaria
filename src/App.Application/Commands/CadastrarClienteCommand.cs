namespace App.Application.Commands
{
    public class CadastrarClienteCommand
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public int Tipo { get; set; } // 0 = Física, 1 = Jurídica
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}