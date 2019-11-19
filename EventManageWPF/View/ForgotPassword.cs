using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManageWPF.Util;
using EventManageWPF.UiComponents;

namespace EventManageWPF.View
{
    public partial class ForgotPassword : Form
    {
        User userModel = new User();
        UsersCredential usersCredentialModel = new UsersCredential();

        EventManagerEntities db = new EventManagerEntities();

        ErrorHandling errorHandling = new ErrorHandling();
        CommonUtil commonUtil = new CommonUtil();
        String otp;
        public ForgotPassword()
        {
            InitializeComponent();

            this.logo.Image = Properties.Resources.logoo;
            this.logo.AutoSize = false;
            this.logo.Left = this.Width / 2 - this.logo.Width / 2;

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);

            this.txt_confirmpasword.AutoSize = false;
            this.txt_confirmpasword.Size = new System.Drawing.Size(250, 30);

            this.txt_otp.AutoSize = false;
            this.txt_otp.Size = new System.Drawing.Size(250, 30);

            this.email_panel.BringToFront();
        }

        private async void btn_send_mail_Click(object sender, EventArgs e)
        {
            if (txt_email.Text.Trim().Equals(""))
            {
                PictureBox pictureBox = Controls.Find("ptx_" + this.txt_email, true).FirstOrDefault() as PictureBox;
                if (pictureBox == null)
                {
                    PictureBox error = errorHandling.addErrorIcon(this.txt_email.Name, this.txt_email.Location.X + 255, this.txt_email.Location.Y + 2);
                    this.Controls.Add(error);
                }
            }
            else
            {
                this.otp = commonUtil.generateOTP();
                String t = await Task.Run(() => this.sendEmail());
                if (t.Equals("success"))
                {
                    Banner banner = new Banner();
                    banner.Dock = DockStyle.Top;
                    banner.BackColor = Color.FromArgb(212, 237, 218);
                    banner.ForeGround = Color.FromArgb(21, 87, 36);
                    banner.BackGround = Color.FromArgb(212, 237, 218);
                    banner.Title = "OTP Sent";
                    banner.Text =  $"Email has been sent to {txt_email.Text.Trim()}, it may take a few minutes!";
                    this.verification_panel.Controls.Add(banner);
                    this.verification_panel.BringToFront();
                }
                else
                {
                    Banner banner = new Banner();
                    banner.Dock = DockStyle.Top;
                    banner.BackColor = Color.FromArgb(248, 215, 218);
                    banner.ForeGround = Color.FromArgb(114, 28, 36);
                    banner.BackGround = Color.FromArgb(248, 215, 218);
                    banner.Title = "Error";
                    banner.Text = "Unable to send email! Please try again later!";
                    this.email_panel.Controls.Add(banner);
                }
            }
        }

        private void txt_proceed_Click(object sender, EventArgs e)
        {
            if (this.txt_otp.Text.Trim().Equals(this.otp))
            {
                this.reset_panel.BringToFront();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (this.validatePassword())
            {
                UsersCredential usersCredential = new UsersCredential();
                usersCredential = db.UsersCredentials.Find(this.txt_email.Text.Trim().ToLower());
                usersCredential.password = txt_password.Text.Trim();
                db.SaveChanges();
                Application.UserAppDataRegistry.SetValue("remeberMe", false);
                Application.UserAppDataRegistry.SetValue("username", "");
                Application.UserAppDataRegistry.SetValue("password","");
                Login login = new Login();
                login.Show();
                this.Close();
            }
 
        }

        private String sendEmail()
        {
            try
            {
                MailSender mailSender = new MailSender();
                mailSender.SendEmail(this.txt_email.Text, this.otp);               
                return "success";
            }
            catch (Exception ex)
            {
                return "faliure";
            }
        }

        private Boolean validatePassword()
        {
            if (txt_password.Text.Trim().Equals(txt_confirmpasword.Text.Trim()) && !txt_password.Text.Trim().Equals("")
                && !txt_confirmpasword.Text.Trim().Equals(""))
            {
                PictureBox pictureBox = Controls.Find("ptx_" + txt_confirmpasword.Name, true).FirstOrDefault() as PictureBox;
                Controls.Remove(pictureBox);
                return true;
            }
            else
            {
                PictureBox error = errorHandling.addPasswordErrorIcon(txt_confirmpasword.Name, txt_confirmpasword.Location.X + 255, txt_confirmpasword.Location.Y + 2);
                this.Controls.Add(error);
                MessageBox.Show("Passwords do not match!");
                return false;
            }
        }

        private void lbl_login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
