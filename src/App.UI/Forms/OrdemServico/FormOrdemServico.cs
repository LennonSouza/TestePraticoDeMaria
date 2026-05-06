using App.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace App.UI.Forms.OrdemServico
{
    public partial class FormOrdemServico : Form
    {
        private int _paginaAtual = 1;
        private const int TamanhoPagina = 20;

        public FormOrdemServico()
        {
            InitializeComponent();
        }

        private void FormOrdemServico_Load(object sender, EventArgs e)
        {
            CarregarClientes();
            Pesquisar();
        }

        private void CarregarClientes()
        {
            try
            {
                cmbCliente.Items.Clear();
                cmbCliente.Items.Add(new ComboItem(0, "Todos os clientes"));

                var clientes = Program.Services.Clientes.Listar(
                    nome: null, documento: null, ativo: true,
                    pagina: 1, tamanhoPagina: 200
                );

                foreach (var c in clientes)
                    cmbCliente.Items.Add(new ComboItem(c.Id, c.Nome));

                cmbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                DateTime? dataInicio = dtpDe.Checked ? dtpDe.Value.Date : (DateTime?)null;
                DateTime? dataFim = dtpAte.Checked ? dtpAte.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                int? clienteId = null;
                if (cmbCliente.SelectedItem is ComboItem item && item.Id > 0)
                    clienteId = item.Id;

                int? status = null;
                if (cmbStatus.SelectedIndex > 0)
                    status = cmbStatus.SelectedIndex;

                var lista = Program.Services.OrdensServico.Listar(
                    dataInicio: dataInicio,
                    dataFim: dataFim,
                    clienteId: clienteId,
                    status: status,
                    pagina: _paginaAtual,
                    tamanhoPagina: TamanhoPagina
                );

                CarregarGrid(lista);
                AtualizarPaginacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pesquisar OS:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarGrid(IEnumerable<OrdemServicoDto> lista)
        {
            dgvOs.Rows.Clear();

            foreach (var os in lista)
            {
                var idx = dgvOs.Rows.Add(
                    os.Id,
                    os.ClienteNome,
                    os.DataAbertura.ToString("dd/MM/yyyy"),
                    os.DataConclusao.HasValue ? os.DataConclusao.Value.ToString("dd/MM/yyyy") : "—",
                    os.Status,
                    os.ValorTotal.ToString("C2")
                );

                var row = dgvOs.Rows[idx];
                AplicarCorStatus(row, os.Status);
            }
        }

        private void AplicarCorStatus(DataGridViewRow row, string status)
        {
            switch (status)
            {
                case "Aberta":
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(24, 95, 165);
                    break;
                case "EmAndamento":
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(133, 79, 11);
                    break;
                case "Concluida":
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(59, 109, 17);
                    break;
                case "Cancelada":
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(163, 45, 45);
                    break;
            }
        }

        private void AtualizarPaginacao()
        {
            lblPagina.Text = $"Página {_paginaAtual}";
            btnAnterior.Enabled = _paginaAtual > 1;
            btnProxima.Enabled = dgvOs.Rows.Count == TamanhoPagina;
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            using (var form = new FormCadastroOS())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = ObterIdSelecionado();
            if (id == null) return;

            using (var form = new FormEditarOS(id.Value))
            {
                form.ShowDialog();
                Pesquisar();
            }
        }

        private void dgvOs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
            if (dgvOs.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma OS na lista.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return (int)dgvOs.CurrentRow.Cells["colId"].Value;
        }
    }

    // Classe auxiliar para o ComboBox de clientes
    public class ComboItem
    {
        public int Id { get; }
        public string Nome { get; }

        public ComboItem(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public override string ToString() => Nome;
    }
}