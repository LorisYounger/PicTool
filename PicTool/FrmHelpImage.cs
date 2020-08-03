using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicTool
{
    public partial class FrmHelpImage : Form
    {
        public FrmHelpImage(Image img)
        {
            InitializeComponent();
            pictureBoxHelpImage.Image = img;
        }

        private void pictureBoxHelpImage_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
