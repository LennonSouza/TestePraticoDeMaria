using System;
using System.Windows.Forms;

namespace App.UI.Forms.OrdemServico
{
    public partial class FormCadastroOS : Form
    {
        public FormCadastroOS()
        {
            InitializeComponent();
        }

        private void FormCadastroOS_Load(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                cmbCliente.Items.Clear();

                var clientes = Program.Services.Clientes.Listar(
                    nome: null, documento: null, ativo: true,
                    pagina: 1, tamanhoPagina: 200
                );

                foreach (var c in clientes)
                    cmbCliente.Items.Add(new ComboItem(c.Id, c.Nome));

                if (cmbCliente.Items.Count > 0)
                    cmbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.Focus();
                return;
            }

            try
            {
                var clienteId = ((ComboItem)cmbCliente.SelectedItem).Id;

                Program.Services.OrdensServico.Abrir(
                    clienteId: clienteId,
                    observacao: txtObservacao.Text.Trim()
                );

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir OS:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}