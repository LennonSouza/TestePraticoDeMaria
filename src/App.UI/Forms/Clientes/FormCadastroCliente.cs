using App.Application.Commands;
using App.Application.DTOs;
using App.Application.Exceptions;
using System;
using System.Windows.Forms;

namespace App.UI.Forms.Clientes
{
    public partial class FormCadastroCliente : Form
    {
        private readonly int? _clienteId;

        // Novo cliente
        public FormCadastroCliente()
        {
            InitializeComponent();
            _clienteId = null;
            Text = "Novo Cliente";
        }

        // Editar cliente existente
        public FormCadastroCliente(int clienteId)
        {
            InitializeComponent();
            _clienteId = clienteId;
            Text = "Editar Cliente";
        }

        private void FormCadastroCliente_Load(object sender, EventArgs e)
        {
            if (_clienteId.HasValue)
                CarregarCliente(_clienteId.Value);
            else
                chkAtivo.Checked = true;
        }

        private void CarregarCliente(int id)
        {
            try
            {
                var dto = Program.Services.Clientes.ObterPorId(id);
                if (dto == null)
                {
                    MessageBox.Show("Cliente não encontrado.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtNome.Text = dto.Nome;
                txtDocumento.Text = dto.Documento;
                txtEmail.Text = dto.Email;
                txtTelefone.Text = dto.Telefone;
                chkAtivo.Checked = dto.Ativo;

                cmbTipo.SelectedIndex = dto.Tipo == "Juridica" ? 1 : 0;

                // Documento não pode ser alterado após cadastro
                txtDocumento.ReadOnly = true;
                cmbTipo.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar cliente:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                if (_clienteId.HasValue)
                    Atualizar();
                else
                    Cadastrar();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (DocumentoDuplicadoException)
            {
                MessageBox.Show(
                    "Já existe um cliente cadastrado com este documento.",
                    "Documento duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar cliente:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cadastrar()
        {
            Program.Services.Clientes.Cadastrar(new CadastrarClienteCommand
            {
                Nome = txtNome.Text.Trim(),
                Documento = txtDocumento.Text.Trim(),
                Tipo = cmbTipo.SelectedIndex,
                Email = txtEmail.Text.Trim(),
                Telefone = txtTelefone.Text.Trim()
            });
        }

        private void Atualizar()
        {
            Program.Services.Clientes.Atualizar(new AtualizarClienteCommand
            {
                Id = _clienteId.Value,
                Nome = txtNome.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefone = txtTelefone.Text.Trim(),
                Ativo = chkAtivo.Checked
            });
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Nome é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if (!_clienteId.HasValue && string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Documento é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (cmbTipo.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione o tipo de pessoa.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipo.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}