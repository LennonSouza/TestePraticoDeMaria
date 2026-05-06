namespace App.UI.Forms.Servicos
{
    partial class FormServicos
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblAtivo;
        private System.Windows.Forms.ComboBox cmbAtivo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.DataGridView dgvServicos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImposto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAtivo;
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
            this.lblAtivo = new System.Windows.Forms.Label();
            this.cmbAtivo = new System.Windows.Forms.ComboBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.dgvServicos = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImposto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAtivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnProxima = new System.Windows.Forms.Button();

            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicos)).BeginInit();
            this.pnlAcoes.SuspendLayout();
            this.pnlPaginacao.SuspendLayout();
            this.SuspendLayout();

            // colId
            this.colId.Name = "colId";
            this.colId.HeaderText = "#";
            this.colId.FillWeight = 5F;
            this.colId.Visible = false;

            // colNome
            this.colNome.Name = "colNome";
            this.colNome.HeaderText = "Nome";
            this.colNome.FillWeight = 45F;

            // colValorBase
            this.colValorBase.Name = "colValorBase";
            this.colValorBase.HeaderText = "Valor base";
            this.colValorBase.FillWeight = 20F;
            this.colValorBase.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // colImposto
            this.colImposto.Name = "colImposto";
            this.colImposto.HeaderText = "% Imposto";
            this.colImposto.FillWeight = 15F;
            this.colImposto.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // colAtivo
            this.colAtivo.Name = "colAtivo";
            this.colAtivo.HeaderText = "Ativo";
            this.colAtivo.FillWeight = 10F;

            // pnlFiltros
            this.pnlFiltros.Controls.Add(this.btnNovo);
            this.pnlFiltros.Controls.Add(this.btnPesquisar);
            this.pnlFiltros.Controls.Add(this.cmbAtivo);
            this.pnlFiltros.Controls.Add(this.lblAtivo);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Height = 50;
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(8, 10, 8, 0);

            // lblAtivo
            this.lblAtivo.AutoSize = true;
            this.lblAtivo.Location = new System.Drawing.Point(8, 13);
            this.lblAtivo.Name = "lblAtivo";
            this.lblAtivo.Text = "Situacao:";

            // cmbAtivo
            this.cmbAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAtivo.Items.AddRange(new object[] { "Todos", "Ativo", "Inativo" });
            this.cmbAtivo.Location = new System.Drawing.Point(70, 10);
            this.cmbAtivo.Name = "cmbAtivo";
            this.cmbAtivo.SelectedIndex = 0;
            this.cmbAtivo.Width = 100;

            // btnPesquisar
            this.btnPesquisar.Location = new System.Drawing.Point(178, 9);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Text = "Buscar";
            this.btnPesquisar.Width = 70;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            // btnNovo
            this.btnNovo.Location = new System.Drawing.Point(256, 9);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Text = "+ Novo";
            this.btnNovo.Width = 70;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);

            // dgvServicos
            this.dgvServicos.AllowUserToAddRows = false;
            this.dgvServicos.AllowUserToDeleteRows = false;
            this.dgvServicos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colNome, this.colValorBase, this.colImposto, this.colAtivo
            });
            this.dgvServicos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServicos.MultiSelect = false;
            this.dgvServicos.Name = "dgvServicos";
            this.dgvServicos.ReadOnly = true;
            this.dgvServicos.RowHeadersVisible = false;
            this.dgvServicos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServicos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServicos_CellDoubleClick);

            // pnlAcoes
            this.pnlAcoes.Controls.Add(this.btnEditar);
            this.pnlAcoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcoes.Height = 40;
            this.pnlAcoes.Name = "pnlAcoes";
            this.pnlAcoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);

            // btnEditar
            this.btnEditar.Location = new System.Drawing.Point(8, 6);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Text = "Editar";
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

            // FormServicos
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 480);
            this.Controls.Add(this.dgvServicos);
            this.Controls.Add(this.pnlAcoes);
            this.Controls.Add(this.pnlPaginacao);
            this.Controls.Add(this.pnlFiltros);
            this.Name = "FormServicos";
            this.Text = "Servicos";
            this.Load += new System.EventHandler(this.FormServicos_Load);

            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicos)).EndInit();
            this.pnlAcoes.ResumeLayout(false);
            this.pnlPaginacao.ResumeLayout(false);
            this.pnlPaginacao.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}