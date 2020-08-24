namespace PicTool
{
    partial class FrmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.pictureBoxCG = new System.Windows.Forms.PictureBox();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.labeltPicTool = new System.Windows.Forms.Label();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.linkLabelSteam = new System.Windows.Forms.LinkLabel();
            this.labeltAuther = new System.Windows.Forms.Label();
            this.linkLabelCopyRight = new System.Windows.Forms.LinkLabel();
            this.linkLabelContributor = new System.Windows.Forms.LinkLabel();
            this.labelthanks = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCG
            // 
            this.pictureBoxCG.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCG.Image")));
            this.pictureBoxCG.Location = new System.Drawing.Point(225, 12);
            this.pictureBoxCG.Name = "pictureBoxCG";
            this.pictureBoxCG.Size = new System.Drawing.Size(300, 200);
            this.pictureBoxCG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCG.TabIndex = 0;
            this.pictureBoxCG.TabStop = false;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxIcon.Image")));
            this.pictureBoxIcon.Location = new System.Drawing.Point(12, 19);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 1;
            this.pictureBoxIcon.TabStop = false;
            // 
            // labeltPicTool
            // 
            this.labeltPicTool.AutoSize = true;
            this.labeltPicTool.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltPicTool.Location = new System.Drawing.Point(78, 32);
            this.labeltPicTool.Name = "labeltPicTool";
            this.labeltPicTool.Size = new System.Drawing.Size(115, 36);
            this.labeltPicTool.TabIndex = 2;
            this.labeltPicTool.Text = "PicTool";
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Font = new System.Drawing.Font("Arial", 12F);
            this.linkLabelGithub.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabelGithub.Location = new System.Drawing.Point(9, 175);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(210, 18);
            this.linkLabelGithub.TabIndex = 3;
            this.linkLabelGithub.Text = "GitHub: LorisYounger/PicTool";
            this.linkLabelGithub.VisitedLinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabelGithub.Click += new System.EventHandler(this.linkLabelGithub_Click);
            // 
            // linkLabelSteam
            // 
            this.linkLabelSteam.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabelSteam.AutoSize = true;
            this.linkLabelSteam.Font = new System.Drawing.Font("Arial", 12F);
            this.linkLabelSteam.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabelSteam.Location = new System.Drawing.Point(9, 193);
            this.linkLabelSteam.Name = "linkLabelSteam";
            this.linkLabelSteam.Size = new System.Drawing.Size(211, 18);
            this.linkLabelSteam.TabIndex = 4;
            this.linkLabelSteam.Text = "Steam: app/1381380/PicTool";
            this.linkLabelSteam.VisitedLinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabelSteam.Click += new System.EventHandler(this.linkLabelSteam_Click);
            // 
            // labeltAuther
            // 
            this.labeltAuther.AutoSize = true;
            this.labeltAuther.Location = new System.Drawing.Point(9, 88);
            this.labeltAuther.Name = "labeltAuther";
            this.labeltAuther.Size = new System.Drawing.Size(168, 32);
            this.labeltAuther.TabIndex = 5;
            this.labeltAuther.Text = "Author: LorisYounger\r\nArt Design: 星玲";
            // 
            // linkLabelCopyRight
            // 
            this.linkLabelCopyRight.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabelCopyRight.AutoSize = true;
            this.linkLabelCopyRight.Font = new System.Drawing.Font("Arial", 12F);
            this.linkLabelCopyRight.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabelCopyRight.Location = new System.Drawing.Point(9, 159);
            this.linkLabelCopyRight.Name = "linkLabelCopyRight";
            this.linkLabelCopyRight.Size = new System.Drawing.Size(152, 18);
            this.linkLabelCopyRight.TabIndex = 6;
            this.linkLabelCopyRight.Text = "CopyRight: exLB.org";
            this.linkLabelCopyRight.VisitedLinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabelCopyRight.Click += new System.EventHandler(this.linkLabelCopyRight_Click);
            // 
            // linkLabelContributor
            // 
            this.linkLabelContributor.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabelContributor.AutoSize = true;
            this.linkLabelContributor.Font = new System.Drawing.Font("Arial", 12F);
            this.linkLabelContributor.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabelContributor.Location = new System.Drawing.Point(9, 142);
            this.linkLabelContributor.Name = "linkLabelContributor";
            this.linkLabelContributor.Size = new System.Drawing.Size(133, 18);
            this.linkLabelContributor.TabIndex = 7;
            this.linkLabelContributor.Text = "Contributor:  More";
            this.linkLabelContributor.VisitedLinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabelContributor.Click += new System.EventHandler(this.linkLabelContributor_Click);
            // 
            // labelthanks
            // 
            this.labelthanks.AutoSize = true;
            this.labelthanks.Location = new System.Drawing.Point(9, 120);
            this.labelthanks.Name = "labelthanks";
            this.labelthanks.Size = new System.Drawing.Size(136, 16);
            this.labelthanks.TabIndex = 8;
            this.labelthanks.Text = "Thanks: UserName";
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(534, 221);
            this.Controls.Add(this.labelthanks);
            this.Controls.Add(this.linkLabelContributor);
            this.Controls.Add(this.linkLabelCopyRight);
            this.Controls.Add(this.labeltAuther);
            this.Controls.Add(this.linkLabelSteam);
            this.Controls.Add(this.linkLabelGithub);
            this.Controls.Add(this.labeltPicTool);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.pictureBoxCG);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCG;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label labeltPicTool;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
        private System.Windows.Forms.LinkLabel linkLabelSteam;
        private System.Windows.Forms.Label labeltAuther;
        private System.Windows.Forms.LinkLabel linkLabelCopyRight;
        private System.Windows.Forms.LinkLabel linkLabelContributor;
        private System.Windows.Forms.Label labelthanks;
    }
}