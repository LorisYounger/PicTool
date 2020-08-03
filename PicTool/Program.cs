using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());

        }

        public static readonly StringBuilder Log = new StringBuilder();

        /// <summary>
        /// 定义版本号
        /// </summary>
        public const string Version = "1.0";

        /// <summary>
        /// 判断是否是steam用户
        /// </summary>
        public static bool IsSteamUser = false;


    }
}
