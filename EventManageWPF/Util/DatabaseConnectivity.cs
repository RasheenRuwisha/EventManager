using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManageWPF.Util
{
    class DatabaseConnectivity
    {
        public async Task connectionValidator()
        {
            String t = await Task.Run(() => this.checkConnection());
        }

        public String checkConnection()
        {
            try
            {
                EventManagerEntities db = new EventManagerEntities();
                db.Database.Connection.Open();
                db.Database.Connection.Close();
                return "success";
            }
            catch (Exception ex)
            {
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
                notifyIcon.Visible = true;
                notifyIcon.Text = "Event Manager";
                notifyIcon.BalloonTipText = "The database connection could not be established.";
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipTitle = "Test App Installation";
                notifyIcon.ShowBalloonTip(10000);
                return "false";
            }
        }
    }
}
