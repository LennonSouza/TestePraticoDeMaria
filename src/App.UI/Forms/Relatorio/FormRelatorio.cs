using App.Application.DTOs;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace App.UI.Forms.Relatorio
{
    public partial class FormRelatorio : Form
    {
        public FormRelatorio()
        {
            InitializeComponent();
        }

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                cmbCliente.Items.Clear();
                cmbCliente.Items.Add(new ComboItemCliente(0, "Todos os clientes"));

                var clientes = Program.Services.Clientes.Listar(
                    nome: null, documento: null, ativo: null,
                    pagina: 1, tamanhoPagina: 200
                );

                foreach (var c in clientes)
                    cmbCliente.Items.Add(new ComboItemCliente(c.Id, c.Nome));

                cmbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            GerarRelatorio();
        }

        private void GerarRelatorio()
        {
            try
            {
                DateTime? dataInicio = dtpDe.Checked ? dtpDe.Value.Date : (DateTime?)null;
                DateTime? dataFim = dtpAte.Checked ? dtpAte.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                int? clienteId = null;
                if (cmbCliente.SelectedItem is ComboItemCliente item && item.Id > 0)
                    clienteId = item.Id;

                int? status = null;
                if (cmbStatus.SelectedIndex > 0)
                    status = cmbStatus.SelectedIndex;

                var dto = Program.Services.Relatorios.GerarRelatorio(
                    dataInicio, dataFim, clienteId, status
                );

                if (dto.Grupos.Count == 0)
                {
                    MessageBox.Show("Nenhum registro encontrado para os filtros informados.",
                        "Sem resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ExibirNoReportViewer(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar relatório:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExibirNoReportViewer(RelatorioDto dto)
        {
            // Cabecalho
            var tblCabecalho = new DataTable("Cabecalho");
            tblCabecalho.Columns.Add("DataInicio", typeof(string));
            tblCabecalho.Columns.Add("DataFim", typeof(string));
            tblCabecalho.Columns.Add("TotalGeral", typeof(decimal));
            tblCabecalho.Columns.Add("TotalImpostos", typeof(decimal));
            tblCabecalho.Columns.Add("QuantidadeTotalOs", typeof(int));
            tblCabecalho.Columns.Add("DataGeracao", typeof(string));

            tblCabecalho.Rows.Add(
             dto.DataInicio.HasValue ? dto.DataInicio.Value.ToString("dd/MM/yyyy") : "Todos",
             dto.DataFim.HasValue ? dto.DataFim.Value.ToString("dd/MM/yyyy") : "Todos",
             dto.TotalGeral,
             dto.TotalImpostos,
             dto.QuantidadeTotalOs,
             DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            // Grupos
            var tblGrupos = new DataTable("Grupos");
            tblGrupos.Columns.Add("ClienteId", typeof(int));
            tblGrupos.Columns.Add("ClienteNome", typeof(string));
            tblGrupos.Columns.Add("TotalCliente", typeof(decimal));
            tblGrupos.Columns.Add("TotalImpostos", typeof(decimal));
            tblGrupos.Columns.Add("QuantidadeOs", typeof(int));

            foreach (var g in dto.Grupos)
                tblGrupos.Rows.Add(g.ClienteId, g.ClienteNome, g.TotalCliente, g.TotalImpostos, g.QuantidadeOs);

            // Itens
            var tblItens = new DataTable("Itens");
            tblItens.Columns.Add("ClienteId", typeof(int));
            tblItens.Columns.Add("ClienteNome", typeof(string));
            tblItens.Columns.Add("OsId", typeof(string));
            tblItens.Columns.Add("DataAbertura", typeof(string));
            tblItens.Columns.Add("DataConclusao", typeof(string));
            tblItens.Columns.Add("Status", typeof(string));
            tblItens.Columns.Add("ValorTotal", typeof(decimal));
            tblItens.Columns.Add("TotalImpostos", typeof(decimal));

            foreach (var g in dto.Grupos)
                foreach (var i in g.Itens)
                    tblItens.Rows.Add(
                        g.ClienteId,                    // int
                        g.ClienteNome,                  // string
                        $"#{i.OsId}",                   // string
                        i.DataAbertura.ToString("dd/MM/yyyy"),
                        i.DataConclusao.HasValue
                            ? i.DataConclusao.Value.ToString("dd/MM/yyyy")
                            : "—",
                        i.Status,                       // string
                        i.ValorTotal,                   // decimal
                        i.TotalImpostos                 // decimal
                    );

            // Configura ReportViewer
            reportViewer.Reset();
            reportViewer.LocalReport.ReportEmbeddedResource =
                "App.UI.Forms.Relatorio.RelatorioReport.rdlc";

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Cabecalho", tblCabecalho));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Grupos", tblGrupos));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Itens", tblItens));

            reportViewer.LocalReport.Refresh();
            reportViewer.RefreshReport();
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                var bytes = reportViewer.LocalReport.Render("PDF");

                using (var dlg = new SaveFileDialog())
                {
                    dlg.Filter = "PDF|*.pdf";
                    dlg.FileName = $"RelatorioOS_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(dlg.FileName, bytes);
                        MessageBox.Show("PDF exportado com sucesso.", "Exportação",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar PDF:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ComboItemCliente
    {
        public int Id { get; }
        public string Nome { get; }

        public ComboItemCliente(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public override string ToString() => Nome;
    }
}