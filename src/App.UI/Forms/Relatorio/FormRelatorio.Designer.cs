namespace App.UI.Forms.Relatorio
{
    partial class FormRelatorio
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.DateTimePicker dtpDe;
        private System.Windows.Forms.Label lblAte;
        private System.Windows.Forms.DateTimePicker dtpAte;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnExportarPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.lblDe = new System.Windows.Forms.Label();
            this.dtpDe = new System.Windows.Forms.DateTimePicker();
            this.lblAte = new System.Windows.Forms.Label();
            this.dtpAte = new System.Windows.Forms.DateTimePicker();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();

            this.pnlFiltros.SuspendLayout();
            this.SuspendLayout();

            // lblDe
            this.lblDe.AutoSize = true;
            this.lblDe.Location = new System.Drawing.Point(8, 13);
            this.lblDe.Name = "lblDe";
            this.lblDe.Text = "De:";

            // dtpDe
            this.dtpDe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDe.Location = new System.Drawing.Point(34, 10);
            this.dtpDe.Name = "dtpDe";
            this.dtpDe.ShowCheckBox = true;
            this.dtpDe.Checked = false;
            this.dtpDe.Width = 110;

            // lblAte
            this.lblAte.AutoSize = true;
            this.lblAte.Location = new System.Drawing.Point(152, 13);
            this.lblAte.Name = "lblAte";
            this.lblAte.Text = "Ate:";

            // dtpAte
            this.dtpAte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAte.Location = new System.Drawing.Point(182, 10);
            this.dtpAte.Name = "dtpAte";
            this.dtpAte.ShowCheckBox = true;
            this.dtpAte.Checked = false;
            this.dtpAte.Width = 110;

            // lblCliente
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(300, 13);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Text = "Cliente:";

            // cmbCliente
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.Location = new System.Drawing.Point(352, 10);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Width = 180;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(540, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Status:";

            // cmbStatus
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] { "Todos", "Aberta", "Em andamento", "Concluida", "Cancelada" });
            this.cmbStatus.Location = new System.Drawing.Point(588, 10);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.SelectedIndex = 0;
            this.cmbStatus.Width = 120;

            // btnGerar
            this.btnGerar.Location = new System.Drawing.Point(716, 9);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Text = "Gerar";
            this.btnGerar.Width = 70;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);

            // btnExportarPdf
            this.btnExportarPdf.Location = new System.Drawing.Point(794, 9);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.Width = 100;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);

            // pnlFiltros
            this.pnlFiltros.Controls.Add(this.btnExportarPdf);
            this.pnlFiltros.Controls.Add(this.btnGerar);
            this.pnlFiltros.Controls.Add(this.cmbStatus);
            this.pnlFiltros.Controls.Add(this.lblStatus);
            this.pnlFiltros.Controls.Add(this.cmbCliente);
            this.pnlFiltros.Controls.Add(this.lblCliente);
            this.pnlFiltros.Controls.Add(this.dtpAte);
            this.pnlFiltros.Controls.Add(this.lblAte);
            this.pnlFiltros.Controls.Add(this.dtpDe);
            this.pnlFiltros.Controls.Add(this.lblDe);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Height = 50;
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(8, 10, 8, 0);

            // reportViewer
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Name = "reportViewer";

            // FormRelatorio
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.pnlFiltros);
            this.Name = "FormRelatorio";
            this.Text = "Relatorio de Ordens de Servico";
            this.Load += new System.EventHandler(this.FormRelatorio_Load);

            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}