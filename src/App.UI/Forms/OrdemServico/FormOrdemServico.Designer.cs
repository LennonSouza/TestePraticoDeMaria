namespace App.UI.Forms.OrdemServico
{
    partial class FormOrdemServico
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
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.DataGridView dgvOs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAbertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConclusao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValor;
        private System.Windows.Forms.Panel pnlAcoes;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Panel pnlPaginacao;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.Button btnProxima;

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
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnNova = new System.Windows.Forms.Button();
            this.dgvOs = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAbertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConclusao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnProxima = new System.Windows.Forms.Button();

            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOs)).BeginInit();
            this.pnlAcoes.SuspendLayout();
            this.pnlPaginacao.SuspendLayout();
            this.SuspendLayout();

            // colId
            this.colId.Name = "colId";
            this.colId.HeaderText = "#";
            this.colId.FillWeight = 6F;
            this.colId.Visible = false;

            // colCliente
            this.colCliente.Name = "colCliente";
            this.colCliente.HeaderText = "Cliente";
            this.colCliente.FillWeight = 30F;

            // colAbertura
            this.colAbertura.Name = "colAbertura";
            this.colAbertura.HeaderText = "Abertura";
            this.colAbertura.FillWeight = 12F;

            // colConclusao
            this.colConclusao.Name = "colConclusao";
            this.colConclusao.HeaderText = "Conclusao";
            this.colConclusao.FillWeight = 12F;

            // colStatus
            this.colStatus.Name = "colStatus";
            this.colStatus.HeaderText = "Status";
            this.colStatus.FillWeight = 14F;

            // colValor
            this.colValor.Name = "colValor";
            this.colValor.HeaderText = "Valor total";
            this.colValor.FillWeight = 14F;
            this.colValor.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // pnlFiltros
            this.pnlFiltros.Controls.Add(this.btnNova);
            this.pnlFiltros.Controls.Add(this.btnPesquisar);
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
            this.cmbStatus.Width = 130;

            // btnPesquisar
            this.btnPesquisar.Location = new System.Drawing.Point(726, 9);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Text = "Buscar";
            this.btnPesquisar.Width = 70;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            // btnNova
            this.btnNova.Location = new System.Drawing.Point(804, 9);
            this.btnNova.Name = "btnNova";
            this.btnNova.Text = "+ Nova OS";
            this.btnNova.Width = 85;
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);

            // dgvOs
            this.dgvOs.AllowUserToAddRows = false;
            this.dgvOs.AllowUserToDeleteRows = false;
            this.dgvOs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colCliente, this.colAbertura,
                this.colConclusao, this.colStatus, this.colValor
            });
            this.dgvOs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOs.MultiSelect = false;
            this.dgvOs.Name = "dgvOs";
            this.dgvOs.ReadOnly = true;
            this.dgvOs.RowHeadersVisible = false;
            this.dgvOs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOs_CellDoubleClick);

            // pnlAcoes
            this.pnlAcoes.Controls.Add(this.btnEditar);
            this.pnlAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcoes.Height = 40;
            this.pnlAcoes.Name = "pnlAcoes";
            this.pnlAcoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);

            // btnEditar
            this.btnEditar.Location = new System.Drawing.Point(8, 6);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Text = "Abrir OS";
            this.btnEditar.Width = 80;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // pnlPaginacao
            this.pnlPaginacao.Controls.Add(this.btnProxima);
            this.pnlPaginacao.Controls.Add(this.lblPagina);
            this.pnlPaginacao.Controls.Add(this.btnAnterior);
            this.pnlPaginacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaginacao.Height = 36;
            this.pnlPaginacao.Name = "pnlPaginacao";
            this.pnlPaginacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);

            // btnAnterior
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Location = new System.Drawing.Point(8, 6);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Text = "< Anterior";
            this.btnAnterior.Width = 80;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);

            // lblPagina
            this.lblPagina.AutoSize = true;
            this.lblPagina.Location = new System.Drawing.Point(96, 10);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Text = "Pagina 1";

            // btnProxima
            this.btnProxima.Location = new System.Drawing.Point(170, 6);
            this.btnProxima.Name = "btnProxima";
            this.btnProxima.Text = "Proxima >";
            this.btnProxima.Width = 80;
            this.btnProxima.Click += new System.EventHandler(this.btnProxima_Click);

            // FormOrdemServico
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 520);
            this.Controls.Add(this.dgvOs);
            this.Controls.Add(this.pnlAcoes);
            this.Controls.Add(this.pnlPaginacao);
            this.Controls.Add(this.pnlFiltros);
            this.Name = "FormOrdemServico";
            this.Text = "Ordens de Servico";
            this.Load += new System.EventHandler(this.FormOrdemServico_Load);

            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOs)).EndInit();
            this.pnlAcoes.ResumeLayout(false);
            this.pnlPaginacao.ResumeLayout(false);
            this.pnlPaginacao.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}