namespace PicTool
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.sourcePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlToolChose = new System.Windows.Forms.TabControl();
            this.tabPageCompatible = new System.Windows.Forms.TabPage();
            this.numericUpDownCMBTimes = new System.Windows.Forms.NumericUpDown();
            this.buttonCMBUniversalColor = new System.Windows.Forms.Button();
            this.labeltCMB = new System.Windows.Forms.Label();
            this.buttonCMBStart = new System.Windows.Forms.Button();
            this.textBoxCMBColor = new System.Windows.Forms.TextBox();
            this.tabPageGrayscale = new System.Windows.Forms.TabPage();
            this.tabPageCommon = new System.Windows.Forms.TabPage();
            this.tabPageTextImg = new System.Windows.Forms.TabPage();
            this.tabPageOther = new System.Windows.Forms.TabPage();
            this.tabPageWorkShop = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxBefore = new System.Windows.Forms.PictureBox();
            this.pictureBoxAfter = new System.Windows.Forms.PictureBox();
            this.buttonChooseBefore = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonExportjpg = new System.Windows.Forms.Button();
            this.buttonExportPng = new System.Windows.Forms.Button();
            this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
            this.textBoxToolTips = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exportAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsJPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureBoxWait = new System.Windows.Forms.PictureBox();
            this.progressBarWait = new System.Windows.Forms.ProgressBar();
            this.menuStrip.SuspendLayout();
            this.tabControlToolChose.SuspendLayout();
            this.tabPageCompatible.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCMBTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourcePictureToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.batchProcessingToolStripMenuItem,
            this.dlcToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // sourcePictureToolStripMenuItem
            // 
            this.sourcePictureToolStripMenuItem.Name = "sourcePictureToolStripMenuItem";
            this.sourcePictureToolStripMenuItem.Size = new System.Drawing.Size(95, 21);
            this.sourcePictureToolStripMenuItem.Text = "Open Picture";
            this.sourcePictureToolStripMenuItem.Click += new System.EventHandler(this.sourcePictureToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportAsPNGToolStripMenuItem,
            this.exportAsJPGToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // batchProcessingToolStripMenuItem
            // 
            this.batchProcessingToolStripMenuItem.ForeColor = System.Drawing.Color.DimGray;
            this.batchProcessingToolStripMenuItem.Name = "batchProcessingToolStripMenuItem";
            this.batchProcessingToolStripMenuItem.Size = new System.Drawing.Size(115, 21);
            this.batchProcessingToolStripMenuItem.Text = "BatchProcessing";
            this.batchProcessingToolStripMenuItem.Click += new System.EventHandler(this.batchProcessingToolStripMenuItem_Click);
            // 
            // dlcToolStripMenuItem
            // 
            this.dlcToolStripMenuItem.Name = "dlcToolStripMenuItem";
            this.dlcToolStripMenuItem.Size = new System.Drawing.Size(104, 21);
            this.dlcToolStripMenuItem.Text = "DLC - ArtBook";
            this.dlcToolStripMenuItem.Visible = false;
            this.dlcToolStripMenuItem.Click += new System.EventHandler(this.dlcToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.languageToolStripMenuItem.Text = "Language";
            this.languageToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // tabControlToolChose
            // 
            this.tabControlToolChose.Controls.Add(this.tabPageCompatible);
            this.tabControlToolChose.Controls.Add(this.tabPageGrayscale);
            this.tabControlToolChose.Controls.Add(this.tabPageCommon);
            this.tabControlToolChose.Controls.Add(this.tabPageTextImg);
            this.tabControlToolChose.Controls.Add(this.tabPageOther);
            this.tabControlToolChose.Controls.Add(this.tabPageWorkShop);
            this.tabControlToolChose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlToolChose.Location = new System.Drawing.Point(0, 0);
            this.tabControlToolChose.Name = "tabControlToolChose";
            this.tabControlToolChose.SelectedIndex = 0;
            this.tabControlToolChose.Size = new System.Drawing.Size(557, 198);
            this.tabControlToolChose.TabIndex = 1;
            this.tabControlToolChose.TabStop = false;
            // 
            // tabPageCompatible
            // 
            this.tabPageCompatible.Controls.Add(this.numericUpDownCMBTimes);
            this.tabPageCompatible.Controls.Add(this.buttonCMBUniversalColor);
            this.tabPageCompatible.Controls.Add(this.labeltCMB);
            this.tabPageCompatible.Controls.Add(this.buttonCMBStart);
            this.tabPageCompatible.Controls.Add(this.textBoxCMBColor);
            this.tabPageCompatible.Location = new System.Drawing.Point(4, 26);
            this.tabPageCompatible.Name = "tabPageCompatible";
            this.tabPageCompatible.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCompatible.Size = new System.Drawing.Size(549, 168);
            this.tabPageCompatible.TabIndex = 0;
            this.tabPageCompatible.Text = "颜色兼容";
            this.toolTip1.SetToolTip(this.tabPageCompatible, "图片兼容是将图片中所有转换成指定颜色\r\n用于减少图片中颜色数量");
            this.tabPageCompatible.UseVisualStyleBackColor = true;
            // 
            // numericUpDownCMBTimes
            // 
            this.numericUpDownCMBTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownCMBTimes.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownCMBTimes.Location = new System.Drawing.Point(156, 3);
            this.numericUpDownCMBTimes.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDownCMBTimes.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownCMBTimes.Name = "numericUpDownCMBTimes";
            this.numericUpDownCMBTimes.Size = new System.Drawing.Size(36, 26);
            this.numericUpDownCMBTimes.TabIndex = 14;
            this.numericUpDownCMBTimes.TabStop = false;
            this.toolTip1.SetToolTip(this.numericUpDownCMBTimes, "通用颜色之间的间隔\r\n数值越大间隔越大");
            this.numericUpDownCMBTimes.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // buttonCMBUniversalColor
            // 
            this.buttonCMBUniversalColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCMBUniversalColor.Font = new System.Drawing.Font("宋体", 11F);
            this.buttonCMBUniversalColor.Location = new System.Drawing.Point(5, 3);
            this.buttonCMBUniversalColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCMBUniversalColor.Name = "buttonCMBUniversalColor";
            this.buttonCMBUniversalColor.Size = new System.Drawing.Size(146, 25);
            this.buttonCMBUniversalColor.TabIndex = 13;
            this.buttonCMBUniversalColor.TabStop = false;
            this.buttonCMBUniversalColor.Text = "生成通用颜色";
            this.toolTip1.SetToolTip(this.buttonCMBUniversalColor, "生成通用的颜色集\r\n修改旁边的数字决定间隔");
            this.buttonCMBUniversalColor.UseVisualStyleBackColor = true;
            this.buttonCMBUniversalColor.Click += new System.EventHandler(this.buttonCMBUniversalColor_Click);
            // 
            // labeltCMB
            // 
            this.labeltCMB.AutoSize = true;
            this.labeltCMB.Location = new System.Drawing.Point(8, 30);
            this.labeltCMB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labeltCMB.Name = "labeltCMB";
            this.labeltCMB.Size = new System.Drawing.Size(104, 16);
            this.labeltCMB.TabIndex = 9;
            this.labeltCMB.Text = "要兼容的颜色";
            // 
            // buttonCMBStart
            // 
            this.buttonCMBStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCMBStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCMBStart.Location = new System.Drawing.Point(208, 6);
            this.buttonCMBStart.Name = "buttonCMBStart";
            this.buttonCMBStart.Size = new System.Drawing.Size(182, 37);
            this.buttonCMBStart.TabIndex = 12;
            this.buttonCMBStart.TabStop = false;
            this.buttonCMBStart.Text = "开始兼容";
            this.toolTip1.SetToolTip(this.buttonCMBStart, "将图片里的所有颜色近似转换为指定的颜色集");
            this.buttonCMBStart.UseVisualStyleBackColor = false;
            this.buttonCMBStart.Click += new System.EventHandler(this.buttonCMBStart_Click);
            // 
            // textBoxCMBColor
            // 
            this.textBoxCMBColor.Location = new System.Drawing.Point(7, 48);
            this.textBoxCMBColor.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCMBColor.Multiline = true;
            this.textBoxCMBColor.Name = "textBoxCMBColor";
            this.textBoxCMBColor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCMBColor.Size = new System.Drawing.Size(383, 115);
            this.textBoxCMBColor.TabIndex = 11;
            this.textBoxCMBColor.TabStop = false;
            this.textBoxCMBColor.Text = "#000;#008;#00f;#080;#088;#08f;#0ff;#8ff;#fff";
            this.toolTip1.SetToolTip(this.textBoxCMBColor, "要兼容的颜色\r\n会将图片里的所有颜色近似转换为这个文本框所写的颜色集");
            this.textBoxCMBColor.DoubleClick += new System.EventHandler(this.textBoxCMBColor_DoubleClick);
            // 
            // tabPageGrayscale
            // 
            this.tabPageGrayscale.Location = new System.Drawing.Point(4, 26);
            this.tabPageGrayscale.Name = "tabPageGrayscale";
            this.tabPageGrayscale.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGrayscale.Size = new System.Drawing.Size(549, 168);
            this.tabPageGrayscale.TabIndex = 1;
            this.tabPageGrayscale.Text = "灰度";
            this.tabPageGrayscale.UseVisualStyleBackColor = true;
            // 
            // tabPageCommon
            // 
            this.tabPageCommon.Location = new System.Drawing.Point(4, 26);
            this.tabPageCommon.Name = "tabPageCommon";
            this.tabPageCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommon.Size = new System.Drawing.Size(549, 168);
            this.tabPageCommon.TabIndex = 2;
            this.tabPageCommon.Text = "常用效果";
            this.tabPageCommon.UseVisualStyleBackColor = true;
            // 
            // tabPageTextImg
            // 
            this.tabPageTextImg.Location = new System.Drawing.Point(4, 22);
            this.tabPageTextImg.Name = "tabPageTextImg";
            this.tabPageTextImg.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextImg.Size = new System.Drawing.Size(549, 172);
            this.tabPageTextImg.TabIndex = 4;
            this.tabPageTextImg.Text = "图片文本化";
            this.tabPageTextImg.UseVisualStyleBackColor = true;
            // 
            // tabPageOther
            // 
            this.tabPageOther.Location = new System.Drawing.Point(4, 22);
            this.tabPageOther.Name = "tabPageOther";
            this.tabPageOther.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOther.Size = new System.Drawing.Size(549, 172);
            this.tabPageOther.TabIndex = 3;
            this.tabPageOther.Text = "其他功能";
            this.tabPageOther.UseVisualStyleBackColor = true;
            // 
            // tabPageWorkShop
            // 
            this.tabPageWorkShop.Location = new System.Drawing.Point(4, 22);
            this.tabPageWorkShop.Name = "tabPageWorkShop";
            this.tabPageWorkShop.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWorkShop.Size = new System.Drawing.Size(549, 172);
            this.tabPageWorkShop.TabIndex = 5;
            this.tabPageWorkShop.Text = "WorkShop";
            this.tabPageWorkShop.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // pictureBoxBefore
            // 
            this.pictureBoxBefore.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBoxBefore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBefore.Location = new System.Drawing.Point(12, 35);
            this.pictureBoxBefore.Name = "pictureBoxBefore";
            this.pictureBoxBefore.Size = new System.Drawing.Size(360, 290);
            this.pictureBoxBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBefore.TabIndex = 3;
            this.pictureBoxBefore.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxBefore, "选择需要加工的图片");
            this.pictureBoxBefore.Click += new System.EventHandler(this.pictureBoxBefore_Click);
            // 
            // pictureBoxAfter
            // 
            this.pictureBoxAfter.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxAfter.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxAfter.Image")));
            this.pictureBoxAfter.Location = new System.Drawing.Point(410, 35);
            this.pictureBoxAfter.Name = "pictureBoxAfter";
            this.pictureBoxAfter.Size = new System.Drawing.Size(360, 290);
            this.pictureBoxAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAfter.TabIndex = 4;
            this.pictureBoxAfter.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxAfter, "导出加工后的图片");
            // 
            // buttonChooseBefore
            // 
            this.buttonChooseBefore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChooseBefore.Location = new System.Drawing.Point(111, 329);
            this.buttonChooseBefore.Name = "buttonChooseBefore";
            this.buttonChooseBefore.Size = new System.Drawing.Size(140, 26);
            this.buttonChooseBefore.TabIndex = 7;
            this.buttonChooseBefore.TabStop = false;
            this.buttonChooseBefore.Text = "选择原图片";
            this.toolTip1.SetToolTip(this.buttonChooseBefore, "选择需要加工的图片");
            this.buttonChooseBefore.UseVisualStyleBackColor = true;
            this.buttonChooseBefore.Click += new System.EventHandler(this.buttonChooseBefore_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Location = new System.Drawing.Point(528, 329);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(140, 26);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.TabStop = false;
            this.buttonExport.Text = "导出图片";
            this.toolTip1.SetToolTip(this.buttonExport, "导出加工后的图片");
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonExportjpg
            // 
            this.buttonExportjpg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportjpg.Location = new System.Drawing.Point(667, 329);
            this.buttonExportjpg.Name = "buttonExportjpg";
            this.buttonExportjpg.Size = new System.Drawing.Size(52, 26);
            this.buttonExportjpg.TabIndex = 9;
            this.buttonExportjpg.TabStop = false;
            this.buttonExportjpg.Text = ".jpg";
            this.toolTip1.SetToolTip(this.buttonExportjpg, "导出为联合图像专家组(*.jpg)");
            this.buttonExportjpg.UseVisualStyleBackColor = true;
            // 
            // buttonExportPng
            // 
            this.buttonExportPng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportPng.Location = new System.Drawing.Point(718, 329);
            this.buttonExportPng.Name = "buttonExportPng";
            this.buttonExportPng.Size = new System.Drawing.Size(52, 26);
            this.buttonExportPng.TabIndex = 10;
            this.buttonExportPng.TabStop = false;
            this.buttonExportPng.Text = ".png";
            this.toolTip1.SetToolTip(this.buttonExportPng, "导出为便携式网络图形文件(*.png)");
            this.buttonExportPng.UseVisualStyleBackColor = true;
            // 
            // pictureBoxHelp
            // 
            this.pictureBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxHelp.Location = new System.Drawing.Point(400, 451);
            this.pictureBoxHelp.Name = "pictureBoxHelp";
            this.pictureBoxHelp.Size = new System.Drawing.Size(150, 100);
            this.pictureBoxHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHelp.TabIndex = 12;
            this.pictureBoxHelp.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxHelp, "帮助图片");
            this.pictureBoxHelp.Click += new System.EventHandler(this.pictureBoxHelp_Click);
            // 
            // textBoxToolTips
            // 
            this.textBoxToolTips.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxToolTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxToolTips.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxToolTips.Font = new System.Drawing.Font("宋体", 10F);
            this.textBoxToolTips.Location = new System.Drawing.Point(400, 392);
            this.textBoxToolTips.Multiline = true;
            this.textBoxToolTips.Name = "textBoxToolTips";
            this.textBoxToolTips.ReadOnly = true;
            this.textBoxToolTips.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxToolTips.Size = new System.Drawing.Size(150, 60);
            this.textBoxToolTips.TabIndex = 11;
            this.textBoxToolTips.TabStop = false;
            this.textBoxToolTips.Text = "鼠标悬停查看帮助\r\n悬停或点击下方帮助图片查看大图";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 361);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlToolChose);
            this.splitContainer1.Size = new System.Drawing.Size(784, 200);
            this.splitContainer1.SplitterDistance = 559;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.TabStop = false;
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.BackColor = System.Drawing.Color.DimGray;
            this.textBoxConsole.CausesValidation = false;
            this.textBoxConsole.Font = new System.Drawing.Font("宋体", 9F);
            this.textBoxConsole.ForeColor = System.Drawing.Color.White;
            this.textBoxConsole.Location = new System.Drawing.Point(562, 361);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.ReadOnly = true;
            this.textBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConsole.Size = new System.Drawing.Size(222, 199);
            this.textBoxConsole.TabIndex = 0;
            this.textBoxConsole.TabStop = false;
            this.textBoxConsole.Text = "消息日志输出\r\n";
            this.toolTip1.SetToolTip(this.textBoxConsole, "消息日志输出");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "原图片";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "加工后图片";
            // 
            // exportAsToolStripMenuItem
            // 
            this.exportAsToolStripMenuItem.Name = "exportAsToolStripMenuItem";
            this.exportAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAsToolStripMenuItem.Text = "Export As";
            // 
            // exportAsPNGToolStripMenuItem
            // 
            this.exportAsPNGToolStripMenuItem.Name = "exportAsPNGToolStripMenuItem";
            this.exportAsPNGToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAsPNGToolStripMenuItem.Text = "Export As PNG";
            // 
            // exportAsJPGToolStripMenuItem
            // 
            this.exportAsJPGToolStripMenuItem.Name = "exportAsJPGToolStripMenuItem";
            this.exportAsJPGToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAsJPGToolStripMenuItem.Text = "Export As JPG";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // pictureBoxWait
            // 
            this.pictureBoxWait.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWait.Image")));
            this.pictureBoxWait.Location = new System.Drawing.Point(304, 153);
            this.pictureBoxWait.Name = "pictureBoxWait";
            this.pictureBoxWait.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWait.TabIndex = 13;
            this.pictureBoxWait.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxWait, "请等待程序处理完成");
            this.pictureBoxWait.Visible = false;
            // 
            // progressBarWait
            // 
            this.progressBarWait.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarWait.Location = new System.Drawing.Point(0, 25);
            this.progressBarWait.Name = "progressBarWait";
            this.progressBarWait.Size = new System.Drawing.Size(784, 20);
            this.progressBarWait.TabIndex = 14;
            this.progressBarWait.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.progressBarWait);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.pictureBoxWait);
            this.Controls.Add(this.pictureBoxHelp);
            this.Controls.Add(this.textBoxToolTips);
            this.Controls.Add(this.buttonExportPng);
            this.Controls.Add(this.buttonExportjpg);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonChooseBefore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxAfter);
            this.Controls.Add(this.pictureBoxBefore);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PicTool - 一个简单的图片加工工具";
            this.toolTip1.SetToolTip(this, "鼠标悬停查看帮助\r\n悬停或点击下方帮助图片查看大图");
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControlToolChose.ResumeLayout(false);
            this.tabPageCompatible.ResumeLayout(false);
            this.tabPageCompatible.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCMBTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.TabControl tabControlToolChose;
        private System.Windows.Forms.TabPage tabPageCompatible;
        private System.Windows.Forms.TabPage tabPageGrayscale;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.PictureBox pictureBoxBefore;
        private System.Windows.Forms.PictureBox pictureBoxAfter;
        private System.Windows.Forms.TabPage tabPageCommon;
        private System.Windows.Forms.TabPage tabPageOther;
        private System.Windows.Forms.TabPage tabPageTextImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonChooseBefore;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonExportjpg;
        private System.Windows.Forms.Button buttonExportPng;
        private System.Windows.Forms.TabPage tabPageWorkShop;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchProcessingToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxToolTips;
        private System.Windows.Forms.ToolStripMenuItem dlcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourcePictureToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxHelp;
        private System.Windows.Forms.NumericUpDown numericUpDownCMBTimes;
        private System.Windows.Forms.Button buttonCMBUniversalColor;
        private System.Windows.Forms.Label labeltCMB;
        private System.Windows.Forms.Button buttonCMBStart;
        private System.Windows.Forms.TextBox textBoxCMBColor;
        private System.Windows.Forms.ToolStripMenuItem exportAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportAsPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsJPGToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxWait;
        private System.Windows.Forms.ProgressBar progressBarWait;
    }
}

