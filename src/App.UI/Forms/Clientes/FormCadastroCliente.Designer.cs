namespace App.UI.Forms.Clientes
{
    partial class FormCadastroCliente
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
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
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblNome
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 23);
            this.lblNome.Name = "lblNome";
            this.lblNome.Text = "Nome *";
            this.lblNome.Width = 90;

            // txtNome
            this.txtNome.Location = new System.Drawing.Point(105, 20);
            this.txtNome.MaxLength = 150;
            this.txtNome.Name = "txtNome";
            this.txtNome.Width = 250;

            // lblDocumento
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(12, 55);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Text = "Documento *";
            this.lblDocumento.Width = 90;

            // txtDocumento
            this.txtDocumento.Location = new System.Drawing.Point(105, 52);
            this.txtDocumento.MaxLength = 18;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Width = 250;

            // lblTipo
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(12, 87);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Text = "Tipo *";
            this.lblTipo.Width = 90;

            // cmbTipo
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Items.AddRange(new object[] { "Fisica", "Juridica" });
            this.cmbTipo.Location = new System.Drawing.Point(105, 84);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.SelectedIndex = 0;
            this.cmbTipo.Width = 140;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 119);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "E-mail";
            this.lblEmail.Width = 90;

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(105, 116);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Width = 250;

            // lblTelefone
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(12, 151);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Text = "Telefone";
            this.lblTelefone.Width = 90;

            // txtTelefone
            this.txtTelefone.Location = new System.Drawing.Point(105, 148);
            this.txtTelefone.MaxLength = 20;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Width = 130;

            // chkAtivo
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(105, 183);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Text = "Cliente ativo";

            // btnSalvar
            this.btnSalvar.Location = new System.Drawing.Point(105, 223);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Width = 80;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(193, 223);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Width = 80;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FormCadastroCliente
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 290);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCadastroCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormCadastroCliente_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}