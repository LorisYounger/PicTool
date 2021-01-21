using System;
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
using System.Threading.Tasks;

namespace PicTool
{
    public partial class FrmMain : Form
    {

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

        public FrmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            pictureBoxWait.Dock = DockStyle.Fill;//等待窗口填充满
            hideprocessbartimer = new System.Timers.Timer()
            {
                AutoReset = false,
                Interval = 2000,
            };
            hideprocessbartimer.Elapsed += Hideprocessbartimer_Elapsed;

            //读取软件设置
            if (new FileInfo(Application.StartupPath + @"\Setting.lpt").Exists)
            {
                Setting = new LpsDocument(File.ReadAllText(Application.StartupPath + @"\Setting.lpt"));
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
                    lang = Langs.Find(x => x.ThreeLetterWindowsLanguageName == settinglang);
                }
                else
                {
                    //尝试读取下系统语言放进去
                    //自动识别语言用的ThreeLetterWindows见https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.cultureinfo.threeletterisolanguagename
                    lang = Langs.Find(x => x.ThreeLetterWindowsLanguageName == System.Globalization.CultureInfo.InstalledUICulture.ThreeLetterWindowsLanguageName);
                }
                //如果还是找不到试试给个英语,应该都会吧
                if (lang == null)
                {
                    lang = Langs.Find(x => x.ThreeLetterWindowsLanguageName == "ENU");
                }
            }
            if (lang == null)
            {
                lang = new Lang();//还找不到就用软件自带的简体中文
                Text += " - ver " + Program.Version;
            }
            else
                Translate(lang);
            if (IsSteamUser)
                log(lang.Translate("欢迎使用PicTool") + ',' + Steamworks.SteamClient.Name);
            else
                log(lang.Translate("欢迎使用PicTool") + ',' + Environment.UserName);
            log("");
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

        //帮助组件
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            switch (e.AssociatedControl.Name)
            {
                case "pictureBoxHelp"://如果是图片框,就突出显示
                    FrmHelpImage fhi = new FrmHelpImage(pictureBoxHelp.Image);
                    fhi.ShowDialog();
                    return;
            }
            textBoxToolTips.Text = toolTip1.GetToolTip(e.AssociatedControl);
            if (e.AssociatedControl.Tag != null)
                pictureBoxHelp.Image = Image.FromFile(Application.StartupPath + $"\\help\\{e.AssociatedControl.Tag}.png");
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
            //储存设置
            Setting.AddorReplaceLine(new Line("textBoxCMBColor", textBoxCMBColor.Text));
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

        private void buttonGSTurn_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(TurnToGray, "Gray"));
        }

        private void buttonGSTurnIgray_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(TurnToGrayrgb2gray, "RGB2Gray"));
        }

        private void buttonGSTurnR_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(TurnToGrayR, "GrayR"));
        }

        private void buttonGSReturnR_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(BackToGrayR, "BackGrayR"));
        }

        private void buttonGSTurnRPlus_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(TurnToGrayRPlus, "GrayRPlus"));

        }

        private void buttonGSReturnRPlus_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(BackToGrayRPlus, "BackGrayRPlus"));

        }

        private void buttonGSBlack_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);

            ////储存设置 这个设置以后在打开和关闭的时候统一存一下
            //Setting.AddorReplaceLine(new Line("numericUpDownGSBlack", numericUpDownGSBlack.Value.ToString()));

            tuns.Start(new TransferFunction((x) => TurnToBlack(x, (byte)numericUpDownGSBlack.Value), "Black"));
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            pictureBoxBefore.Image = pictureBoxAfter.Image;
        }

        private void buttonCleanA_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction((x) => CleanA(x, (byte)numericUpDownGSBlack.Value), "ClearA"));
        }

        private void buttonGSBlack2gray_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);

            //储存设置
            Setting.AddorReplaceLine(new Line("numericUpDownGSBlack", numericUpDownGSBlack.Value.ToString()));

            tuns.Start(new TransferFunction((x) => TurnToBlackrgb2gray(x, (byte)numericUpDownGSBlack.Value), "RGB2Black"));
        }

        private void buttonTurnDarker_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(TurnDarker, "TurnDarker"));
        }

        private void buttonTurnLighter_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction(TurnLighter, "TurnLighter"));
        }

        private async void buttonCanny_ClickAsync(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Waiting(true);
            pictureBoxAfter.Image = await Task.Run(() => CannyChange(new Bitmap(pictureBoxBefore.Image)));
            Waiting(false);
            sProducesResult = true;
        }

        private void buttontextpicautocom_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            int xv = (int)(Math.Sqrt(pictureBoxBefore.Image.Width) * 10);
            if (checkBoxtextpicLattice.Checked)
                xv = xv / 2 * 2;
            if (xv > 999)
            {
                MessageBox.Show(lang.Translate("无法自动推荐,图片过大,请手动指定"));
                return;
            }
            if (xv < 10)
            {
                MessageBox.Show(lang.Translate("无法自动推荐,图片过小"));
                return;
            }
            numericUpDowntextpicX.Value = xv;
            xv = (int)(pictureBoxBefore.Image.Height / Math.Sqrt(pictureBoxBefore.Image.Width) * 9.7);
            if (checkBoxtextpicLattice.Checked)
                xv = xv / 3 * 3;
            if (xv > 999)
            {
                MessageBox.Show(lang.Translate("无法自动推荐,图片过大,请手动指定"));
                return;
            }
            if (xv < 10)
            {
                MessageBox.Show(lang.Translate("无法自动推荐,图片过小"));
                return;
            }
            numericUpDowntextpicY.Value = xv;
        }

        private void buttonCleanCColor_Click(object sender, EventArgs e)
        {
            FrmColorDialog cd = new FrmColorDialog(buttonCleanCColor.BackColor, Setting.FindorAddLine("ColorDialog"));
            cd.LanguageDIY(lang.Translate("基本颜色"), lang.Translate("自定义颜色"), lang.Translate("确定"), lang.Translate("取消"));
            if (cd.ShowDialog() == DialogResult.OK)
                buttonCleanCColor.BackColor = cd.SelectColor;
        }

        private void buttonCleanC_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction((x) => CleanC(x, buttonCleanCColor.BackColor), "CleanC"));
        }

        private void buttonCleanCdeviation_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction((x) => CleanCdeviation(x, buttonCleanCColor.BackColor, (int)numericUpDownCleanCdeviation.Value), "CleanCdeviation"));
        }

        private void colorDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmColorDialog cd = new FrmColorDialog(Setting.FindorAddLine("ColorDialog"));
            cd.LanguageDIY(lang.Translate("基本颜色"), lang.Translate("自定义颜色"), lang.Translate("确定"), lang.Translate("取消"));
            cd.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout(lang.FindLangForm("FrmAbout"));
            about.ShowDialog();
        }

        private void exportAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void exportAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(PictureType.Png);
        }

        private void exportAsJPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(PictureType.Jpg);
        }

        private void buttonExportjpg_Click(object sender, EventArgs e)
        {
            SaveImage(PictureType.Jpg);
        }

        private void buttonExportPng_Click(object sender, EventArgs e)
        {
            SaveImage(PictureType.Png);
        }

        private void pictureBoxAfter_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void buttonSwitchAColor_Click(object sender, EventArgs e)
        {
            FrmColorDialog cd = new FrmColorDialog(buttonSwitchAColor.BackColor, Setting.FindorAddLine("ColorDialog"));
            cd.LanguageDIY(lang.Translate("基本颜色"), lang.Translate("自定义颜色"), lang.Translate("确定"), lang.Translate("取消"));
            if (cd.ShowDialog() == DialogResult.OK)
                buttonSwitchAColor.BackColor = cd.SelectColor;
        }

        private void buttonSwitchA_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Thread tuns = new Thread(Transfer);
            Waiting(true);
            tuns.Start(new TransferFunction((x) => SwitchA(x, buttonSwitchAColor.BackColor), "SwitchA"));
        }
        //private const string Lattice = "⠀⠠⠄⠤⠐⠰⠔⠴⠂⠢";
        private void buttontextpicmake_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)//以后可以设置成透明度有专属的符号啥的
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Waiting(true);
            Thread thread;
            if (checkBoxtextpicLattice.Checked)
                thread = new Thread(() =>
                {
                    log("\r\n--" + lang.Translate("正在进行图片点阵化生成") + "--\r\n");
                    int ix = ((int)numericUpDowntextpicX.Value) / 2 * 2;
                    int iy = ((int)numericUpDowntextpicY.Value) / 4 * 4;
                    StringBuilder sb = new StringBuilder();
                    Bitmap img = ResizeImage((Bitmap)pictureBoxBefore.Image, ix, iy);
                    progressBarWait.Maximum = iy;
                    for (int y = 0; y < iy; y += 4)
                    {
                        for (int x = 0; x < ix; x += 2)
                        {
                            //bool[,] bs = new bool[2, 3];
                            int ans = 0;
                            for (int by = 0; by < 4; by++)
                                for (int bx = 0; bx < 2; bx++)
                                {
                                    ans += TurnToBlackbool(img.GetPixel(x + bx, y + by)) ? 0 : 1;
                                    ans <<= 1;
                                }
                            //if (ans == 0)//无论是原生空格还是盲文空格,都不能对齐
                            //    sb.Append(' ');
                            //else
                            sb.Append(char.ConvertFromUtf32(10240 + (ans >> 1)));
                        }
                        sb.AppendLine();
                        progressBarWait.Value = y;
                    }
                    log("\r\n--" + lang.Translate("全部任务已完成") + "--\r\n");
                    Waiting(false);
                    textBoxtextpicres.Text = sb.ToString();
                });
            else
                thread = new Thread(() =>
                {
                    log("\r\n--" + lang.Translate("正在进行图片文本化生成") + "--\r\n");
                    int lv = 256 / textBoxtextpicsmb.Text.Length;
                    StringBuilder sb = new StringBuilder();
                    Bitmap img = ResizeImage((Bitmap)pictureBoxBefore.Image, (int)numericUpDowntextpicX.Value, (int)numericUpDowntextpicY.Value);
                    progressBarWait.Maximum = img.Height;
                    for (int y = 0; y < img.Height; y++)
                    {
                        for (int x = 0; x < img.Width; x++)
                        {
                            //int i = TurnToBlackByte(img.GetPixel(x, y)) / lv;
                            sb.Append(textBoxtextpicsmb.Text[TurnToGrayByte(img.GetPixel(x, y)) / lv]);
                        }
                        sb.AppendLine();
                        progressBarWait.Value = y;
                    }
                    log("\r\n--" + lang.Translate("全部任务已完成") + "--\r\n");
                    Waiting(false);
                    textBoxtextpicres.Text = sb.ToString();
                });
            thread.Start();
        }

        private void buttondouble_Click(object sender, EventArgs e)
        {
            if (!sChooseImage)
            {
                MessageBox.Show(lang.Translate("请选择需要加工的图片"));
                return;
            }
            Bitmap img2 = openimage();
            if (img2 == null)
            {
                MessageBox.Show(lang.Translate("请选择需要另外一张用于重叠的图片"));
                return;
            }
            //如果img1和2大小不同
            if (pictureBoxBefore.Image.Width != img2.Width || pictureBoxBefore.Image.Height != img2.Height)
                img2 = ResizeImage(img2, pictureBoxBefore.Image.Width, pictureBoxBefore.Image.Height);

            Thread tuns = new Thread(TransferMulti);
            Waiting(true);
            tuns.Start(new TransferFunctionMulti(DoubleColor,img2, "DoubleImage"));
        }

        private void textBoxtextpicres_DoubleClick(object sender, EventArgs e)
        {
            textBoxtextpicres.SelectAll();
        }

        private void checkBoxtextpicLattice_CheckedChanged(object sender, EventArgs e)
        {
            textBoxtextpicsmb.Enabled = !checkBoxtextpicLattice.Checked;
        }

    }
}
