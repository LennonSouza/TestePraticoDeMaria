namespace App.UI.Forms.Clientes
{
    partial class FormClientes
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblAtivo;
        private System.Windows.Forms.ComboBox cmbAtivo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelefone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAtivo;
        private System.Windows.Forms.Panel pnlAcoes;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
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
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblAtivo = new System.Windows.Forms.Label();
            this.cmbAtivo = new System.Windows.Forms.ComboBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelefone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAtivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnProxima = new System.Windows.Forms.Button();

            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
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
            this.colNome.FillWeight = 35F;

            // colDoc
            this.colDoc.Name = "colDoc";
            this.colDoc.HeaderText = "Documento";
            this.colDoc.FillWeight = 20F;

            // colTipo
            this.colTipo.Name = "colTipo";
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.FillWeight = 10F;

            // colTelefone
            this.colTelefone.Name = "colTelefone";
            this.colTelefone.HeaderText = "Telefone";
            this.colTelefone.FillWeight = 15F;

            // colAtivo
            this.colAtivo.Name = "colAtivo";
            this.colAtivo.HeaderText = "Ativo";
            this.colAtivo.FillWeight = 8F;

            // pnlFiltros
            this.pnlFiltros.Controls.Add(this.btnNovo);
            this.pnlFiltros.Controls.Add(this.btnPesquisar);
            this.pnlFiltros.Controls.Add(this.cmbAtivo);
            this.pnlFiltros.Controls.Add(this.lblAtivo);
            this.pnlFiltros.Controls.Add(this.txtDocumento);
            this.pnlFiltros.Controls.Add(this.lblDocumento);
            this.pnlFiltros.Controls.Add(this.txtNome);
            this.pnlFiltros.Controls.Add(this.lblNome);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Height = 50;
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(8, 10, 8, 0);

            // lblNome
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(8, 13);
            this.lblNome.Name = "lblNome";
            this.lblNome.Text = "Nome:";

            // txtNome
            this.txtNome.Location = new System.Drawing.Point(53, 10);
            this.txtNome.Name = "txtNome";
            this.txtNome.Width = 160;
            this.txtNome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNome_KeyDown);

            // lblDocumento
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(221, 13);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Text = "Documento:";

            // txtDocumento
            this.txtDocumento.Location = new System.Drawing.Point(293, 10);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Width = 130;
            this.txtDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumento_KeyDown);

            // lblAtivo
            this.lblAtivo.AutoSize = true;
            this.lblAtivo.Location = new System.Drawing.Point(431, 13);
            this.lblAtivo.Name = "lblAtivo";
            this.lblAtivo.Text = "Situacao:";

            // cmbAtivo
            this.cmbAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAtivo.Items.AddRange(new object[] { "Todos", "Ativo", "Inativo" });
            this.cmbAtivo.Location = new System.Drawing.Point(493, 10);
            this.cmbAtivo.Name = "cmbAtivo";
            this.cmbAtivo.SelectedIndex = 0;
            this.cmbAtivo.Width = 100;

            // btnPesquisar
            this.btnPesquisar.Location = new System.Drawing.Point(601, 9);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Text = "Buscar";
            this.btnPesquisar.Width = 70;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            // btnNovo
            this.btnNovo.Location = new System.Drawing.Point(679, 9);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Text = "+ Novo";
            this.btnNovo.Width = 70;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);

            // dgvClientes
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colNome, this.colDoc,
                this.colTipo, this.colTelefone, this.colAtivo
            });
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellDoubleClick);

            // pnlAcoes
            this.pnlAcoes.Controls.Add(this.btnExcluir);
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

            // btnExcluir
            this.btnExcluir.Location = new System.Drawing.Point(96, 6);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Width = 80;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);

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

            // FormClientes
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 500);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.pnlAcoes);
            this.Controls.Add(this.pnlPaginacao);
            this.Controls.Add(this.pnlFiltros);
            this.Name = "FormClientes";
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.FormClientes_Load);

            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.pnlAcoes.ResumeLayout(false);
            this.pnlPaginacao.ResumeLayout(false);
            this.pnlPaginacao.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}