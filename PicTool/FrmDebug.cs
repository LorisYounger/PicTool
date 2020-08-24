using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicTool
{
    public partial class FrmDebug : Form
    {
        public FrmDebug(string Log)
        {
            InitializeComponent();
            textBoxLog.Text = Log;
            textBoxLog.Select(Log.Length, 0);

            LogPath = Application.StartupPath + $"\\{DateTime.Now:yyMMddhhmmss}.log";
            //储存错误日志
            File.WriteAllText(LogPath, Log);
        }
        public string LogPath;
        /// <summary>
        /// 是否上传错误信息
        /// </summary>
        public bool Upload
        {
            get => DialogResult != DialogResult.Cancel && checkBoxUpload.Checked;
        }

        private void FrmDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Upload)
            {
                Process.Start("https://connect.exlb.org/?type=error&product=PicTool");
                Process.Start("notepad.exe", LogPath);
            }
        }
    }
}
