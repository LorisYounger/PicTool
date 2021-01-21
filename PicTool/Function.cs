using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicTool
{
    ///大部分功能使用的代码存放位置
    public static class Function
    {
        /// <summary>
        /// 通用随机器
        /// </summary>
        public static Random Rnd = new Random();
        /// <summary>
        /// HEX值转颜色
        /// </summary>
        /// <param name="HEX">HEX值</param>
        /// <returns></returns>
        public static Color HEXToColor(string HEX = "")
        {
            HEX = HEX.Replace("#", "");
            if (HEX.Length == 3)
            {
                return Color.FromArgb(Convert.ToInt32("0xff" + HEX[0] + HEX[0] + HEX[1] + HEX[1] + HEX[2] + HEX[2], 16));
            }
            else if (HEX.Length == 6)
            {
                return Color.FromArgb(Convert.ToInt32("0xff" + HEX, 16));
            }
            else
            {
                throw new Exception("HEXToColor");
                int hash = Math.Abs(HEX.GetHashCode());
                int hash1 = hash / 256;
                int hash2 = hash1 / 256;
                return Color.FromArgb(hash % 256, hash1 % 256, hash2 % 256);
            }
        }
        /// <summary>
        /// HEX值(带透明度通道)转颜色
        /// </summary>
        /// <param name="AHEX">HEX值(带透明度通道)</param>
        /// <returns></returns>
        public static Color AHEXToColor(string AHEX = "")
        {
            AHEX = AHEX.Replace("#", "");
            if (AHEX.Length == 4)
            {
                return Color.FromArgb(Convert.ToInt32("0x" + AHEX[0] + AHEX[0] + AHEX[1] + AHEX[1] + AHEX[2] + AHEX[2] + AHEX[3] + AHEX[3], 16));
            }
            else if (AHEX.Length == 8)
            {
                return Color.FromArgb(Convert.ToInt32("0x" + AHEX, 16));
            }
            else
            {
                throw new Exception("AHEXToColor");
                int hash = Math.Abs(AHEX.GetHashCode());
                int hash1 = hash / 256;
                int hash2 = hash1 / 256;
                return Color.FromArgb(hash % 256, hash1 % 256, hash2 % 256);
            }
        }
        /// <summary>
        /// HSB颜色值转颜色
        /// </summary>
        /// <param name="H">色相 0-359</param>
        /// <param name="S">饱和度 0-100%</param>
        /// <param name="B">亮度 0-100%</param>
        /// <param name="alpha">透明度 0-255 可选</param>
        /// <returns>颜色</returns>
        public static Color HSBtoRGB(float H, float S, float B, byte alpha = 255)
        {

            float[] rgb = new float[3];
            //先令饱和度和亮度为100%，调节色相h
            for (int offset = 240, i = 0; i < 3; i++, offset -= 120)
            {
                //算出色相h的值和三个区域中心点(即0°，120°和240°)相差多少，然后根据坐标图按分段函数算出rgb。但因为色环展开后，红色区域的中心点是0°同时也是360°，不好算，索性将三个区域的中心点都向右平移到240°再计算比较方便
                float x = Math.Abs((H + offset) % 360 - 240);
                //如果相差小于60°则为255
                if (x <= 60) rgb[i] = 255;
                //如果相差在60°和120°之间，
                else if (60 < x && x < 120) rgb[i] = ((1 - (x - 60) / 60) * 255);
                //如果相差大于120°则为0
                else rgb[i] = 0;
            }
            //在调节饱和度s
            for (int i = 0; i < 3; i++)
                rgb[i] += (255 - rgb[i]) * (1 - S);
            //最后调节亮度b
            for (int i = 0; i < 3; i++)
                rgb[i] *= B;
            return Color.FromArgb(alpha, (int)rgb[0], (int)rgb[1], (int)rgb[2]);
        }
        /// <summary>
        /// 颜色(RGB)转HSB颜色值
        /// </summary>
        /// <param name="color">RGB颜色</param>
        /// <returns></returns>
        public static float[] RGBtoHSB(Color color)
        {
            float[] hsb = new float[3];
            float[] rgb = new float[3] { color.R, color.G, color.B };
            float[] rearranged = rgb.ToArray();
            int maxIndex = 0, minIndex = 0;
            float tmp;
            //将rgb的值从小到大排列，存在rearranged数组里
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2 - i; j++)
                    if (rearranged[j] > rearranged[j + 1])
                    {
                        tmp = rearranged[j + 1];
                        rearranged[j + 1] = rearranged[j];
                        rearranged[j] = tmp;
                    }
            }
            //rgb的下标分别为0、1、2，maxIndex和minIndex用于存储rgb中最大最小值的下标
            for (int i = 0; i < 3; i++)
            {
                if (rearranged[0] == rgb[i]) minIndex = i;
                if (rearranged[2] == rgb[i]) maxIndex = i;
            }
            if (rearranged[2] == 0)//如果是纯黑,那就直接退回纯黑
                return new float[] { 0, 0, 0 };
            //算出亮度
            hsb[2] = rearranged[2] / 255.0f;
            //算出饱和度
            hsb[1] = 1 - rearranged[0] / rearranged[2];

            //算出色相
            if (hsb[1] == 0)//如果是灰度,H设置为默认的0
                hsb[0] = 0;
            else
                hsb[0] = maxIndex * 120 + 60 * (rearranged[1] / hsb[1] / rearranged[2] + (1 - 1 / hsb[1])) * ((maxIndex - minIndex + 3) % 3 == 1 ? 1 : -1);

            //防止色相为负值
            hsb[0] = (hsb[0] + 360) % 360;
            if (hsb[0] == 360) hsb[0] = 0;//防止色相=360
            return hsb;
        }



        /// <summary>
        /// 颜色转HEX值
        /// </summary>
        /// <param name="Color">颜色</param>
        /// <returns></returns>
        public static string ColorToHEX(Color Color)
        {
            return (Color.R * 65536 + Color.G * 256 + Color.B).ToString("X").PadLeft(6, '0');
        }
        /// <summary>
        /// 颜色转AHEX值
        /// </summary>
        /// <param name="Color">颜色</param>
        /// <returns></returns>
        public static string ColorToAHEX(Color Color)
        {
            return (Color.A * 16777216 + Color.R * 65536 + Color.G * 256 + Color.B).ToString("X").PadLeft(8, '0');
        }
        /// <summary>
        /// 通过Canny生成轮廓图
        /// </summary>
        /// <param name="Img">想要生成的图片</param>
        /// <returns>轮廓图</returns>
        public static Bitmap CannyChange(Bitmap Img)
        {
            Canny CannyData = new Canny(Img);
            return CannyData.DispImg();
        }
        /// <summary>
        /// 随机获得图片中的颜色
        /// </summary>
        /// <param name="Image">图片</param>
        /// <param name="Times">次数</param>
        /// <returns></returns>
        public static List<Color> GetRndColor(Bitmap Image, int Times)
        {
            List<Color> RndList = new List<Color>();
            Random rnd = new Random();
            for (int i = 0; i < Times; i++)
            {
                RndList.Add(Image.GetPixel(rnd.Next(Image.Width), rnd.Next(Image.Height)));
            }
            return RndList;
        }
        /// <summary>
        /// 生成通用颜色
        /// </summary>
        /// <param name="Increment">增量 1-7</param>
        /// <returns>出现次数较多的颜色</returns>
        public static Color[] UniversalColor(int Increment)
        {
            if (Increment > 7)
                Increment = 7;
            else if (Increment < 1)
                Increment = 1;
            Increment = (int)Math.Pow(2, Increment);
            List<Color> Colors = new List<Color>();
            for (int r = 0; r <= 255; r += Increment)
            {
                for (int g = 0; g <= 255; g += Increment)
                {
                    for (int b = 0; b <= 255; b += Increment)
                    {
                        Colors.Add(Color.FromArgb(r, g, b));
                    }
                }
            }
            return Colors.ToArray();
        }
        /// <summary>
        /// 通过比对确定在所有颜色集中哪个颜色最为相似
        /// </summary>
        /// <param name="NowColor">要比对的颜色</param>
        /// <param name="AllColor">全部颜色集</param>
        /// <returns></returns>
        public static Color CompatibleColor(Color NowColor, List<Color> AllColor)
        {
            List<double> doubles = new List<double>();
            double tmp;
            foreach (Color cl in AllColor)
            {
                tmp = ComparerWith(NowColor, cl);
                if (tmp == 0)
                {
                    return NowColor;
                }
                doubles.Add(tmp);
            }
            tmp = Min(doubles);
            return AllColor[doubles.FindIndex(x => x == tmp)];
        }
        /// <summary>
        /// 获取列表中最小的值
        /// </summary>
        /// <param name="dbs">double列表</param>
        /// <returns>最小的值</returns>
        public static double Min(List<double> dbs)
        {
            double min = dbs[0];
            for (int i = 0; i < dbs.Count; i++)
            {
                min = Math.Min(min, dbs[i]);
            }
            return min;
        }
        /// <summary>
        /// 计算两个颜色之间的差距
        /// </summary>
        /// <param name="color1">颜色1</param>
        /// <param name="color2">颜色2</param>
        /// <returns></returns>
        public static double ComparerWith(Color color1, Color color2)
        {
            return Math.Pow(Math.Abs(color1.R - color2.R), 1.2) + Math.Pow(Math.Abs(color1.G - color2.G), 1.2) + Math.Pow(Math.Abs(color1.B - color2.B), 1.2);
        }
        /// <summary>
        /// 将颜色转换为灰度
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度颜色</returns>
        public static Color TurnToGray(Color color)
        {
            byte cl = (byte)(((color.R + color.G + color.B) / 3) * (color.A / 255.0));
            return Color.FromArgb(cl, cl, cl);
        }
        /// <summary>
        /// 将颜色转换为黑白(rec709)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="Threshold">阈值</param>
        /// <returns>灰度颜色</returns>
        public static Color TurnToBlack(Color color, byte Threshold = 128)
        {
            if (((color.R + color.G + color.B) / 3) * (1 - color.A / 255.0) <= Threshold)
                return Color.Black;
            else
                return Color.White;
        }

        /// <summary>
        /// 将颜色转换为黑白
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="Threshold">阈值</param>
        /// <returns>灰度颜色</returns>
        public static Color TurnToBlackrgb2gray(Color color, byte Threshold = 128)
        {
            if (((color.R * 0.2126 + color.G * 0.7152 + color.B * 0.0722) * (color.A / 255.0)) <= Threshold)
                return Color.Black;
            else
                return Color.White;
        }
        /// <summary>
        /// 将颜色转换为灰度值(rgb2gray)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度值</returns>
        public static byte TurnToGrayByte(Color color) => (byte)((color.R * 0.2126 + color.G * 0.7152 + color.B * 0.0722) * (color.A / 255.0));
        /// <summary>
        /// 将颜色转换为黑白值(rgb2gray)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>True白色 False黑色</returns>
        public static bool TurnToBlackbool(Color color) => ((color.R * 0.2126 + color.G * 0.7152 + color.B * 0.0722) * (color.A / 255.0) > 127);
        /// <summary>
        /// 将颜色转换为灰度 通过rgb2gray方法(rec709)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度颜色</returns>
        public static Color TurnToGrayrgb2gray(Color color)
        {
            byte cl = (byte)((color.R * 0.2126 + color.G * 0.7152 + color.B * 0.0722) * (color.A / 255.0));
            return Color.FromArgb(cl, cl, cl);
        }
        /// <summary>
        /// 将颜色转换为灰度 通过R法(可还原)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度颜色</returns>
        public static Color TurnToGrayR(Color color)
        {
            byte cl = (byte)(color.R / 64 * 64 + color.G / 64 * 16 + color.B / 64 * 4 + color.A / 64);
            return Color.FromArgb(cl, cl, cl);
        }
        /// <summary>
        /// 将颜色从灰度转换为彩色 通过逆R方法
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度颜色</returns>
        public static Color BackToGrayR(Color color)
        {
            byte r = (byte)(color.R / 64 * 64);
            byte g = (byte)((color.R - r) / 16 * 64 + 32);
            byte a = (byte)(color.R % 4 * 64);
            byte b = (byte)((color.R % 64 / 4) * 64 + 32);
            r += 32;
            a += 32;
            return Color.FromArgb(a, r, g, b);
        }
        /// <summary>
        /// 将两种颜色调成一种
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public static Color DoubleColor(Color color1, Color color2)
        {
            int col1 = (int)(TurnToGrayByte(color2) * 0.7 + 0.3);
            int col2 = (int)(TurnToGrayByte(color1) * 0.3);
            int a = 255 - col1 + col2;
            if (a > 255)
                a = 255;
            int col = 255;
            if (a != 0)
                col = (int)((double)col2 / a);
            return Color.FromArgb(a, col, col, col);
        }

        /// <summary>
        /// 将颜色转换为灰度 通过R法(可还原)(无透明度)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度颜色</returns>
        public static Color TurnToGrayRPlus(Color color)
        {
            bool r, g, b; double tmp;
            tmp = (color.R / 64.0);
            tmp -= (int)tmp;
            r = tmp < 0.5;
            tmp = (color.G / 64.0);
            tmp -= (int)tmp;
            g = tmp < 0.5;
            tmp = (color.B / 64.0);
            tmp -= (int)tmp;
            b = tmp < 0.5;
            byte a = 0;
            if (r && g && b)
                a = 1;
            else if (r && g)
                a = 2;
            else if (g && b)
                a = 3;
            byte cl = (byte)(color.R / 64 * 64 + color.G / 64 * 16 + color.B / 64 * 4 + a);
            return Color.FromArgb(cl, cl, cl);
        }
        /// <summary>
        /// 将颜色从灰度转换为彩色 通过逆R方法(无透明度)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>灰度颜色</returns>
        public static Color BackToGrayRPlus(Color color)
        {
            byte r = (byte)(color.R / 64 * 64);
            byte g = (byte)((color.R - r) / 16 * 64 + 63);
            byte a = (byte)(color.R % 4);//A在plus里面作为指示作用
            byte b = (byte)((color.R % 64 / 4) * 64 + 63);
            r += 63;
            switch (a)
            {
                case 1://全降
                    r -= 47;
                    g -= 47;
                    b -= 47;
                    break;
                case 2://降低rg
                    r -= 47;
                    g -= 47;
                    break;
                case 3://降低gb
                    g -= 47;
                    b -= 47;
                    break;
            }
            return Color.FromArgb(r, g, b);
        }
        /// <summary>
        /// 半透明颜色变不透明
        /// </summary>
        /// <param name="cl">颜色</param>
        /// <param name="Threshold">阈值</param>
        /// <returns></returns>
        public static Color CleanA(Color cl, byte Threshold)
        {
            if (cl.A < Threshold)
                return cl;
            return Color.FromArgb(255, cl);
        }
        /// <summary>
        /// 指定颜色变透明
        /// </summary>
        /// <param name="cl">颜色</param>
        /// <param name="Specify">指定的颜色</param>
        /// <returns></returns>
        public static Color CleanC(Color cl, Color Specify)
        {
            if (cl.A == Specify.A && cl.R == Specify.R && cl.G == Specify.G && cl.B == Specify.B)
                return Color.FromArgb(0, cl);
            return cl;
        }
        /// <summary>
        /// 指定颜色变透明
        /// </summary>
        /// <param name="cl">颜色</param>
        /// <param name="Specify">指定的颜色</param>
        /// <param name="deviation">误差</param>
        /// <returns></returns>
        public static Color CleanCdeviation(Color cl, Color Specify, int deviation)
        {
            if (Math.Abs(cl.A - Specify.A) + Math.Abs(cl.R - Specify.R) + Math.Abs(cl.G - Specify.G) + Math.Abs(cl.B - Specify.B) < deviation)
                return Color.FromArgb(0, cl);
            return cl;
        }
        /// <summary>
        /// 透明变指定颜色
        /// </summary>
        /// <param name="cl">颜色</param>
        /// <param name="Specify">指定的颜色</param>
        /// <returns></returns>
        public static Color SwitchA(Color cl, Color Specify)
        {
            if (cl.A == 0)
                return Specify;
            return cl;
        }
        public static byte TurnDarkerByte(byte color) => (byte)(255 * Math.Pow((color / 255.0), 2));
        public static byte TurnLighterByte(byte color) => (byte)(255 * Math.Sqrt((color / 255.0)));

        /// <summary>
        /// 将颜色变得更深
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>更深的颜色</returns>
        public static Color TurnDarker(Color color) => Color.FromArgb(color.A, TurnDarkerByte(color.R), TurnDarkerByte(color.G), TurnDarkerByte(color.B));
        /// <summary>
        /// 将颜色变得更亮
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>更亮的颜色</returns>
        public static Color TurnLighter(Color color) => Color.FromArgb(color.A, TurnLighterByte(color.R), TurnLighterByte(color.G), TurnLighterByte(color.B));
        /// <summary>
        /// 修改图片大小
        /// </summary>
        /// <param name="bmp">图片</param>
        /// <param name="newW">宽</param>
        /// <param name="newH">高</param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Bitmap bmp, int newW, int newH)
        {
            Bitmap b = new Bitmap(newW, newH);
            Graphics g = Graphics.FromImage(b);
            // 插值算法的质量 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.Dispose();
            return b;
        }

        /// <summary>
        /// 从指定位置加载图片 不占用图片
        /// </summary>
        /// <param name="Path">图片位置</param>
        /// <returns>图片</returns>
        public static Bitmap ImageFromFile(string Path) => new Bitmap(new MemoryStream(File.ReadAllBytes(Path)));
    }
}
