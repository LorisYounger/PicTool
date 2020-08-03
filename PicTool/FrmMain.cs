using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MultiLang;
using LinePutScript;
using System.IO;
using Steamworks;
using static PicTool.Program;
using static PicTool.Function;
using System.Diagnostics;
using System.Threading;

namespace PicTool
{
    public partial class FrmMain : Form
    {
        #region 多语言支持
        //多语言支持项目来自 https://github.com/LorisYounger/Multi-Language-Support
        //语言项目
        public List<Lang> Langs = new List<Lang>();

        /// <summary>
        /// 该Form的翻译方法
        /// </summary>
        /// <param name="lang">语言</param>
        private void Translate(Lang lang)
        {
            lang.Translate(this);
            //手动添加进行修改 例如 menu
            foreach (Line line in lang.FindLangForm(this).FindGroupLine("menu"))
                foreach (var tmp in menuStrip.Items.Find(line.Info, true))
                {
                    tmp.Text = line.Text;
                }
            foreach (Line line in lang.FindLangForm(this).FindGroupLine(".ToolTip"))
            {
                foreach (var tmp in this.Controls.Find(line.Info.Split('.')[0], true))
                {
                    toolTip1.SetToolTip(tmp, line.Text);
                }
            }
            foreach (TabPage tp in tabControlToolChose.TabPages)
            {
                var tmps = lang.FindLangForm(this).FindGroupLine("Header");
                var tmp = tmps.Find(x => x.Info == tp.Name);
                if (tmp != null)
                    tp.Text = tmp.Text;
            }

            //版本号加上
            this.Text += " - ver " + Program.Version;
        }

        public Lang lang;
        public void LangClick(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            Setting.FindorAddLine("Lang").Info = mi.Text;
            var lang = Langs.Find(x => x.Language == mi.Text);
            Translate(lang);
        }
        #endregion

        /// <summary>
        /// 软件设置
        /// </summary>
        public LpsDocument Setting;
        /// <summary>
        /// 是否选择了图片
        /// </summary>
        private bool sChooseImage = false;
        /// <summary>
        /// 是否生成了图片
        /// </summary>
        private bool sProducesResult = false;
        /// <summary>
        /// 储存软件设置
        /// </summary>
        public void SaveSetting()
        {
            File.WriteAllText(Application.StartupPath + @"\Setting.lpt", Setting.ToString());
        }
        /// <summary>
        /// 兼容用的多线程模块
        /// </summary>
        /// <param name="ocpd"></param>
        public void ComPatible(object ocpd)
        {
            ComPatData cpd = (ComPatData)ocpd;
            for (; cpd.x < cpd.xp; cpd.x++)
            {
                for (int y = 0; y < cpd.img.Height; y++)
                {
                    cpd.img.SetPixel(cpd.x, y, CompatibleColor(cpd.img.GetPixel(cpd.x, y), cpd.Colors));
                }
            }
            log($"[{cpd.id}]" + lang.Translate("线程已经完成"));
        }
        public struct ComPatData
        {
            public List<Color> Colors;
            public Bitmap img;
            public int x;
            public int xp;
            public int id;
        }
        public FrmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            pictureBoxWait.Dock = DockStyle.Fill;//等待窗口填充满
            hideprocessbartimer = new System.Timers.Timer()
            {
                AutoReset =false,
                Interval = 3000,                
            };
            hideprocessbartimer.Elapsed += Hideprocessbartimer_Elapsed;

            //读取软件设置
            if (new FileInfo(Application.StartupPath + @"\Setting.lpt").Exists)
            {
                Setting = new LpsDocument(Application.StartupPath + @"\Setting.lpt");
            }
            else
                Setting = new LpsDocument("Setting#PicTool:|\n");

            //判断是不是Steam用户,因为本软件会发布到Steam
            //在 https://store.steampowered.com/app/1381380/PicTool/
            try
            {
                SteamClient.Init(1381380, true);
                SteamClient.RunCallbacks();
                IsSteamUser = SteamClient.IsValid;
                //同时看看有没有买dlc,如果有就添加dlc按钮
                if (Steamworks.SteamApps.IsDlcInstalled(1386450))
                    dlcToolStripMenuItem.Visible = true;
            }
            catch
            {
                IsSteamUser = false;
            }

            if (IsSteamUser)
            {
                //DEBUG:清空成就信息
                //SteamUserStats.ResetAll(true); // true = wipe achivements too
                //SteamUserStats.StoreStats();
                //SteamUserStats.RequestCurrentStats();

                //Steamworks.SteamUserStats.OnAchievementProgress += AchievementChanged;

                //激活第一个成就 第一次启动程序
                Steamworks.SteamUserStats.AddStat("stat_OperationCounts", 1);
                Steamworks.SteamUserStats.StoreStats();

                //MessageBox.Show(Steamworks.SteamUserStats.GetStatInt("stat_OperationCounts").ToString());
            }

            //加载语言模块

            //收集全部语言
            DirectoryInfo info = new DirectoryInfo(Application.StartupPath + @"\lang\");
            if (info.Exists)
            {
                Lang tmp;
                StreamReader sr;
                foreach (FileInfo fi in info.GetFiles("*.lang"))
                {
                    sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
                    tmp = new Lang(sr.ReadToEnd(), "PicTool");
                    sr.Close();
                    sr.Dispose();
                    if (!tmp.Language.Contains("ERROR"))
                    {
                        Langs.Add(tmp);
                        languageToolStripMenuItem.DropDownItems.Add(Langs.Last().Language, null, LangClick);
                    }
                }
                //加载语言选项
                if (Setting.FindLine("Lang") != null)
                {
                    string settinglang = Setting.FindLine("Lang").Info;
                    lang = Langs.Find(x => x.Language == settinglang);
                }
                else
                {
                    //尝试读取下系统语言放进去
                    lang = Langs.Find(x => x.ThreeLetterWindowsLanguageName == System.Globalization.CultureInfo.InstalledUICulture.ThreeLetterWindowsLanguageName);
                }
                //如果还是找不到试试给个英语,应该都会吧
                if (lang == null)
                {
                    lang = Langs.Find(x => x.ThreeLetterWindowsLanguageName == "ENU");
                }
            }
            if (lang == null)
                lang = new Lang();
            Text += " - ver " + Program.Version;
            if (IsSteamUser)
                log(lang.Translate("欢迎使用PicTool") + ',' + Steamworks.SteamClient.Name);
            else
                log(lang.Translate("欢迎使用PicTool"));

        }


        /// <summary>
        /// 打开图片
        /// </summary>
        private void OpenImage()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = lang.Translate("全部可用图片(*.jpg;*.jpge;*.png;*.bmp;*.ptraw)|*.jpg;*.jpge;*.png;*.bmp;*.ptraw|全部文件(*.*)|*.*"),
            };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (ofd.FileName.ToLower().EndsWith(".ptraw"))
            {
                //PTraw 新格式
                //其实就是txt文本,里面的全是图片颜色值

                //TODO

                sChooseImage = true;
                return;
            }
            try
            {
                pictureBoxBefore.Image = Image.FromFile(ofd.FileName);
            }
            catch
            {
                MessageBox.Show(lang.Translate("文件已损坏"));
                return;
            }
            sChooseImage = true;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        private void SaveImage()
        {
            if (!sProducesResult)
            {
                MessageBox.Show(lang.Translate("请先在加工图片后再导出"));
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = lang.Translate("便携式网络图形(*.png)|*.png|联合图像专家组(*.jpg;*.jpge)|*.jpg;*.jpge|图形交换格式(*.gif)|*.gif|纯文本源数据(*.ptraw)|*.ptraw"),
            };
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (sfd.FileName.ToLower().EndsWith(".ptraw"))
            {
                //PTraw 新格式
                //其实就是txt文本,里面的全是图片颜色值

                //TODO

                return;
            }
            pictureBoxAfter.Image.Save(sfd.FileName);
        }
        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="logtext">日志文本</param>
        private void log(string logtext)
        {
            textBoxConsole.AppendText("\r\n" + logtext);
            textBoxConsole.SelectionStart = textBoxConsole.Text.Length;
            Log.AppendLine(logtext);
        }

        private void Waiting(bool wait)
        {
            UseWaitCursor = wait;
            pictureBoxWait.Visible = wait;
            splitContainer1.Visible = !wait;
            if (wait)
            {
                progressBarWait.Value = 0;
                progressBarWait.Maximum = 100;
                progressBarWait.Visible = true;
            }
            else
            {
                hideprocessbartimer.Start();
                progressBarWait.Value = progressBarWait.Maximum;
            }
        }



        /// <summary>
        /// 颜色兼容操作
        /// </summary>
        private void CMBStart()
        {
            log(lang.Translate("\r\n--正在进行图片兼容--\r\n"));


            List<Color> Colors = new List<Color>();
            try
            {
                foreach (string str in textBoxCMBColor.Text.Trim(';').Split(';'))
                {
                    Colors.Add(HEXToColor(str));
                }
            }
            catch
            {
                MessageBox.Show(lang.Translate("请检查兼容颜色框文本框中的输入有误,请检查"));
            }

            log(lang.Translate("读取图片集完成,共计[0]个颜色\r\n", Colors.Count.ToString()));

            if (Colors.Count < 2)
            {
                Waiting(false);
                MessageBox.Show(lang.Translate("颜色不足2个,无法进行兼容计算,任务失败"));
                return;
            }
            progressBarWait.Value = 10;
            //开始干活

            Bitmap img = new Bitmap(pictureBoxBefore.Image);
            Bitmap[] imgs = new Bitmap[7];//多线程操作
            Thread[] threads = new Thread[7];
            int xb = img.Width / 8;
            for (int s = 0; s < 7; s++)
            {
                threads[s] = new Thread(new ParameterizedThreadStart(ComPatible));
                imgs[s] = new Bitmap(img);
                threads[s].Start(new ComPatData()
                {
                    id = s,
                    x = s * xb,
                    xp = (s + 1) * xb,
                    Colors = Colors,
                    img = imgs[s]
                });
            }
            for (int x = xb * 7; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    img.SetPixel(x, y, CompatibleColor(img.GetPixel(x, y), Colors));
                }
            }
            progressBarWait.Value = 80;
            Graphics g1 = Graphics.FromImage(img);

            for (int s = 0; s < 7; s++)
            {
                threads[s].Join();
                g1.DrawImage(imgs[s], s * xb, 0, new Rectangle(new Point(s * xb, 0), new Size(xb, img.Height)), GraphicsUnit.Pixel);
                imgs[s].Dispose();
            }
            pictureBoxAfter.Image = img;
            log(lang.Translate("\r\n--全部任务已完成--\r\n"));
            g1.Dispose();
            sProducesResult = true;
            Waiting(false);
        }



        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsSteamUser)
                Steamworks.SteamClient.Shutdown();//关掉和Steam的连线
            //储存软件设置
            SaveSetting();
        }
        //private void AchievementChanged(Steamworks.Data.Achievement ach, int currentProgress, int progress)
        //{//由于steamoverlayUI不可用,尝试手动复刻成就解锁
        //    //我太难了,WPF的overlayUI会主动附着到按键上导致程序卡死
        //    //WinForm的overlayUI打不开
        //    if (ach.State)
        //    {
        //        MessageBox.Show($"{ach.Name} WAS UNLOCKED!");
        //    }
        //    //不过我看了下,就算OverUI不起作用,正常的弹窗还是可以弹出来的,可以注释掉这个了
        //}

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Waiting(true);

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            switch (e.AssociatedControl.Name)
            {
                case "pictureBoxHelp":
                    FrmHelpImage fhi = new FrmHelpImage(pictureBoxHelp.Image);
                    fhi.ShowDialog();
                    return;
            }

            textBoxToolTips.Text = toolTip1.GetToolTip(e.AssociatedControl);
            if (File.Exists(Application.StartupPath + $"\\help\\{e.AssociatedControl.Name}.png"))
                pictureBoxHelp.Image = Image.FromFile(Application.StartupPath + $"\\help\\{e.AssociatedControl.Name}.png");
            else
                pictureBoxHelp.Image = Image.FromFile(Application.StartupPath + $"\\help\\Nomal{Rnd.Next(5)}.png");
        }

        private void batchProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lang.Translate("在未来可用"));
        }

        private void buttonChooseBefore_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void sourcePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void pictureBoxBefore_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void dlcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + @"\Artbook"))
            {
                Process.Start("explorer.exe", Application.StartupPath + @"\Artbook");
            }
            else
            {
                MessageBox.Show(lang.Translate("请在steam界面下载此dlc"));
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void pictureBoxHelp_Click(object sender, EventArgs e)
        {
            FrmHelpImage fhi = new FrmHelpImage(pictureBoxHelp.Image);
            fhi.ShowDialog();
        }

        private void buttonCMBUniversalColor_Click(object sender, EventArgs e)
        {
            //if (!sChooseImage)
            //{
            //    MessageBox.Show(Language.Translate("请选择需要加工的图片"));
            //    return;
            //}

            //输出步骤
            int Increment = (int)numericUpDownCMBTimes.Value;

            StringBuilder sb = new StringBuilder();
            for (int r = 0; r <= 256; r += Increment)
            {
                if (r == 256)
                    r = 255;
                for (int g = 0; g <= 256; g += Increment)
                {
                    if (g == 256)
                        g = 255;
                    for (int b = 0; b <= 256; b += Increment)
                    {
                        if (b == 256)
                            b = 255;
                        sb.Append('#');
                        sb.Append((r * 65536 + g * 256 + b).ToString("x").PadLeft(6, '0'));
                        sb.Append(';');
                    }
                }
            }
            sb.Append("#ffffff");
            textBoxCMBColor.Text = sb.ToString();
        }

        private void buttonCMBStart_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread cmb = new Thread(CMBStart);
            Waiting(true);
            cmb.Start();
        }

        private void textBoxCMBColor_DoubleClick(object sender, EventArgs e)
        {
            textBoxCMBColor.SelectAll();
        }
        private void Hideprocessbartimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            progressBarWait.Visible = false;
        }

        System.Timers.Timer hideprocessbartimer;

    }
}
