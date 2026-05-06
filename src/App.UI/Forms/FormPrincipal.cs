using App.UI.Forms.Clientes;
using System.Windows.Forms;

namespace App.UI.Forms
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            AtualizarStatusBar();
        }

        private void AtualizarStatusBar()
        {
            lblUsuario.Text = $"Usuário: {Program.Services.UsuarioAtual}";
        }

        private void clientesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AbrirFormFilho<FormClientes>();
        }

        private void servicosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AbrirFormFilho<Forms.Servicos.FormServicos>();
        }

        private void ordensDeServicoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AbrirFormFilho<Forms.OrdemServico.FormOrdemServico>();
        }

        private void relatorioToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AbrirFormFilho<Forms.Relatorio.FormRelatorio>();
        }

        // Garante que só existe uma instância de cada form filha aberta
        private void AbrirFormFilho<T>() where T : Form, new()
        {
            foreach (Form f in MdiChildren)
            {
                if (f is T)
                {
                    f.Activate();
                    return;
                }
            }

            var form = new T();
            form.MdiParent = this;
            form.Show();
        }
    }
}