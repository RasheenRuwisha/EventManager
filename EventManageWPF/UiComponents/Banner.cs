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
    public partial class Banner : UserControl
    {
        public Banner()
        {
            InitializeComponent();
        }

        public String Title
        {
            set
            {
                this.lbl_title.Text = value;
            }
        }

        public String Text
        {
            set
            {
                this.lbl_txt.Text = value;
            }
        }

        public Color ForeGround
        {
            set
            {
                this.lbl_title.ForeColor = value;
                this.lbl_txt.ForeColor = value;
            }
        }

        public Color BackGround
        {
            set
            {
                this.lbl_title.BackColor = value;
                this.lbl_txt.BackColor = value;
            }
        }
    }
}
