namespace App.UI.Forms.OrdemServico
{
    partial class FormCadastroOS
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblCliente
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(12, 23);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Text = "Cliente *";
            this.lblCliente.Width = 90;

            // cmbCliente
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.Location = new System.Drawing.Point(105, 20);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Width = 260;

            // lblObservacao
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.Location = new System.Drawing.Point(12, 55);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Text = "Observacao";
            this.lblObservacao.Width = 90;

            // txtObservacao
            this.txtObservacao.Location = new System.Drawing.Point(105, 52);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Width = 260;
            this.txtObservacao.Height = 60;

            // btnSalvar
            this.btnSalvar.Location = new System.Drawing.Point(105, 123);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Text = "Abrir OS";
            this.btnSalvar.Width = 80;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(193, 123);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Width = 80;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FormCadastroOS
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 173);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cmbCliente);
            this.Controls.Add(this.lblObservacao);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCadastroOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nova Ordem de Servico";
            this.Load += new System.EventHandler(this.FormCadastroOS_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}