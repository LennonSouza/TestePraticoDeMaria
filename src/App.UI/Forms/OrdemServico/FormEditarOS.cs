using App.Application.Commands;
using App.Application.DTOs;
using App.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App.UI.Forms.OrdemServico
{
    public partial class FormEditarOS : Form
    {
        private readonly int _osId;
        private OrdemServicoDto _osAtual;

        public FormEditarOS(int osId)
        {
            InitializeComponent();
            _osId = osId;
        }

        private void FormEditarOS_Load(object sender, EventArgs e)
        {
            CarregarServicos();
            CarregarOS();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                splitMain.Panel1MinSize = 200;
                splitMain.Panel2MinSize = 150;
                splitMain.SplitterDistance = (int)(splitMain.Width * 0.65);
            }
            catch { }
        }

        private void CarregarServicos()
        {
            try
            {
                var servicos = Program.Services.Servicos.Listar(
                    ativo: true, pagina: 1, tamanhoPagina: 200
                );

                foreach (var s in servicos)
                    cmbServico.Items.Add(new ComboItemServico(s.Id, s.Nome, s.ValorBase, s.PercentualImposto));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar serviços:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarOS()
        {
            try
            {
                _osAtual = Program.Services.OrdensServico.ObterPorId(_osId);
                if (_osAtual == null)
                {
                    MessageBox.Show("OS não encontrada.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                PreencherCabecalho();
                CarregarItensGrid();
                CarregarHistorico();
                ConfigurarBotoesStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar OS:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreencherCabecalho()
        {
            lblOsId.Text = $"OS # {_osAtual.Id}";
            lblCliente.Text = _osAtual.ClienteNome;
            lblAbertura.Text = _osAtual.DataAbertura.ToString("dd/MM/yyyy HH:mm");
            lblStatus.Text = FormatarStatus(_osAtual.Status);
            txtObservacao.Text = _osAtual.Observacao;
            AtualizarValorTotal();
        }

        private void CarregarItensGrid()
        {
            dgvItens.Rows.Clear();

            foreach (var item in _osAtual.Itens)
            {
                dgvItens.Rows.Add(
                    item.ServicoId,
                    item.ServicoNome,
                    item.Quantidade,
                    item.ValorUnitario.ToString("N2"),
                    item.PercentualImpostoAplicado.ToString("N2"),
                    item.ValorTotalItem.ToString("C2")
                );
            }
        }

        private void CarregarHistorico()
        {
            lstHistorico.Items.Clear();
            lstHistorico.Items.Add($"OS aberta em {_osAtual.DataAbertura:dd/MM/yyyy HH:mm}");
        }

        private void ConfigurarBotoesStatus()
        {
            var editavel = _osAtual.Status == "Aberta" || _osAtual.Status == "EmAndamento";

            btnAdicionarItem.Enabled = editavel;
            btnRemoverItem.Enabled = editavel;
            btnSalvarItens.Enabled = editavel;
            txtObservacao.ReadOnly = !editavel;

            btnIniciarAndamento.Enabled = _osAtual.Status == "Aberta";
            btnConcluir.Enabled = _osAtual.Status == "EmAndamento" || _osAtual.Status == "Aberta";
            btnCancelar.Enabled = editavel;
        }

        private void AtualizarValorTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvItens.Rows)
            {
                if (row.IsNewRow) continue;
                if (decimal.TryParse(
                    row.Cells["colValorTotalItem"].Value?.ToString()
                        .Replace("R$", "").Replace(".", "").Replace(",", ".").Trim(),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var v))
                    total += v;
            }

            lblValorTotal.Text = total.ToString("C2");
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            if (cmbServico.SelectedItem == null)
            {
                MessageBox.Show("Selecione um serviço.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out var qtd) || qtd <= 0)
            {
                MessageBox.Show("Quantidade deve ser maior que zero.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantidade.Focus();
                return;
            }

            var servico = (ComboItemServico)cmbServico.SelectedItem;

            var subtotal = qtd * servico.ValorBase;
            var total = subtotal + subtotal * (servico.PercentualImposto / 100m);

            dgvItens.Rows.Add(
                servico.Id,
                servico.Nome,
                qtd,
                servico.ValorBase.ToString("N2"),
                servico.PercentualImposto.ToString("N2"),
                total.ToString("C2")
            );

            AtualizarValorTotal();
            LimparCamposItem();
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (dgvItens.CurrentRow == null || dgvItens.CurrentRow.IsNewRow) return;

            dgvItens.Rows.Remove(dgvItens.CurrentRow);
            AtualizarValorTotal();
        }

        private void LimparCamposItem()
        {
            cmbServico.SelectedIndex = -1;
            txtQuantidade.Text = "1";
        }

        private void btnSalvarItens_Click(object sender, EventArgs e)
        {
            try
            {
                var cmd = new SalvarItensOsCommand
                {
                    OsId = _osAtual.Id,
                    Versao = _osAtual.Versao,
                    Observacao = txtObservacao.Text.Trim(),
                    Itens = new List<ItemOsCommand>()
                };

                foreach (DataGridViewRow row in dgvItens.Rows)
                {
                    if (row.IsNewRow) continue;

                    cmd.Itens.Add(new ItemOsCommand
                    {
                        ServicoId = (int)row.Cells["colServicoId"].Value,
                        Quantidade = (int)row.Cells["colQuantidade"].Value,
                        ValorUnitario = ParseDecimal(row.Cells["colValorUnitario"].Value?.ToString()),
                        PercentualImposto = ParseDecimal(row.Cells["colPercentual"].Value?.ToString())
                    });
                }

                Program.Services.OrdensServico.SalvarItens(cmd);

                MessageBox.Show("Itens salvos com sucesso.", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarOS();
            }
            catch (ConcorrenciaException ex)
            {
                MessageBox.Show(ex.Message, "Conflito de edição",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CarregarOS();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar itens:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIniciarAndamento_Click(object sender, EventArgs e)
            => MudarStatus(2, "Iniciando andamento");

        private void btnConcluir_Click(object sender, EventArgs e)
            => MudarStatus(3, "Concluída");

        private void btnCancelarOS_Click(object sender, EventArgs e)
            => MudarStatus(4, "Cancelada pelo usuário");

        private void MudarStatus(int novoStatus, string observacao)
        {
            var confirmacao = MessageBox.Show(
                $"Confirma a mudança de status para '{FormatarStatus(novoStatus.ToString())}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacao != DialogResult.Yes) return;

            try
            {
                Program.Services.OrdensServico.MudarStatus(new MudarStatusOsCommand
                {
                    OsId = _osAtual.Id,
                    Versao = _osAtual.Versao,
                    NovoStatus = novoStatus,
                    Observacao = observacao
                });

                CarregarOS();
            }
            catch (ConcorrenciaException ex)
            {
                MessageBox.Show(ex.Message, "Conflito de edição",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CarregarOS();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operação inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao mudar status:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatarStatus(string status)
        {
            switch (status)
            {
                case "Aberta": return "Aberta";
                case "EmAndamento": return "Em andamento";
                case "Concluida": return "Concluída";
                case "Cancelada": return "Cancelada";
                default: return status;
            }
        }

        private static decimal ParseDecimal(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return 0;
            decimal.TryParse(
                valor.Replace("R$", "").Replace(".", "").Replace(",", ".").Trim(),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var result
            );
            return result;
        }

        private void btnFechar_Click(object sender, EventArgs e) => Close();
    }

    public class ComboItemServico
    {
        public int Id { get; }
        public string Nome { get; }
        public decimal ValorBase { get; }
        public decimal PercentualImposto { get; }

        public ComboItemServico(int id, string nome, decimal valorBase, decimal percentualImposto)
        {
            Id = id;
            Nome = nome;
            ValorBase = valorBase;
            PercentualImposto = percentualImposto;
        }

        public override string ToString() => Nome;
    }
}