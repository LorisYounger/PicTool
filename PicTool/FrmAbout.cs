using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiLang;
using Steamworks;
using static PicTool.Program;

namespace PicTool
{
    public partial class FrmAbout : Form
    {
        LangForm Lang;
        public FrmAbout(LangForm lang)
        {            
            InitializeComponent();
            Lang = lang;
            lang.Translate(this);
            if (IsSteamUser)
            {
                labelthanks.Text = Lang.Translate("感谢: ") + SteamClient.Name;
            }
            else
            {
                labelthanks.Text = Lang.Translate("感谢: ") + Environment.UserName;
            }
        }

        private void linkLabelContributor_Click(object sender, EventArgs e)
        {
            linkLabelContributor.LinkVisited = true;
            Process.Start("https://github.com/LorisYounger/PicTool/graphs/contributors");
        }

        private void linkLabelCopyRight_Click(object sender, EventArgs e)
        {
            linkLabelCopyRight.LinkVisited = true;
            Process.Start("https://exlb.org/");
        }

        private void linkLabelGithub_Click(object sender, EventArgs e)
        {
            linkLabelGithub.LinkVisited = true;
            Process.Start("https://github.com/LorisYounger/PicTool/");
        }

        private void linkLabelSteam_Click(object sender, EventArgs e)
        {
            linkLabelSteam.LinkVisited = true;
            Process.Start("https://store.steampowered.com/app/1381380/PicTool/");
        }
    }
}
