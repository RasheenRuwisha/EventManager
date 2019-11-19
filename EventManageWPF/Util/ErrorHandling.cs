using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManageWPF.Util
{
    class ErrorHandling
    {

        public PictureBox addErrorIcon(string name, int x, int y)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(x, y);
            picture.Name = "ptx_" + name;
            picture.Size = new Size(25, 25);
            picture.BackColor = Color.Transparent;
            picture.Image = Properties.Resources.erroricon;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            return picture;
        }

        public PictureBox addPasswordErrorIcon(string name, int x, int y)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(x, y);
            picture.Name = "ptx_" + name;
            picture.Size = new Size(25, 35);
            picture.BackColor = Color.Transparent;
            picture.Image = Properties.Resources.passworderror;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            return picture;
        }


    }
}
