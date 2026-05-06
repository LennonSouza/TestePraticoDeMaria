using App.Application.DTOs;
using App.UI.Forms.Clientes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App.UI.Forms.Clientes
{
    public partial class FormClientes : Form
    {
        private int _paginaAtual = 1;
        private const int TamanhoPagina = 20;

        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            _paginaAtual = 1;
            Pesquisar();
        }

        private void txtNome_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                btnPesquisar_Click(sender, e);
        }

        private void txtDocumento_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                btnPesquisar_Click(sender, e);
        }

        private void Pesquisar()
        {
            try
            {
                bool? ativo = null;
                if (cmbAtivo.SelectedIndex == 1) ativo = true;
                if (cmbAtivo.SelectedIndex == 2) ativo = false;

                var clientes = Program.Services.Clientes.Listar(
                    nome: txtNome.Text.Trim(),
                    documento: txtDocumento.Text.Trim(),
                    ativo: ativo,
                    pagina: _paginaAtual,
                    tamanhoPagina: TamanhoPagina
                );

                CarregarGrid(clientes);
                AtualizarPaginacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao pesquisar clientes:\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CarregarGrid(IEnumerable<ClienteDto> clientes)
        {
            dgvClientes.Rows.Clear();

            foreach (var c in clientes)
            {
                var idx = dgvClientes.Rows.Add(
                    c.Id,
                    c.Nome,
                    c.Documento,
                    c.Tipo,
                    c.Telefone ?? "—",
                    c.Ativo ? "Sim" : "Não"
                );

                // Destaca linhas de clientes inativos
                if (!c.Ativo)
                    dgvClientes.Rows[idx].DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        private void AtualizarPaginacao()
        {
            lblPagina.Text = $"Página {_paginaAtual}";
            btnAnterior.Enabled = _paginaAtual > 1;
            // Habilita próxima se grid estiver cheio (pode ter mais)
            btnProxima.Enabled = dgvClientes.Rows.Count == TamanhoPagina;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var form = new FormCadastroCliente())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = ObterIdSelecionado();
            if (id == null) return;

            using (var form = new FormCadastroCliente(id.Value))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            btnEditar_Click(sender, e);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var id = ObterIdSelecionado();
            if (id == null) return;

            try
            {
                if (Program.Services.Clientes.PossuiOsVinculada(id.Value))
                {
                    MessageBox.Show(
                        "Não é possível excluir este cliente pois existem Ordens de Serviço vinculadas.\n" +
                        "Para desativá-lo, use a opção Editar.",
                        "Exclusão bloqueada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                var confirmacao = MessageBox.Show(
                    "Deseja realmente excluir este cliente?",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacao != DialogResult.Yes) return;

                Program.Services.Clientes.Excluir(id.Value);
                Pesquisar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir cliente:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione um cliente na lista.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return null;
            }

            return (int)dgvClientes.CurrentRow.Cells["colId"].Value;
        }
    }
}