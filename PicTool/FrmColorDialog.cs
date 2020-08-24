using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinePutScript;
using static PicTool.Function;
namespace PicTool
{
    /// <summary>
    /// 颜色选择窗体
    /// </summary>
    /// 这个颜色选择框写的太好了,我自己都感动了,或许以后可以拿到别的项目用或者单独新建一个
    public partial class FrmColorDialog : Form
    {
        /// <summary>
        /// 设置当前选择的颜色
        /// </summary>
        /// <param name="color">颜色</param>
        private void SetColor(Color color)
        {
            //停止所有更改
            enabled = false;
            //设置选定颜色
            selectcolor = color;
            //显示选定颜色
            Bitmap cci = new Bitmap(10, 10);
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    cci.SetPixel(x, y, color);
            pictureBoxSelectColor.Image = cci;

            //设置滚轮值
            numericUpDownRGBR.Value = color.R;
            numericUpDownRGBG.Value = color.G;
            numericUpDownRGBB.Value = color.B;

            float[] hsb = RGBtoHSB(color);

            numericUpDownHSBH.Value = (decimal)hsb[0];
            numericUpDownHSBS.Value = (decimal)hsb[1] * 100;
            numericUpDownHSBB.Value = (decimal)hsb[2] * 100;
            //设置透明度
            numericUpDownA.Value = (decimal)(color.A / 2.55);

            //设置按钮位置
            SetHbotton(hsb[0]);
            SetSBbotton(hsb[1], hsb[2], color);
            //设置HEX
            textBoxHEX.Text = ColorToAHEX(color);


            ////生成侧边H,一次性代码,用完就注释掉
            //Bitmap cc2 = new Bitmap(1, 360);
            //for (int y = 0; y < 360; y++)
            //    cc2.SetPixel(0, y, HSBtoRGB(y, 1, 1));
            //cc2.Save(@"C:\Visual Studio 2019\Projects\PicTool\cc2.png");

            //生成SB图片
            pictureBoxSB.Image = GenerateSB(hsb[0], color.A);

            enabled = true;
        }

        public Color SelectColor
        {
            set => SetColor(value);
            get => selectcolor;
        }
        private Color selectcolor
        {
            get => ccolor;
            set
            {
                ccolor = value;
                if (diyid != -1)
                {//修改DIY颜色
                    SetDIYColor(diyid, value);
                }
                Setting.FindorAdd("Last").info = ColorToAHEX(value);
            }
        }
        private Color ccolor;
        /// <summary>
        /// 通过H生成SB图片
        /// </summary>
        /// <param name="H">色相H值</param>
        public static Bitmap GenerateSB(float H, byte alpha = 255)
        {
            Bitmap sb = new Bitmap(100, 100);
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
                    sb.SetPixel(x, y, HSBtoRGB(H, (float)(x / 100.0), (float)((100 - y) / 100.0), alpha));
            return sb;
        }
        /// <summary>
        /// 设置H按钮的位置
        /// </summary>
        /// <param name="H">色相H值</param>
        private void SetHbotton(float H)
        {
            //按钮行动范围: x=480, y=21-211 w=190
            buttonH.Top = 21 + (int)(190.0 * H / 359.0);
            buttonH.BackColor = HSBtoRGB(H, 1, 1);
        }
        /// <summary>
        /// 设置DIY颜色
        /// </summary>
        /// <param name="DIYid">DIY编号</param>
        /// <param name="color">颜色</param>
        public void SetDIYColor(int DIYid, Color color)
        {
            Setting.AddorReplaceSub(new Sub("DIY" + DIYid, ColorToAHEX(color)));
            panelBG.Controls.Find("buttonDIY" + DIYid, false)[0].BackColor = color;
        }
        /// <summary>
        /// 获取设置中的DIY颜色
        /// </summary>
        /// <param name="DIYid">DIY编号</param>
        /// <returns>设置中的DIY颜色</returns>
        public Color GetDIYColor(int DIYid)
        {
            var diy = Setting.Find("DIY" + DIYid);
            if (diy == null)
                return Color.White;
            else
                return AHEXToColor(diy.info);
        }
        /// <summary>
        /// 设置SB按钮的位置
        /// <param name="S">饱和度 0-100%</param>
        /// <param name="B">亮度 0-100%</param>
        private void SetSBbotton(float S, float B, Color color)
        {
            //按钮行动范围: start:259, 21  w:190
            buttonSB.Top = 211 - (int)(190.0 * B);
            buttonSB.Left = 259 + (int)(190.0 * S);
            buttonSB.BackColor = color;
            if (B > 0.6 && S < 0.6)
                buttonSB.FlatAppearance.BorderColor = Color.Black;
            else
                buttonSB.FlatAppearance.BorderColor = Color.White;
        }

        /// <summary>
        /// 设置HSB颜色
        /// </summary>
        /// <param name="H">色相 0-359</param>
        /// <param name="S">饱和度 0-100%</param>
        /// <param name="B">亮度 0-100%</param>
        /// <param name="alpha">透明度 0-255 可选</param>
        /// <returns>颜色</returns>
        private void SetHSBColor(float H, float S, float B, byte alpha = 255)
        {
            enabled = false;

            //先计算出新颜色值
            selectcolor = HSBtoRGB(H, S, B, alpha);

            Bitmap cci = new Bitmap(10, 10);
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    cci.SetPixel(x, y, selectcolor);

            pictureBoxSelectColor.Image = cci;


            numericUpDownRGBR.Value = selectcolor.R;
            numericUpDownRGBG.Value = selectcolor.G;
            numericUpDownRGBB.Value = selectcolor.B;

            //float[] hsb = RGBtoHSB(color);

            //numericUpDownHSBH.Value = (decimal)hsb[0];
            //numericUpDownHSBS.Value = (decimal)hsb[1] * 100;
            //numericUpDownHSBB.Value = (decimal)hsb[2] * 100;

            SetHbotton(H);
            SetSBbotton(S, B, selectcolor);

            textBoxHEX.Text = ColorToAHEX(selectcolor);

            //numericUpDownA.Value = (decimal)(color.A / 2.55);

            //pictureBoxSB.Image = GenerateSB(H, selectcolor.A);

            enabled = true;
        }

        /// <summary>
        /// 设置文件,包括储存的自定义颜色和上次选择的颜色
        /// </summary>
        public Line Setting;





        public FrmColorDialog()
        {
            InitializeComponent();
            Setting = new Line("ColorDialog", "Temporary");
            SetColor(Color.Black);
        }

        public FrmColorDialog(Line setting)
        {
            InitializeComponent();
            Setting = setting;
            Sub lastcolor = Setting.Find("Last");
            if (lastcolor == null)
                SetColor(Color.Black);
            else
                SetColor(AHEXToColor(lastcolor.info));
            LoadDIY();
        }
        private void LoadDIY()
        {
            foreach (Button button in panelBG.Controls)
            {
                button.BackColor = GetDIYColor(Convert.ToInt32(button.Tag));
            }
        }
        public FrmColorDialog(Color selectColor)
        {
            InitializeComponent();
            Setting = new Line("ColorDialog", "Temporary");
            SetColor(selectColor);
        }
        public FrmColorDialog(Color selectColor, Line setting)
        {
            InitializeComponent();
            Setting = setting;
            SetColor(selectColor);
            LoadDIY();
        }
        /// <summary>
        /// 自定义语言文本
        /// </summary>
        /// <param name="BaseColor">基本颜色</param>
        /// <param name="DIYColor">自定义颜色</param>
        /// <param name="Yes">是</param>
        /// <param name="No">否</param>
        public void LanguageDIY(string BaseColor = "Base Color", string DIYColor = "DIY Color",string Yes = "OK", string No = "Cancel")
        {
            groupBoxBase.Text = BaseColor;
            groupBoxDIY.Text = DIYColor;
            buttonOK.Text = Yes;
            buttonCancel.Text = No;
        }
        /// <summary>
        /// DIY颜色编号,不是DIY为-1
        /// </summary>
        private int diyid = -1;
        /// <summary>
        /// 是否可以修改参数
        /// </summary>
        private bool enabled = true;
        private void buttonSelectColor_Click(object sender, EventArgs e)
        {
            diyid = -1;
            SetColor(((Button)sender).BackColor);
        }

        bool pictureBoxHclick = false;
        private void pictureBoxH_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxHclick = true;
            if (e.Y >= 197 || e.Y < 0)
                numericUpDownHSBH.Value = 0;
            else
                numericUpDownHSBH.Value = (decimal)(e.Y * 1.82);
        }

        private void pictureBoxH_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxHclick = false;
            pictureBoxSB.Image = GenerateSB((float)numericUpDownHSBH.Value, selectcolor.A);
        }

        private void pictureBoxH_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBoxHclick)
                if (e.Y >= 197 || e.Y < 0)
                    numericUpDownHSBH.Value = 0;
                else
                    numericUpDownHSBH.Value = (decimal)(e.Y * 1.82);
        }

        private void numericUpDownHSB_ValueChanged(object sender, EventArgs e)
        {
            if (!enabled)
                return;//如果不允许修改就直接返回
            SetHSBColor((float)numericUpDownHSBH.Value, (float)numericUpDownHSBS.Value / 100, (float)numericUpDownHSBB.Value / 100, (byte)((double)numericUpDownA.Value * 2.55 + 0.01));
        }
        bool pictureBoxSBclick = false;
        private void pictureBoxSB_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxSBclick = true;
            if (e.Y >= 196)
                numericUpDownHSBB.Value = 0;
            else if (e.Y < 0)
                numericUpDownHSBB.Value = 100;
            else
                numericUpDownHSBB.Value = (decimal)(100 - e.Y * 0.51);
            if (e.X >= 196)
                numericUpDownHSBS.Value = 100;
            else if (e.X < 0)
                numericUpDownHSBS.Value = 0;
            else
                numericUpDownHSBS.Value = (decimal)(e.X * 0.51);
        }

        private void pictureBoxSB_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxSBclick = false;
        }

        private void pictureBoxSB_MouseMove(object sender, MouseEventArgs e)
        {
            if (!pictureBoxSBclick)
                return;
            if (e.Y >= 196)
                numericUpDownHSBB.Value = 0;
            else if (e.Y < 0)
                numericUpDownHSBB.Value = 100;
            else
                numericUpDownHSBB.Value = (decimal)(100 - e.Y * 0.51);
            if (e.X >= 196)
                numericUpDownHSBS.Value = 100;
            else if (e.X < 0)
                numericUpDownHSBS.Value = 0;
            else
                numericUpDownHSBS.Value = (decimal)(e.X * 0.51);
        }

        private void numericUpDownHSBH_ValueChanged(object sender, EventArgs e)
        {
            if (!enabled)
                return;//如果不允许修改就直接返回
            SetHSBColor((float)numericUpDownHSBH.Value, (float)numericUpDownHSBS.Value / 100, (float)numericUpDownHSBB.Value / 100, (byte)((double)numericUpDownA.Value * 2.55));
            if (!pictureBoxHclick)
                pictureBoxSB.Image = GenerateSB((float)numericUpDownHSBH.Value, selectcolor.A);
        }

        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            if (!enabled)
                return;//如果不允许修改就直接返回
            enabled = false;

            //先修改选择的颜色
            selectcolor = Color.FromArgb((byte)((double)numericUpDownA.Value * 2.55 + 0.01), selectcolor);

            //修改调色盘
            pictureBoxSB.Image = GenerateSB((float)numericUpDownHSBH.Value, selectcolor.A);

            //修改显示颜色
            Bitmap cci = new Bitmap(10, 10);
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    cci.SetPixel(x, y, selectcolor);

            pictureBoxSelectColor.Image = cci;

            //修改HEX
            textBoxHEX.Text = ColorToAHEX(selectcolor);

            enabled = true;
        }

        private void textBoxHEX_TextChanged(object sender, EventArgs e)
        {
            if (!enabled)
                return;//如果不允许修改就直接返回
            enabled = false;
            if (textBoxHEX.Text.Contains('#'))
                textBoxHEX.Text = textBoxHEX.Text.Replace("#", "");
            Color color;
            try
            {
                switch (textBoxHEX.Text.Length)
                {
                    case 3:
                    case 6:
                        color = HEXToColor(textBoxHEX.Text);
                        break;
                    case 4:
                    case 8:
                        color = AHEXToColor(textBoxHEX.Text);
                        break;
                    default:
                        enabled = true;
                        return;
                }
            }
            catch
            {
                enabled = true;
                return;
            }
            //设置选定颜色
            selectcolor = color;
            //显示选定颜色
            Bitmap cci = new Bitmap(10, 10);
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    cci.SetPixel(x, y, color);
            pictureBoxSelectColor.Image = cci;

            //设置滚轮值
            numericUpDownRGBR.Value = color.R;
            numericUpDownRGBG.Value = color.G;
            numericUpDownRGBB.Value = color.B;

            float[] hsb = RGBtoHSB(color);

            numericUpDownHSBH.Value = (decimal)hsb[0];
            numericUpDownHSBS.Value = (decimal)hsb[1] * 100;
            numericUpDownHSBB.Value = (decimal)hsb[2] * 100;
            //设置透明度
            numericUpDownA.Value = (decimal)(color.A / 2.55);

            //设置按钮位置
            SetHbotton(hsb[0]);
            SetSBbotton(hsb[1], hsb[2], color);

            //生成SB图片
            pictureBoxSB.Image = GenerateSB(hsb[0], color.A);

            enabled = true;
        }

        private void numericUpDownRGB_ValueChanged(object sender, EventArgs e)
        {
            if (!enabled)
                return;//如果不允许修改就直接返回
            SetColor(Color.FromArgb((byte)((double)numericUpDownA.Value * 2.55 + 0.01), (byte)numericUpDownRGBR.Value, (byte)numericUpDownRGBG.Value, (byte)numericUpDownRGBB.Value));
        }

        private void buttonDIY_Click(object sender, EventArgs e)
        {
            diyid = Convert.ToInt32(((Button)sender).Tag);
            SetColor(GetDIYColor(diyid));
        }
    }
}
