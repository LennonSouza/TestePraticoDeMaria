namespace App.UI.Forms
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem osToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordensDeServicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatoriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatorioToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuario;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.osToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordensDeServicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatoriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatorioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.cadastrosToolStripMenuItem,
                this.osToolStripMenuItem,
                this.relatoriosToolStripMenuItem
            });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1100, 24);

            // cadastrosToolStripMenuItem
            this.cadastrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.clientesToolStripMenuItem,
                this.servicosToolStripMenuItem
            });
            this.cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            this.cadastrosToolStripMenuItem.Text = "Cadastros";

            // clientesToolStripMenuItem
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);

            // servicosToolStripMenuItem
            this.servicosToolStripMenuItem.Name = "servicosToolStripMenuItem";
            this.servicosToolStripMenuItem.Text = "Serviços";
            this.servicosToolStripMenuItem.Click += new System.EventHandler(this.servicosToolStripMenuItem_Click);

            // osToolStripMenuItem
            this.osToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.ordensDeServicoToolStripMenuItem
            });
            this.osToolStripMenuItem.Name = "osToolStripMenuItem";
            this.osToolStripMenuItem.Text = "Ordens de Serviço";

            // ordensDeServicoToolStripMenuItem
            this.ordensDeServicoToolStripMenuItem.Name = "ordensDeServicoToolStripMenuItem";
            this.ordensDeServicoToolStripMenuItem.Text = "Gerenciar OS";
            this.ordensDeServicoToolStripMenuItem.Click += new System.EventHandler(this.ordensDeServicoToolStripMenuItem_Click);

            // relatoriosToolStripMenuItem
            this.relatoriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.relatorioToolStripMenuItem
            });
            this.relatoriosToolStripMenuItem.Name = "relatoriosToolStripMenuItem";
            this.relatoriosToolStripMenuItem.Text = "Relatórios";

            // relatorioToolStripMenuItem
            this.relatorioToolStripMenuItem.Name = "relatorioToolStripMenuItem";
            this.relatorioToolStripMenuItem.Text = "Relatório de OS";
            this.relatorioToolStripMenuItem.Click += new System.EventHandler(this.relatorioToolStripMenuItem_Click);

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblUsuario
            });
            this.statusStrip.Location = new System.Drawing.Point(0, 628);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1100, 22);

            // lblUsuario
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuário: —";

            // FormPrincipal
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormPrincipal";
            this.Text = "Gestão de Ordens de Serviço";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}