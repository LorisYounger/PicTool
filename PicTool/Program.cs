using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Start:
            try
            {
                Application.Run(new FrmMain());
            }
            catch (Exception e)
            {
                Log.AppendLine("StackTrace:" + e.StackTrace);
                Log.AppendLine("TargetSite:" + e.TargetSite.Name);
                Log.AppendLine("Source:" + e.Source);
                Log.AppendLine("Message:" + e.Message);
                if (new FrmDebug(Log.ToString()).ShowDialog() == DialogResult.Retry)
                    goto Start;
            }


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
