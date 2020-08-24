namespace PicTool
{
    partial class FrmDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDebug));
            this.pictureBoxQCry = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.checkBoxUpload = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQCry)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxQCry
            // 
            this.pictureBoxQCry.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxQCry.Image")));
            this.pictureBoxQCry.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxQCry.Name = "pictureBoxQCry";
            this.pictureBoxQCry.Size = new System.Drawing.Size(200, 300);
            this.pictureBoxQCry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxQCry.TabIndex = 0;
            this.pictureBoxQCry.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 72);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sorry, a fatal error occurred in the PicTool.\r\nThe processed picture data may be " +
    "lost.\r\n\r\nLog:";
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.Font = new System.Drawing.Font("Arial", 9F);
            this.textBoxLog.Location = new System.Drawing.Point(221, 87);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(351, 157);
            this.textBoxLog.TabIndex = 2;
            this.textBoxLog.TabStop = false;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Font = new System.Drawing.Font("Arial", 14F);
            this.buttonClose.Location = new System.Drawing.Point(221, 278);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(170, 34);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "CLOSE";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonRestart
            // 
            this.buttonRestart.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.buttonRestart.Font = new System.Drawing.Font("Arial", 14F);
            this.buttonRestart.Location = new System.Drawing.Point(402, 278);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(170, 34);
            this.buttonRestart.TabIndex = 4;
            this.buttonRestart.TabStop = false;
            this.buttonRestart.Text = "RESTART";
            this.buttonRestart.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpload
            // 
            this.checkBoxUpload.AutoSize = true;
            this.checkBoxUpload.Checked = true;
            this.checkBoxUpload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpload.Location = new System.Drawing.Point(221, 250);
            this.checkBoxUpload.Name = "checkBoxUpload";
            this.checkBoxUpload.Size = new System.Drawing.Size(278, 22);
            this.checkBoxUpload.TabIndex = 5;
            this.checkBoxUpload.TabStop = false;
            this.checkBoxUpload.Text = "Upload errors for programmers to fix";
            this.checkBoxUpload.UseVisualStyleBackColor = true;
            // 
            // FrmDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 326);
            this.Controls.Add(this.checkBoxUpload);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxQCry);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDebug";
            this.Text = "Fatal Error Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDebug_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQCry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxQCry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.CheckBox checkBoxUpload;
    }
}