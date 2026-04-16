using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class CaptionForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtToImage = new System.Windows.Forms.RichTextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.AudioDuration = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.txtTa = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtT3 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxTextToImagePreview = new System.Windows.Forms.PictureBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblmessage = new System.Windows.Forms.Label();
            this.lblAudioDuration = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.MsgShow = new System.Windows.Forms.RichTextBox();
            this.ClearMessege = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.AudioLocation = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextToImagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.Location = new System.Drawing.Point(87, 501);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.LightGray;
            this.btnNext.Location = new System.Drawing.Point(529, 501);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 28);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Finished";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Content Text";
            // 
            // TxtToImage
            // 
            this.TxtToImage.Location = new System.Drawing.Point(19, 49);
            this.TxtToImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtToImage.Name = "TxtToImage";
            this.TxtToImage.Size = new System.Drawing.Size(611, 164);
            this.TxtToImage.TabIndex = 3;
            this.TxtToImage.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(851, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Caption Image Show";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 426);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "Messege";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(853, 495);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(60, 28);
            this.button7.TabIndex = 35;
            this.button7.Text = "Play";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.OnBtnPlay_Click);
            // 
            // AudioDuration
            // 
            this.AudioDuration.AutoSize = true;
            this.AudioDuration.Location = new System.Drawing.Point(677, 501);
            this.AudioDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AudioDuration.Name = "AudioDuration";
            this.AudioDuration.Size = new System.Drawing.Size(38, 16);
            this.AudioDuration.TabIndex = 36;
            this.AudioDuration.Text = "Time";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 298);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "T1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 299);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "Ta";
            // 
            // txtT1
            // 
            this.txtT1.Location = new System.Drawing.Point(61, 295);
            this.txtT1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtT1.Name = "txtT1";
            this.txtT1.Size = new System.Drawing.Size(141, 22);
            this.txtT1.TabIndex = 39;
            // 
            // txtTa
            // 
            this.txtTa.Location = new System.Drawing.Point(291, 295);
            this.txtTa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTa.Name = "txtTa";
            this.txtTa.Size = new System.Drawing.Size(141, 22);
            this.txtTa.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(448, 299);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "T3";
            // 
            // txtT3
            // 
            this.txtT3.Location = new System.Drawing.Point(487, 297);
            this.txtT3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(143, 22);
            this.txtT3.TabIndex = 43;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBoxTextToImagePreview);
            this.panel1.Location = new System.Drawing.Point(645, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 464);
            this.panel1.TabIndex = 44;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBoxTextToImagePreview
            // 
            this.pictureBoxTextToImagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxTextToImagePreview.Location = new System.Drawing.Point(12, 6);
            this.pictureBoxTextToImagePreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxTextToImagePreview.Name = "pictureBoxTextToImagePreview";
            this.pictureBoxTextToImagePreview.Size = new System.Drawing.Size(250, 360);
            this.pictureBoxTextToImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxTextToImagePreview.TabIndex = 0;
            this.pictureBoxTextToImagePreview.TabStop = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.LightGray;
            this.btnGenerate.Location = new System.Drawing.Point(413, 501);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 28);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click_V2);
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Location = new System.Drawing.Point(149, 426);
            this.lblmessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(0, 16);
            this.lblmessage.TabIndex = 32;
            // 
            // lblAudioDuration
            // 
            this.lblAudioDuration.AutoSize = true;
            this.lblAudioDuration.Location = new System.Drawing.Point(139, 393);
            this.lblAudioDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAudioDuration.Name = "lblAudioDuration";
            this.lblAudioDuration.Size = new System.Drawing.Size(0, 16);
            this.lblAudioDuration.TabIndex = 41;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(924, 495);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(60, 28);
            this.btnPause.TabIndex = 35;
            this.btnPause.Text = "Stop";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.OnPlaybackStopped);
            // 
            // MsgShow
            // 
            this.MsgShow.Location = new System.Drawing.Point(87, 406);
            this.MsgShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MsgShow.Name = "MsgShow";
            this.MsgShow.Size = new System.Drawing.Size(295, 61);
            this.MsgShow.TabIndex = 46;
            this.MsgShow.Text = "";
            // 
            // ClearMessege
            // 
            this.ClearMessege.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClearMessege.Location = new System.Drawing.Point(413, 426);
            this.ClearMessege.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClearMessege.Name = "ClearMessege";
            this.ClearMessege.Size = new System.Drawing.Size(100, 28);
            this.ClearMessege.TabIndex = 47;
            this.ClearMessege.Text = "Clear";
            this.ClearMessege.UseVisualStyleBackColor = false;
            this.ClearMessege.Click += new System.EventHandler(this.ClearMessege_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(19, 341);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "Audio Location";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(307, 335);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 28);
            this.button2.TabIndex = 49;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AudioLocation
            // 
            this.AudioLocation.Location = new System.Drawing.Point(131, 337);
            this.AudioLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AudioLocation.Name = "AudioLocation";
            this.AudioLocation.Size = new System.Drawing.Size(167, 22);
            this.AudioLocation.TabIndex = 50;
            // 
            // CaptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.AudioLocation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClearMessege);
            this.Controls.Add(this.MsgShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtT3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblAudioDuration);
            this.Controls.Add(this.txtTa);
            this.Controls.Add(this.txtT1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AudioDuration);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.lblmessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtToImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CaptionForm";
            this.Text = "CaptionForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextToImagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox TxtToImage;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label AudioDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtT1;
        private System.Windows.Forms.TextBox txtTa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtT3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxTextToImagePreview;
        private System.Windows.Forms.Button btnGenerate;
        private Label lblmessage;
        private Label lblAudioDuration;
        private Button btnPause;
        private RichTextBox MsgShow;
        private Button ClearMessege;
        private Label label2;
        private Button button2;
        private TextBox AudioLocation;
    }
}