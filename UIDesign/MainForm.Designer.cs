namespace WindowsFormsApp1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowNav = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHost = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // panelTitle
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Height = 44;
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(30, 39, 97);
            this.panelTitle.Controls.Add(this.lblTitle);
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "  Generative AI-Based TTS Script Generation Agent";
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // flowNav
            this.flowNav.Name = "flowNav";
            this.flowNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowNav.Width = 190;
            this.flowNav.BackColor = System.Drawing.Color.FromArgb(238, 243, 252);
            this.flowNav.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowNav.WrapContents = false;
            this.flowNav.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
           
            // panelHost
            this.panelHost.Name = "panelHost";
            this.panelHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHost.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelHost);
            this.Controls.Add(this.flowNav);
            this.Controls.Add(this.panelTitle);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generative AI-Based TTS Script Generation Agent";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowNav;
        private System.Windows.Forms.Panel panelHost;
    }
}