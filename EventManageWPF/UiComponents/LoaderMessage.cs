using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManageWPF.UiComponents
{
    public partial class LoaderMessage : UserControl
    {
        public LoaderMessage()
        {
            InitializeComponent();
        }

        public String LabelText
        {
            set
            {
                this.lbl_txt.Text = value;
            }
        }

        public Image ImagePic
        {
            set
            {
                this.pictureBox1.Image = value;
            }
        }
    }
}
