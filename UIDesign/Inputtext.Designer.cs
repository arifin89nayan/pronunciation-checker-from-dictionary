namespace WindowsFormsApp1.UIDesign
{
    partial class Inputtext
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.Back_button = new System.Windows.Forms.Button();
            this.Txt_Input = new System.Windows.Forms.RichTextBox();
            this.StartExractBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_Msg = new System.Windows.Forms.RichTextBox();
            this.btn_FixedList = new System.Windows.Forms.Button();
            this.txt_FixedList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(553, 972);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(488, 48);
            this.label6.TabIndex = 56;
            this.label6.Text = "Message for converting";
            // 
            // Back_button
            // 
            this.Back_button.BackColor = System.Drawing.Color.Red;
            this.Back_button.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Back_button.Location = new System.Drawing.Point(894, 835);
            this.Back_button.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(193, 98);
            this.Back_button.TabIndex = 55;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // Txt_Input
            // 
            this.Txt_Input.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Txt_Input.Location = new System.Drawing.Point(87, 107);
            this.Txt_Input.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Txt_Input.Name = "Txt_Input";
            this.Txt_Input.Size = new System.Drawing.Size(1518, 472);
            this.Txt_Input.TabIndex = 54;
            this.Txt_Input.Text = "";
            // 
            // StartExractBtn
            // 
            this.StartExractBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StartExractBtn.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartExractBtn.ForeColor = System.Drawing.Color.Black;
            this.StartExractBtn.Location = new System.Drawing.Point(488, 835);
            this.StartExractBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.StartExractBtn.Name = "StartExractBtn";
            this.StartExractBtn.Size = new System.Drawing.Size(360, 98);
            this.StartExractBtn.TabIndex = 53;
            this.StartExractBtn.Text = "Start";
            this.StartExractBtn.UseVisualStyleBackColor = false;
            this.StartExractBtn.Click += new System.EventHandler(this.StartExractBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(129, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 48);
            this.label1.TabIndex = 57;
            this.label1.Text = "Input Text";
            // 
            // Txt_Msg
            // 
            this.Txt_Msg.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Txt_Msg.Location = new System.Drawing.Point(74, 1033);
            this.Txt_Msg.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Txt_Msg.Name = "Txt_Msg";
            this.Txt_Msg.Size = new System.Drawing.Size(1518, 371);
            this.Txt_Msg.TabIndex = 58;
            this.Txt_Msg.Text = "";
            // 
            // btn_FixedList
            // 
            this.btn_FixedList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_FixedList.Location = new System.Drawing.Point(1462, 749);
            this.btn_FixedList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_FixedList.Name = "btn_FixedList";
            this.btn_FixedList.Size = new System.Drawing.Size(130, 47);
            this.btn_FixedList.TabIndex = 63;
            this.btn_FixedList.Text = "...";
            this.btn_FixedList.UseVisualStyleBackColor = false;
            this.btn_FixedList.Click += new System.EventHandler(this.btn_FixedList_Click);
            // 
            // txt_FixedList
            // 
            this.txt_FixedList.BackColor = System.Drawing.Color.White;
            this.txt_FixedList.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FixedList.Location = new System.Drawing.Point(84, 688);
            this.txt_FixedList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_FixedList.Name = "txt_FixedList";
            this.txt_FixedList.Size = new System.Drawing.Size(1505, 44);
            this.txt_FixedList.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Orange;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(81, 623);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 55);
            this.label2.TabIndex = 59;
            this.label2.Text = "Select FixedList";
            // 
            // Inputtext
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1814, 1545);
            this.Controls.Add(this.btn_FixedList);
            this.Controls.Add(this.txt_FixedList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Txt_Msg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.Txt_Input);
            this.Controls.Add(this.StartExractBtn);
            this.Name = "Inputtext";
            this.Text = "Inputtext";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.RichTextBox Txt_Input;
        private System.Windows.Forms.Button StartExractBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox Txt_Msg;
        private System.Windows.Forms.Button btn_FixedList;
        private System.Windows.Forms.TextBox txt_FixedList;
        private System.Windows.Forms.Label label2;
    }
}