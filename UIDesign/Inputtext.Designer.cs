namespace WindowsFormsApp1.UIDesign
{
    partial class Inputtext
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblScript = new System.Windows.Forms.Label();
            this.Txt_Input = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_FixedList = new System.Windows.Forms.TextBox();
            this.btn_FixedList = new System.Windows.Forms.Button();
            this.StartExractBtn = new System.Windows.Forms.Button();
            this.Back_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Txt_Msg = new System.Windows.Forms.RichTextBox();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1648, 110);
            this.pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Text = "Input Text";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(188)))));
            this.lblSubtitle.Location = new System.Drawing.Point(44, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Paste Japanese script, choose the fixed list, then start extraction.";
            // 
            // lblScript
            // 
            this.lblScript.AutoSize = true;
            this.lblScript.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblScript.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.lblScript.Location = new System.Drawing.Point(40, 130);
            this.lblScript.Name = "lblScript";
            this.lblScript.Text = "Japanese Script";
            // 
            // Txt_Input
            // 
            this.Txt_Input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Input.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F);
            this.Txt_Input.Location = new System.Drawing.Point(44, 170);
            this.Txt_Input.Name = "Txt_Input";
            this.Txt_Input.Size = new System.Drawing.Size(1560, 300);
            this.Txt_Input.TabIndex = 1;
            this.Txt_Input.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.label2.Location = new System.Drawing.Point(40, 500);
            this.label2.Name = "label2";
            this.label2.Text = "Fixed List (Excel)";
            // 
            // txt_FixedList
            // 
            this.txt_FixedList.BackColor = System.Drawing.Color.White;
            this.txt_FixedList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_FixedList.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txt_FixedList.Location = new System.Drawing.Point(44, 545);
            this.txt_FixedList.Name = "txt_FixedList";
            this.txt_FixedList.Size = new System.Drawing.Size(1410, 39);
            this.txt_FixedList.TabIndex = 2;
            // 
            // btn_FixedList
            // 
            this.btn_FixedList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(229)))), ((int)(((byte)(234)))));
            this.btn_FixedList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FixedList.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_FixedList.ForeColor = System.Drawing.Color.Black;
            this.btn_FixedList.Location = new System.Drawing.Point(1466, 543);
            this.btn_FixedList.Name = "btn_FixedList";
            this.btn_FixedList.Size = new System.Drawing.Size(138, 44);
            this.btn_FixedList.TabIndex = 3;
            this.btn_FixedList.Text = "Browse...";
            this.btn_FixedList.UseVisualStyleBackColor = false;
            this.btn_FixedList.Click += new System.EventHandler(this.btn_FixedList_Click);
            // 
            // StartExractBtn
            // 
            this.StartExractBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StartExractBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartExractBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.StartExractBtn.ForeColor = System.Drawing.Color.Black;
            this.StartExractBtn.Location = new System.Drawing.Point(44, 620);
            this.StartExractBtn.Name = "StartExractBtn";
            this.StartExractBtn.Size = new System.Drawing.Size(380, 80);
            this.StartExractBtn.TabIndex = 4;
            this.StartExractBtn.Text = "Start Extraction";
            this.StartExractBtn.UseVisualStyleBackColor = false;
            this.StartExractBtn.Click += new System.EventHandler(this.StartExractBtn_Click);
            // 
            // Back_button
            // 
            this.Back_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.Back_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_button.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.Back_button.ForeColor = System.Drawing.Color.White;
            this.Back_button.Location = new System.Drawing.Point(1411, 620);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(193, 80);
            this.Back_button.TabIndex = 5;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.label6.Location = new System.Drawing.Point(40, 730);
            this.label6.Name = "label6";
            this.label6.Text = "Processing Log";
            // 
            // Txt_Msg
            // 
            this.Txt_Msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.Txt_Msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Msg.Font = new System.Drawing.Font("Consolas", 12F);
            this.Txt_Msg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(220)))));
            this.Txt_Msg.Location = new System.Drawing.Point(44, 775);
            this.Txt_Msg.Name = "Txt_Msg";
            this.Txt_Msg.ReadOnly = true;
            this.Txt_Msg.Size = new System.Drawing.Size(1560, 360);
            this.Txt_Msg.TabIndex = 6;
            this.Txt_Msg.Text = "";
            // 
            // Inputtext
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1648, 1185);
            this.Controls.Add(this.Txt_Msg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.StartExractBtn);
            this.Controls.Add(this.btn_FixedList);
            this.Controls.Add(this.txt_FixedList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_Input);
            this.Controls.Add(this.lblScript);
            this.Controls.Add(this.pnlHeader);
            this.Name = "Inputtext";
            this.Text = "Input Text";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.RichTextBox Txt_Input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_FixedList;
        private System.Windows.Forms.Button btn_FixedList;
        private System.Windows.Forms.Button StartExractBtn;
        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox Txt_Msg;
    }
}