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
    /// <summary>
    /// FrmMain的方法
    /// </summary>
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
            if (lang.Language == "NULL")
                return;
            lang.Translate(this);
            //手动添加进行修改 例如 menu
            foreach (Line line in lang.FindLangForm(this).FindGroupLine("menu"))
                foreach (var tmp in menuStrip.Items.Find(line.Info, true))
                {
                    tmp.Text = line.Text;
                }
            foreach (Line line in lang.FindLangForm(this).FindGroupLine(".ToolTip"))
            {
                foreach (var tmp in Controls.Find(line.Info, true))
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
            var lang = Langs.Find(x => x.Language == mi.Text);
            Setting.FindorAddLine("Lang").Info = lang.ThreeLetterWindowsLanguageName;
            Translate(lang);
        }
        #endregion



        /// <summary>
        /// 打开图片
        /// </summary>
        private void OpenImage()
        {
            Bitmap img = openimage();
            if (img != null)
                pictureBoxBefore.Image = img;
        }
        private Bitmap openimage()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = lang.Translate("全部可用图片(*.jpg;*.jpge;*.png;*.bmp;*.ptraw)|*.jpg;*.jpge;*.png;*.bmp;*.ptraw|全部文件(*.*)|*.*"),
            };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            log(lang.Translate("正在尝试打开文件") + ' ' + ofd.FileName);
            Bitmap open = null;
            if (ofd.FileName.ToLower().EndsWith(".ptraw"))
            {
                //PTraw 新格式
                //其实就是txt文本,里面的全是图片颜色值

                //TODO

                //sChooseImage = true;
                //return null;
            }
            else
                try
                {
                    open = ImageFromFile(ofd.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(lang.Translate("文件已损坏"), e.Message);
                    log(lang.Translate("文件已损坏") + ' ' + e.Message);
                    return null; ;
                }
            log(lang.Translate("文件已打开,大小:[0],[1]", open.Width.ToString(), open.Height.ToString()));
            sChooseImage = true;
            return open;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        private void SaveImage(PictureType type = PictureType.None)
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
            if (type == PictureType.None)
                sfd.FilterIndex = 0;
            else
                sfd.FilterIndex = (int)type;
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            log(lang.Translate("正在尝试导出文件") + ' ' + sfd.FileName);
            if (sfd.FileName.ToLower().EndsWith(".ptraw"))
            {
                //PTraw 新格式
                //其实就是txt文本,里面的全是图片颜色值

                //TODO

                return;
            }
            try
            {
                pictureBoxAfter.Image.Save(sfd.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(lang.Translate("写入失败"), e.Message);
                log(lang.Translate("写入失败") + ' ' + e.Message);
                return;
            }
            log(lang.Translate("文件成功保存"));
            Process.Start("explorer.exe", sfd.FileName);
        }
        private enum PictureType
        {
            /// <summary>
            /// 无
            /// </summary>
            None,
            /// <summary>
            /// 便携式网络图形
            /// </summary>
            Png,
            /// <summary>
            /// 联合图像专家组
            /// </summary>
            Jpg,
            /// <summary>
            /// 图形交换格式
            /// </summary>
            Gif,
            /// <summary>
            /// 纯文本源数据
            /// </summary>
            Ptraw
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
        /// <summary>
        /// 让用户等待,进入等待界面
        /// </summary>
        /// <param name="wait">开始/结束等待</param>
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
                hideprocessbartimer.Stop();
            }
            else
            {
                hideprocessbartimer.Start();
                progressBarWait.Value = progressBarWait.Maximum;
            }
        }




        /// <summary>
        /// 兼容用的多线程模块
        /// </summary>
        /// <param name="ocpd"></param>
        private void ComPatible(object ocpd)
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
        private struct ComPatData
        {
            public List<Color> Colors;
            public Bitmap img;
            public int x;
            public int xp;
            public int id;
        }
        /// <summary>
        /// 颜色兼容操作
        /// </summary>
        private void CMBStart()
        {
            log("\r\n--" + lang.Translate("正在进行图片兼容") + "--\r\n");


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

            log(lang.Translate("读取图片集完成,共计[0]个颜色", Colors.Count.ToString()) + "\r\n");

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
            progressBarWait.Value = 30;
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
            log("\r\n--" + lang.Translate("全部任务已完成") + "--\r\n");
            g1.Dispose();
            sProducesResult = true;
            Waiting(false);
        }



        /// <summary>
        /// 转换用的多线程模块
        /// </summary>
        private void TurnTo(object otd)
        {
            TurnData td = (TurnData)otd;
            for (; td.x < td.xp; td.x++)
            {
                for (int y = 0; y < td.img.Height; y++)
                {
                    td.img.SetPixel(td.x, y, td.turns(td.img.GetPixel(td.x, y)));
                }
            }
            log($"[{td.id}]" + lang.Translate("线程已经完成"));
        }
        /// <summary>
        /// 转换数据
        /// </summary>
        private struct TurnData
        {
            public Bitmap img;
            public int x;
            public int xp;
            public int id;
            public TransferFunction.Turns turns;
        }
        /// <summary>
        /// 一个包裹用于传递转变方法
        /// </summary>
        private class TransferFunction
        {
            public delegate Color Turns(Color color);
            public Turns turns;
            public string FunctionName;
            public bool AutoEndWait;
            public Bitmap Image;
            /// <summary>
            /// 多线程转换图片操作资源
            /// </summary>
            /// <param name="ts"></param>
            /// <param name="name"></param>
            /// <param name="autoendwait"></param>
            public TransferFunction(Turns ts, string name = "Transfer.Function", bool autoendwait = true, Bitmap image = null)
            {
                turns = ts;
                FunctionName = name;
                AutoEndWait = autoendwait;
                Image = image;
            }
        }
        /// <summary>
        /// 图片转换操作
        /// </summary>
        private void Transfer(object transferFunction)
        {
            TransferFunction tf = (TransferFunction)transferFunction;
            log("\r\n--" + lang.Translate("正在进行图片转换操作([0])", tf.FunctionName + "--\r\n"));
            Bitmap beforimg;
            if (tf.Image == null)
                beforimg = new Bitmap(pictureBoxBefore.Image);
            else
                beforimg = tf.Image;

            Bitmap img = new Bitmap(beforimg.Width, beforimg.Height);
            Bitmap[] imgs = new Bitmap[7];//多线程操作
            Thread[] threads = new Thread[7];
            int xb = beforimg.Width / 8;
            for (int s = 0; s < 7; s++)
            {
                threads[s] = new Thread(new ParameterizedThreadStart(TurnTo));
                imgs[s] = new Bitmap(beforimg);
                threads[s].Start(new TurnData()
                {
                    id = s,
                    x = s * xb,
                    xp = (s + 1) * xb,
                    img = imgs[s],
                    turns = tf.turns
                });
            }
            progressBarWait.Value += 30;
            for (int x = xb * 7; x < beforimg.Width; x++)
            {
                for (int y = 0; y < beforimg.Height; y++)
                {
                    img.SetPixel(x, y, tf.turns(beforimg.GetPixel(x, y)));
                }
            }
            progressBarWait.Value += 50;
            Graphics g1 = Graphics.FromImage(img);

            for (int s = 0; s < 7; s++)
            {
                threads[s].Join();
                g1.DrawImage(imgs[s], s * xb, 0, new Rectangle(new Point(s * xb, 0), new Size(xb, img.Height)), GraphicsUnit.Pixel);
                imgs[s].Dispose();
            }
            if (tf.Image == null)
                pictureBoxAfter.Image = img;
            else
                tf.Image = img;

            g1.Dispose();

            if (tf.AutoEndWait)
            {
                log("\r\n--" + lang.Translate("全部任务已完成") + "--\r\n");
                sProducesResult = true;
                Waiting(false);
            }
            else
            {
                progressBarWait.Value += 20;
            }
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        private struct TurnDataMulti
        {
            public Bitmap img;
            public Bitmap img2;
            public int x;
            public int xp;
            public int id;
            public TransferFunctionMulti.Turns turns;
        }
        /// <summary>
        /// 一个包裹用于传递多图片转变方法
        /// </summary>
        private class TransferFunctionMulti : TransferFunction
        {
            public Bitmap Image2;
            public new delegate Color Turns(Color color1, Color color2);
            public new Turns turns;
            /// <summary>
            /// 多线程转换图片操作资源
            /// </summary>       
            public TransferFunctionMulti(Turns ts, Bitmap image2, string name = "Transfer.Multi.Function", bool autoendwait = true, Bitmap image = null) : base(null, name, autoendwait, image)
            {
                turns = ts;
                Image2 = image2;
            }
        }
        /// <summary>
        /// 转换用的多线程模块
        /// </summary>
        private void TurnToMulti(object otd)
        {
            TurnDataMulti td = (TurnDataMulti)otd;
            for (; td.x < td.xp; td.x++)
            {
                for (int y = 0; y < td.img.Height; y++)
                {
                    td.img.SetPixel(td.x, y, td.turns(td.img.GetPixel(td.x, y), td.img2.GetPixel(td.x, y)));
                }
            }
            log($"[{td.id}]" + lang.Translate("线程已经完成"));
        }
        /// <summary>
        /// 多图片加工操作
        /// </summary>
        private void TransferMulti(object transferFunction)
        {
            TransferFunctionMulti tfm = (TransferFunctionMulti)transferFunction;
            log("\r\n--" + lang.Translate("正在进行多图片转换操作([0])", tfm.FunctionName + "--\r\n"));
            Bitmap beforimg;
            if (tfm.Image == null)
                beforimg = new Bitmap(pictureBoxBefore.Image);
            else
                beforimg = tfm.Image;

            Bitmap img = new Bitmap(beforimg.Width, beforimg.Height);
            Bitmap[] imgs = new Bitmap[7];//多线程操作
            Thread[] threads = new Thread[7];
            int xb = beforimg.Width / 8;
            for (int s = 0; s < 7; s++)
            {
                threads[s] = new Thread(new ParameterizedThreadStart(TurnToMulti));
                imgs[s] = new Bitmap(beforimg);
                threads[s].Start(new TurnDataMulti()
                {
                    id = s,
                    x = s * xb,
                    xp = (s + 1) * xb,
                    img = imgs[s],
                    img2 = new Bitmap(tfm.Image2),
                    turns = tfm.turns
                });
            }
            progressBarWait.Value += 30;
            for (int x = xb * 7; x < beforimg.Width; x++)
            {
                for (int y = 0; y < beforimg.Height; y++)
                {
                    img.SetPixel(x, y, tfm.turns(beforimg.GetPixel(x, y),tfm.Image2.GetPixel(x,y)));
                }
            }
            progressBarWait.Value += 50;
            Graphics g1 = Graphics.FromImage(img);

            for (int s = 0; s < 7; s++)
            {
                threads[s].Join();
                g1.DrawImage(imgs[s], s * xb, 0, new Rectangle(new Point(s * xb, 0), new Size(xb, img.Height)), GraphicsUnit.Pixel);
                imgs[s].Dispose();
            }
            if (tfm.Image == null)
                pictureBoxAfter.Image = img;
            else
                tfm.Image = img;

            g1.Dispose();

            if (tfm.AutoEndWait)
            {
                log("\r\n--" + lang.Translate("全部任务已完成") + "--\r\n");
                sProducesResult = true;
                Waiting(false);
            }
            else
            {
                progressBarWait.Value += 20;
            }
        }


        ///外面再包一层就可以了,不需要这么麻烦
        ///// <summary>
        ///// 转换用的多线程模块 (带int参数)
        ///// </summary>
        ///// <param name="ocpd"></param>
        //public void TurnTowithINT(object otdi)
        //{
        //    TurnDatawithINT tdi = (TurnDatawithINT)otdi;
        //    for (; tdi.x < tdi.xp; tdi.x++)
        //    {
        //        for (int y = 0; y < tdi.img.Height; y++)
        //        {
        //            tdi.img.SetPixel(tdi.x, y, tdi.turns(tdi.img.GetPixel(tdi.x, y), tdi.value));
        //        }
        //    }
        //    log($"[{tdi.id}]" + lang.Translate("线程已经完成"));
        //}
        ///// <summary>
        ///// 转换数据 (带int参数)
        ///// </summary>
        //public struct TurnDatawithINT
        //{
        //    public Bitmap img;
        //    public int x;
        //    public int xp;
        //    public int id;
        //    public TransferFunctionwithINT.Turns turns;
        //    public int value;
        //}
        ///// <summary>
        ///// 一个包裹用于传递转变方法 (带int参数)
        ///// </summary>
        //public struct TransferFunctionwithINT
        //{
        //    public delegate Color Turns(Color color, object obj);
        //    public Turns turns;
        //    public string FunctionName;
        //    public int value;
        //    public TransferFunctionwithINT(Turns ts, int val, string name = "TransferwithINT.Function")
        //    {
        //        turns = ts;
        //        value = val;
        //        FunctionName = name;
        //    }
        //}
        ///// <summary>
        ///// 图片转换操作 (带int参数)
        ///// </summary>
        //private void TransferwithINT(object transferFunctionwithINT)
        //{
        //    TransferFunctionwithINT tf = ((TransferFunctionwithINT)transferFunctionwithINT);
        //    log(lang.Translate("\r\n--正在进行图片转换操作([0])--\r\n", tf.FunctionName));
        //    Bitmap img = new Bitmap(pictureBoxBefore.Image);
        //    Bitmap[] imgs = new Bitmap[7];//多线程操作
        //    Thread[] threads = new Thread[7];
        //    int xb = img.Width / 8;
        //    for (int s = 0; s < 7; s++)
        //    {
        //        threads[s] = new Thread(new ParameterizedThreadStart(TurnTowithINT));
        //        imgs[s] = new Bitmap(img);
        //        threads[s].Start(new TurnDatawithINT()
        //        {
        //            id = s,
        //            x = s * xb,
        //            xp = (s + 1) * xb,
        //            img = imgs[s],
        //            turns = tf.turns,
        //            value = tf.value,
        //        });
        //    }
        //    progressBarWait.Value = 30;
        //    for (int x = xb * 7; x < img.Width; x++)
        //    {
        //        for (int y = 0; y < img.Height; y++)
        //        {
        //            img.SetPixel(x, y, tf.turns(img.GetPixel(x, y), tf.value));
        //        }
        //    }
        //    progressBarWait.Value = 80;
        //    Graphics g1 = Graphics.FromImage(img);

        //    for (int s = 0; s < 7; s++)
        //    {
        //        threads[s].Join();
        //        g1.DrawImage(imgs[s], s * xb, 0, new Rectangle(new Point(s * xb, 0), new Size(xb, img.Height)), GraphicsUnit.Pixel);
        //        imgs[s].Dispose();
        //    }
        //    pictureBoxAfter.Image = img;
        //    log(lang.Translate("\r\n--全部任务已完成--\r\n"));
        //    g1.Dispose();
        //    sProducesResult = true;
        //    Waiting(false);
        //}
    }
}
