using App.Application.DTOs;
using App.UI.Forms.Servicos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App.UI.Forms.Servicos
{
    public partial class FormServicos : Form
    {
        private int _paginaAtual = 1;
        private const int TamanhoPagina = 20;

        public FormServicos()
        {
            InitializeComponent();
        }

        private void FormServicos_Load(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            _paginaAtual = 1;
            Pesquisar();
        }

        private void Pesquisar()
        {
            try
            {
                bool? ativo = null;
                if (cmbAtivo.SelectedIndex == 1) ativo = true;
                if (cmbAtivo.SelectedIndex == 2) ativo = false;

                var servicos = Program.Services.Servicos.Listar(
                    ativo: ativo,
                    pagina: _paginaAtual,
                    tamanhoPagina: TamanhoPagina
                );

                CarregarGrid(servicos);
                AtualizarPaginacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao pesquisar serviços:\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CarregarGrid(IEnumerable<ServicoDto> servicos)
        {
            dgvServicos.Rows.Clear();

            foreach (var s in servicos)
            {
                var idx = dgvServicos.Rows.Add(
                    s.Id,
                    s.Nome,
                    s.ValorBase.ToString("C2"),
                    s.PercentualImposto.ToString("N2") + " %",
                    s.Ativo ? "Sim" : "Não"
                );

                if (!s.Ativo)
                    dgvServicos.Rows[idx].DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        private void AtualizarPaginacao()
        {
            lblPagina.Text = $"Página {_paginaAtual}";
            btnAnterior.Enabled = _paginaAtual > 1;
            btnProxima.Enabled = dgvServicos.Rows.Count == TamanhoPagina;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var form = new FormCadastroServico())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = ObterIdSelecionado();
            if (id == null) return;

            using (var form = new FormCadastroServico(id.Value))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void dgvServicos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            btnEditar_Click(sender, e);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaAtual <= 1) return;
            _paginaAtual--;
            Pesquisar();
        }

        private void btnProxima_Click(object sender, EventArgs e)
        {
            _paginaAtual++;
            Pesquisar();
        }

        private int? ObterIdSelecionado()
        {
            if (dgvServicos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione um serviço na lista.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return null;
            }

            return (int)dgvServicos.CurrentRow.Cells["colId"].Value;
        }
    }
}