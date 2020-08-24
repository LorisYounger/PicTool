namespace PicTool
{
    partial class FrmHelpImage
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
            this.pictureBoxHelpImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxHelpImage
            // 
            this.pictureBoxHelpImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxHelpImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxHelpImage.Name = "pictureBoxHelpImage";
            this.pictureBoxHelpImage.Size = new System.Drawing.Size(450, 300);
            this.pictureBoxHelpImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHelpImage.TabIndex = 0;
            this.pictureBoxHelpImage.TabStop = false;
            this.pictureBoxHelpImage.Click += new System.EventHandler(this.pictureBoxHelpImage_Click);
            // 
            // FrmHelpImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.pictureBoxHelpImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "FrmHelpImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Help Image";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelpImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxHelpImage;
    }
}