namespace App.UI.Forms.Servicos
{
    partial class FormCadastroServico
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblValorBase;
        private System.Windows.Forms.TextBox txtValorBase;
        private System.Windows.Forms.Label lblPercentualImposto;
        private System.Windows.Forms.TextBox txtPercentualImposto;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblValorBase = new System.Windows.Forms.Label();
            this.txtValorBase = new System.Windows.Forms.TextBox();
            this.lblPercentualImposto = new System.Windows.Forms.Label();
            this.txtPercentualImposto = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblNome
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 23);
            this.lblNome.Name = "lblNome";
            this.lblNome.Text = "Nome *";
            this.lblNome.Width = 110;

            // txtNome
            this.txtNome.Location = new System.Drawing.Point(125, 20);
            this.txtNome.MaxLength = 150;
            this.txtNome.Name = "txtNome";
            this.txtNome.Width = 220;

            // lblValorBase
            this.lblValorBase.AutoSize = true;
            this.lblValorBase.Location = new System.Drawing.Point(12, 55);
            this.lblValorBase.Name = "lblValorBase";
            this.lblValorBase.Text = "Valor base (R$) *";
            this.lblValorBase.Width = 110;

            // txtValorBase
            this.txtValorBase.Location = new System.Drawing.Point(125, 52);
            this.txtValorBase.Name = "txtValorBase";
            this.txtValorBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorBase.Width = 100;

            // lblPercentualImposto
            this.lblPercentualImposto.AutoSize = true;
            this.lblPercentualImposto.Location = new System.Drawing.Point(12, 87);
            this.lblPercentualImposto.Name = "lblPercentualImposto";
            this.lblPercentualImposto.Text = "% Imposto *";
            this.lblPercentualImposto.Width = 110;

            // txtPercentualImposto
            this.txtPercentualImposto.Location = new System.Drawing.Point(125, 84);
            this.txtPercentualImposto.Name = "txtPercentualImposto";
            this.txtPercentualImposto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPercentualImposto.Width = 80;

            // chkAtivo
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(125, 119);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Text = "Servico ativo";

            // btnSalvar
            this.btnSalvar.Location = new System.Drawing.Point(125, 159);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Width = 80;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(213, 159);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Width = 80;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FormCadastroServico
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 209);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblValorBase);
            this.Controls.Add(this.txtValorBase);
            this.Controls.Add(this.lblPercentualImposto);
            this.Controls.Add(this.txtPercentualImposto);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCadastroServico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormCadastroServico_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}