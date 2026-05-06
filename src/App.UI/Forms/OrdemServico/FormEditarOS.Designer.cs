namespace App.UI.Forms.OrdemServico
{
    partial class FormEditarOS
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlCabecalho;
        private System.Windows.Forms.Label lblOsId;
        private System.Windows.Forms.Label lblClienteLabel;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblAberturaLabel;
        private System.Windows.Forms.Label lblAbertura;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblValorTotalLabel;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlObservacao;
        private System.Windows.Forms.Label lblObservacaoLabel;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Panel pnlAdicionarItem;
        private System.Windows.Forms.Label lblServico;
        private System.Windows.Forms.ComboBox cmbServico;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Button btnAdicionarItem;
        private System.Windows.Forms.Button btnRemoverItem;
        private System.Windows.Forms.DataGridView dgvItens;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServicoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomeServico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPercentual;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorTotalItem;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Button btnIniciarAndamento;
        private System.Windows.Forms.Button btnConcluir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblHistoricoLabel;
        private System.Windows.Forms.ListBox lstHistorico;
        private System.Windows.Forms.Panel pnlRodape;
        private System.Windows.Forms.Button btnSalvarItens;
        private System.Windows.Forms.Button btnFechar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlCabecalho = new System.Windows.Forms.Panel();
            this.lblOsId = new System.Windows.Forms.Label();
            this.lblClienteLabel = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblAberturaLabel = new System.Windows.Forms.Label();
            this.lblAbertura = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblValorTotalLabel = new System.Windows.Forms.Label();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlObservacao = new System.Windows.Forms.Panel();
            this.lblObservacaoLabel = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.pnlAdicionarItem = new System.Windows.Forms.Panel();
            this.lblServico = new System.Windows.Forms.Label();
            this.cmbServico = new System.Windows.Forms.ComboBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.btnAdicionarItem = new System.Windows.Forms.Button();
            this.btnRemoverItem = new System.Windows.Forms.Button();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.colServicoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNomeServico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPercentual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorTotalItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.btnIniciarAndamento = new System.Windows.Forms.Button();
            this.btnConcluir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblHistoricoLabel = new System.Windows.Forms.Label();
            this.lstHistorico = new System.Windows.Forms.ListBox();
            this.pnlRodape = new System.Windows.Forms.Panel();
            this.btnSalvarItens = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.pnlCabecalho.SuspendLayout();
            this.pnlAdicionarItem.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlRodape.SuspendLayout();
            this.SuspendLayout();

            // colServicoId
            this.colServicoId.Name = "colServicoId";
            this.colServicoId.HeaderText = "ServicoId";
            this.colServicoId.Visible = false;

            // colNomeServico
            this.colNomeServico.Name = "colNomeServico";
            this.colNomeServico.HeaderText = "Servico";
            this.colNomeServico.FillWeight = 35F;

            // colQuantidade
            this.colQuantidade.Name = "colQuantidade";
            this.colQuantidade.HeaderText = "Qtd";
            this.colQuantidade.FillWeight = 8F;
            this.colQuantidade.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // colValorUnitario
            this.colValorUnitario.Name = "colValorUnitario";
            this.colValorUnitario.HeaderText = "Vl. unit.";
            this.colValorUnitario.FillWeight = 18F;
            this.colValorUnitario.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // colPercentual
            this.colPercentual.Name = "colPercentual";
            this.colPercentual.HeaderText = "% Imposto";
            this.colPercentual.FillWeight = 14F;
            this.colPercentual.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // colValorTotalItem
            this.colValorTotalItem.Name = "colValorTotalItem";
            this.colValorTotalItem.HeaderText = "Vl. total";
            this.colValorTotalItem.FillWeight = 18F;
            this.colValorTotalItem.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            // pnlCabecalho
            this.pnlCabecalho.Controls.Add(this.lblValorTotal);
            this.pnlCabecalho.Controls.Add(this.lblValorTotalLabel);
            this.pnlCabecalho.Controls.Add(this.lblStatus);
            this.pnlCabecalho.Controls.Add(this.lblStatusLabel);
            this.pnlCabecalho.Controls.Add(this.lblAbertura);
            this.pnlCabecalho.Controls.Add(this.lblAberturaLabel);
            this.pnlCabecalho.Controls.Add(this.lblCliente);
            this.pnlCabecalho.Controls.Add(this.lblClienteLabel);
            this.pnlCabecalho.Controls.Add(this.lblOsId);
            this.pnlCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecalho.Height = 56;
            this.pnlCabecalho.Name = "pnlCabecalho";
            this.pnlCabecalho.Padding = new System.Windows.Forms.Padding(10, 8, 10, 0);

            // lblOsId
            this.lblOsId.AutoSize = true;
            this.lblOsId.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblOsId.Location = new System.Drawing.Point(10, 8);
            this.lblOsId.Name = "lblOsId";
            this.lblOsId.Text = "OS #";

            // lblClienteLabel
            this.lblClienteLabel.AutoSize = true;
            this.lblClienteLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblClienteLabel.Location = new System.Drawing.Point(130, 8);
            this.lblClienteLabel.Name = "lblClienteLabel";
            this.lblClienteLabel.Text = "Cliente:";

            // lblCliente
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCliente.Location = new System.Drawing.Point(130, 24);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Text = "";

            // lblAberturaLabel
            this.lblAberturaLabel.AutoSize = true;
            this.lblAberturaLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblAberturaLabel.Location = new System.Drawing.Point(330, 8);
            this.lblAberturaLabel.Name = "lblAberturaLabel";
            this.lblAberturaLabel.Text = "Abertura:";

            // lblAbertura
            this.lblAbertura.AutoSize = true;
            this.lblAbertura.Location = new System.Drawing.Point(330, 24);
            this.lblAbertura.Name = "lblAbertura";
            this.lblAbertura.Text = "";

            // lblStatusLabel
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusLabel.Location = new System.Drawing.Point(490, 8);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Text = "Status:";

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(490, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "";

            // lblValorTotalLabel
            this.lblValorTotalLabel.AutoSize = true;
            this.lblValorTotalLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblValorTotalLabel.Location = new System.Drawing.Point(650, 8);
            this.lblValorTotalLabel.Name = "lblValorTotalLabel";
            this.lblValorTotalLabel.Text = "Valor total:";

            // lblValorTotal
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblValorTotal.ForeColor = System.Drawing.Color.FromArgb(59, 109, 17);
            this.lblValorTotal.Location = new System.Drawing.Point(650, 24);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Text = "R$ 0,00";

            // pnlObservacao
            this.pnlObservacao.Controls.Add(this.txtObservacao);
            this.pnlObservacao.Controls.Add(this.lblObservacaoLabel);
            this.pnlObservacao.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlObservacao.Height = 70;
            this.pnlObservacao.Name = "pnlObservacao";
            this.pnlObservacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 4);

            // lblObservacaoLabel
            this.lblObservacaoLabel.AutoSize = true;
            this.lblObservacaoLabel.Location = new System.Drawing.Point(8, 6);
            this.lblObservacaoLabel.Name = "lblObservacaoLabel";
            this.lblObservacaoLabel.Text = "Observacao:";

            // txtObservacao
            this.txtObservacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // pnlAdicionarItem
            this.pnlAdicionarItem.Controls.Add(this.btnRemoverItem);
            this.pnlAdicionarItem.Controls.Add(this.btnAdicionarItem);
            this.pnlAdicionarItem.Controls.Add(this.txtQuantidade);
            this.pnlAdicionarItem.Controls.Add(this.lblQuantidade);
            this.pnlAdicionarItem.Controls.Add(this.cmbServico);
            this.pnlAdicionarItem.Controls.Add(this.lblServico);
            this.pnlAdicionarItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdicionarItem.Height = 42;
            this.pnlAdicionarItem.Name = "pnlAdicionarItem";
            this.pnlAdicionarItem.Padding = new System.Windows.Forms.Padding(8, 8, 8, 0);

            // lblServico
            this.lblServico.AutoSize = true;
            this.lblServico.Location = new System.Drawing.Point(8, 11);
            this.lblServico.Name = "lblServico";
            this.lblServico.Text = "Servico:";

            // cmbServico
            this.cmbServico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServico.Location = new System.Drawing.Point(64, 8);
            this.cmbServico.Name = "cmbServico";
            this.cmbServico.Width = 240;

            // lblQuantidade
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Location = new System.Drawing.Point(312, 11);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Text = "Qtd:";

            // txtQuantidade
            this.txtQuantidade.Location = new System.Drawing.Point(344, 8);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Text = "1";
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantidade.Width = 50;

            // btnAdicionarItem
            this.btnAdicionarItem.Location = new System.Drawing.Point(402, 7);
            this.btnAdicionarItem.Name = "btnAdicionarItem";
            this.btnAdicionarItem.Text = "+ Adicionar";
            this.btnAdicionarItem.Width = 90;
            this.btnAdicionarItem.Click += new System.EventHandler(this.btnAdicionarItem_Click);

            // btnRemoverItem
            this.btnRemoverItem.Location = new System.Drawing.Point(500, 7);
            this.btnRemoverItem.Name = "btnRemoverItem";
            this.btnRemoverItem.Text = "Remover";
            this.btnRemoverItem.Width = 80;
            this.btnRemoverItem.Click += new System.EventHandler(this.btnRemoverItem_Click);

            // dgvItens
            this.dgvItens.AllowUserToAddRows = false;
            this.dgvItens.AllowUserToDeleteRows = false;
            this.dgvItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colServicoId, this.colNomeServico, this.colQuantidade,
                this.colValorUnitario, this.colPercentual, this.colValorTotalItem
            });
            this.dgvItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItens.MultiSelect = false;
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.ReadOnly = true;
            this.dgvItens.RowHeadersVisible = false;
            this.dgvItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // splitMain Panel1
            this.splitMain.Panel1.Controls.Add(this.dgvItens);
            this.splitMain.Panel1.Controls.Add(this.pnlAdicionarItem);
            this.splitMain.Panel1.Controls.Add(this.pnlObservacao);

            // pnlStatus
            this.pnlStatus.Controls.Add(this.btnCancelar);
            this.pnlStatus.Controls.Add(this.btnConcluir);
            this.pnlStatus.Controls.Add(this.btnIniciarAndamento);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatus.Height = 120;
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(8, 8, 8, 0);

            // btnIniciarAndamento
            this.btnIniciarAndamento.Location = new System.Drawing.Point(8, 8);
            this.btnIniciarAndamento.Name = "btnIniciarAndamento";
            this.btnIniciarAndamento.Text = "Iniciar andamento";
            this.btnIniciarAndamento.Width = 150;
            this.btnIniciarAndamento.Height = 30;
            this.btnIniciarAndamento.Click += new System.EventHandler(this.btnIniciarAndamento_Click);

            // btnConcluir
            this.btnConcluir.Location = new System.Drawing.Point(8, 46);
            this.btnConcluir.Name = "btnConcluir";
            this.btnConcluir.Text = "Concluir OS";
            this.btnConcluir.Width = 150;
            this.btnConcluir.Height = 30;
            this.btnConcluir.Click += new System.EventHandler(this.btnConcluir_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(8, 84);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Text = "Cancelar OS";
            this.btnCancelar.Width = 150;
            this.btnCancelar.Height = 30;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelarOS_Click);

            // lblHistoricoLabel
            this.lblHistoricoLabel.AutoSize = true;
            this.lblHistoricoLabel.Location = new System.Drawing.Point(8, 130);
            this.lblHistoricoLabel.Name = "lblHistoricoLabel";
            this.lblHistoricoLabel.Text = "Historico:";

            // lstHistorico
            this.lstHistorico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistorico.Name = "lstHistorico";

            // splitMain Panel2
            this.splitMain.Panel2.Controls.Add(this.lstHistorico);
            this.splitMain.Panel2.Controls.Add(this.lblHistoricoLabel);
            this.splitMain.Panel2.Controls.Add(this.pnlStatus);

            // splitMain
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Name = "splitMain";

            // pnlRodape
            this.pnlRodape.Controls.Add(this.btnFechar);
            this.pnlRodape.Controls.Add(this.btnSalvarItens);
            this.pnlRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRodape.Height = 42;
            this.pnlRodape.Name = "pnlRodape";
            this.pnlRodape.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);

            // btnSalvarItens
            this.btnSalvarItens.Location = new System.Drawing.Point(8, 6);
            this.btnSalvarItens.Name = "btnSalvarItens";
            this.btnSalvarItens.Text = "Salvar itens";
            this.btnSalvarItens.Width = 100;
            this.btnSalvarItens.Click += new System.EventHandler(this.btnSalvarItens_Click);

            // btnFechar
            this.btnFechar.Location = new System.Drawing.Point(116, 6);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Width = 80;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);

            // FormEditarOS
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlRodape);
            this.Controls.Add(this.pnlCabecalho);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "FormEditarOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ordem de Servico";
            this.Load += new System.EventHandler(this.FormEditarOS_Load);

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.pnlCabecalho.ResumeLayout(false);
            this.pnlCabecalho.PerformLayout();
            this.pnlAdicionarItem.ResumeLayout(false);
            this.pnlAdicionarItem.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlRodape.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}