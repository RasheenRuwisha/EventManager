using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EventManageWPF.View
{
    public partial class Login : Form
    {

        User userModel = new User();
        UsersCredential usersCredentialModel = new UsersCredential();

        EventManagerEntities db = new EventManagerEntities();
        public Login()
        {
            InitializeComponent();
            this.logo.Image = Properties.Resources.logoo;
            this.logo.AutoSize = false;
            this.logo.Left = this.Width / 2 - this.logo.Width / 2;

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);
            this.txt_email.Left = this.Width / 2 - this.txt_email.Width / 2;

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);
            this.txt_password.Left = this.Width / 2 - this.txt_password.Width / 2;

            this.btn_login.Left = this.Width / 2 - this.btn_login.Width / 2;
            this.lbl_email.Left = (this.Width / 2) - 3 - this.txt_email.Width / 2;
            this.lbl_password.Left = (this.Width / 2) - 3 - this.txt_email.Width / 2;

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            this.Close();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
           UsersCredential userCredential =  db.UsersCredentials.Find(txt_email.Text.Trim());
           if(userCredential != null)
            {
                if (txt_password.Text.Trim().Equals(userCredential.password))
                {
                    Application.UserAppDataRegistry.SetValue("email", userCredential.email);
                    Application.UserAppDataRegistry.SetValue("password", userCredential.password);
                    Application.UserAppDataRegistry.SetValue("remeberMe", true);

                    ForgotPassword forgotPassword = new ForgotPassword();
                    forgotPassword.Show();
                    this.Close();
                }
            }
        }
    }
}
