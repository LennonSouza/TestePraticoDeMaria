using App.Application.Commands;
using System;
using System.Windows.Forms;

namespace App.UI.Forms.Servicos
{
    public partial class FormCadastroServico : Form
    {
        private readonly int? _servicoId;

        public FormCadastroServico()
        {
            InitializeComponent();
            _servicoId = null;
            Text = "Novo Serviço";
        }

        public FormCadastroServico(int servicoId)
        {
            InitializeComponent();
            _servicoId = servicoId;
            Text = "Editar Serviço";
        }

        private void FormCadastroServico_Load(object sender, EventArgs e)
        {
            if (_servicoId.HasValue)
                CarregarServico(_servicoId.Value);
            else
                chkAtivo.Checked = true;
        }

        private void CarregarServico(int id)
        {
            try
            {
                var dto = Program.Services.Servicos.ObterPorId(id);
                if (dto == null)
                {
                    MessageBox.Show("Serviço não encontrado.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtNome.Text = dto.Nome;
                txtValorBase.Text = dto.ValorBase.ToString("N2");
                txtPercentualImposto.Text = dto.PercentualImposto.ToString("N2");
                chkAtivo.Checked = dto.Ativo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar serviço:\n{ex.Message}", "Erro",
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
                if (_servicoId.HasValue)
                    Atualizar();
                else
                    Cadastrar();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar serviço:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cadastrar()
        {
            Program.Services.Servicos.Cadastrar(new CadastrarServicoCommand
            {
                Nome = txtNome.Text.Trim(),
                ValorBase = ParseDecimal(txtValorBase.Text),
                PercentualImposto = ParseDecimal(txtPercentualImposto.Text)
            });
        }

        private void Atualizar()
        {
            Program.Services.Servicos.Atualizar(new AtualizarServicoCommand
            {
                Id = _servicoId.Value,
                Nome = txtNome.Text.Trim(),
                ValorBase = ParseDecimal(txtValorBase.Text),
                PercentualImposto = ParseDecimal(txtPercentualImposto.Text),
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

            if (!decimal.TryParse(txtValorBase.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var valorBase) || valorBase <= 0)
            {
                MessageBox.Show("Valor base deve ser um número maior que zero.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorBase.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPercentualImposto.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var imposto) || imposto < 0 || imposto > 100)
            {
                MessageBox.Show("Percentual de imposto deve estar entre 0 e 100.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPercentualImposto.Focus();
                return false;
            }

            return true;
        }

        private static decimal ParseDecimal(string valor)
        {
            decimal.TryParse(
                valor.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var result
            );
            return result;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}