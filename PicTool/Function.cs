using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 颜色转HEX值
        /// </summary>
        /// <param name="Color">颜色</param>
        /// <returns></returns>
        public static string ColorToHEX(Color Color)
        {
            return (Color.R * 65536 + Color.G * 256 + Color.B).ToString("x").PadLeft(6, '0');
        }
        /// <summary>
        /// 颜色转AHEX值
        /// </summary>
        /// <param name="Color">颜色</param>
        /// <returns></returns>
        public static string ColorToAHEX(Color Color)
        {
            return (Color.A * 33554432 + Color.R * 65536 + Color.G * 256 + Color.B).ToString("x").PadLeft(8, '0');
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
    }
}
