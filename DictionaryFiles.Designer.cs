namespace WindowsFormsApp1
{
    partial class DictionaryFiles
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
            this.btn_DicFiles = new System.Windows.Forms.Button();
            this.txt_dicfiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SDataOutPut = new System.Windows.Forms.Button();
            this.txt_OutFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Back_button = new System.Windows.Forms.Button();
            this.txt_DicuserMessage = new System.Windows.Forms.RichTextBox();
            this.Btn_Convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_DicFiles
            // 
            this.btn_DicFiles.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_DicFiles.Location = new System.Drawing.Point(794, 49);
            this.btn_DicFiles.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_DicFiles.Name = "btn_DicFiles";
            this.btn_DicFiles.Size = new System.Drawing.Size(130, 47);
            this.btn_DicFiles.TabIndex = 17;
            this.btn_DicFiles.Text = "...";
            this.btn_DicFiles.UseVisualStyleBackColor = false;
            this.btn_DicFiles.Click += new System.EventHandler(this.btn_DicFiles_Click);
            // 
            // txt_dicfiles
            // 
            this.txt_dicfiles.BackColor = System.Drawing.Color.White;
            this.txt_dicfiles.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dicfiles.Location = new System.Drawing.Point(56, 115);
            this.txt_dicfiles.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_dicfiles.Name = "txt_dicfiles";
            this.txt_dicfiles.Size = new System.Drawing.Size(868, 44);
            this.txt_dicfiles.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LemonChiffon;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(50, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(460, 55);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select Dictionary File";
            // 
            // SDataOutPut
            // 
            this.SDataOutPut.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SDataOutPut.Location = new System.Drawing.Point(794, 190);
            this.SDataOutPut.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.SDataOutPut.Name = "SDataOutPut";
            this.SDataOutPut.Size = new System.Drawing.Size(130, 47);
            this.SDataOutPut.TabIndex = 23;
            this.SDataOutPut.Text = "...";
            this.SDataOutPut.UseVisualStyleBackColor = false;
            this.SDataOutPut.Click += new System.EventHandler(this.SDataOutPut_Click);
            // 
            // txt_OutFilePath
            // 
            this.txt_OutFilePath.BackColor = System.Drawing.Color.White;
            this.txt_OutFilePath.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_OutFilePath.Location = new System.Drawing.Point(56, 256);
            this.txt_OutFilePath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_OutFilePath.Name = "txt_OutFilePath";
            this.txt_OutFilePath.Size = new System.Drawing.Size(868, 44);
            this.txt_OutFilePath.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LemonChiffon;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(50, 195);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(495, 55);
            this.label1.TabIndex = 21;
            this.label1.Text = "Select Output Location";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(201, 456);
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
            this.Back_button.Location = new System.Drawing.Point(597, 329);
            this.Back_button.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(193, 98);
            this.Back_button.TabIndex = 55;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = false;
            this.Back_button.Click += new System.EventHandler(this.Back_button_Click);
            // 
            // txt_DicuserMessage
            // 
            this.txt_DicuserMessage.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_DicuserMessage.Location = new System.Drawing.Point(191, 508);
            this.txt_DicuserMessage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txt_DicuserMessage.Name = "txt_DicuserMessage";
            this.txt_DicuserMessage.Size = new System.Drawing.Size(860, 452);
            this.txt_DicuserMessage.TabIndex = 54;
            this.txt_DicuserMessage.Text = "";
            // 
            // Btn_Convert
            // 
            this.Btn_Convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Btn_Convert.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Convert.ForeColor = System.Drawing.Color.Black;
            this.Btn_Convert.Location = new System.Drawing.Point(191, 329);
            this.Btn_Convert.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Btn_Convert.Name = "Btn_Convert";
            this.Btn_Convert.Size = new System.Drawing.Size(360, 98);
            this.Btn_Convert.TabIndex = 53;
            this.Btn_Convert.Text = "Convert";
            this.Btn_Convert.UseVisualStyleBackColor = false;
            this.Btn_Convert.Click += new System.EventHandler(this.Btn_Convert_ClickAsync);
            // 
            // DictionaryFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 996);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Back_button);
            this.Controls.Add(this.txt_DicuserMessage);
            this.Controls.Add(this.Btn_Convert);
            this.Controls.Add(this.SDataOutPut);
            this.Controls.Add(this.txt_OutFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_DicFiles);
            this.Controls.Add(this.txt_dicfiles);
            this.Controls.Add(this.label2);
            this.Name = "DictionaryFiles";
            this.Text = "DictionaryFiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_DicFiles;
        private System.Windows.Forms.TextBox txt_dicfiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SDataOutPut;
        private System.Windows.Forms.TextBox txt_OutFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.RichTextBox txt_DicuserMessage;
        private System.Windows.Forms.Button Btn_Convert;
    }
}