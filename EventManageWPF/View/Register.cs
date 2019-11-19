using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManageWPF.Util;
using EventManageWPF.UiComponents;

namespace EventManageWPF.View
{
    public partial class Register : Form
    {
        bool focus = false;
        User userModel = new User();
        UsersCredential usersCredentialModel = new UsersCredential();

        EventManagerEntities db = new EventManagerEntities();
        ErrorHandling errorHandling = new ErrorHandling();
        public Register()
        {
            InitializeComponent();

            this.logo.Image = Properties.Resources.logoo;
            this.logo.AutoSize = false;
            this.logo.Left = this.Width / 2 - this.logo.Width / 2;

            this.txt_username.AutoSize = false;
            this.txt_username.Size = new System.Drawing.Size(250, 30);

            this.txt_email.AutoSize = false;
            this.txt_email.Size = new System.Drawing.Size(250, 30);

            this.txt_firstname.AutoSize = false;
            this.txt_firstname.Size = new System.Drawing.Size(250, 30);

            this.txt_lastname.AutoSize = false;
            this.txt_lastname.Size = new System.Drawing.Size(250, 30);

            this.txt_password.AutoSize = false;
            this.txt_password.Size = new System.Drawing.Size(250, 30);


            this.txt_confirmpassword.AutoSize = false;
            this.txt_confirmpassword.Size = new System.Drawing.Size(250, 30);
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    base.OnClosing(e);
        //    Application.Exit();
        //}
        private void lbl_login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            LoaderMessage loader = new LoaderMessage();
            loader.LabelText = "Creating Account";
            loader.Name = "loader";
            loader.ImagePic = Properties.Resources.loader;
            this.Controls.Add(loader);
            LoaderMessage load = Controls.Find("loader", true).FirstOrDefault() as LoaderMessage;
            load.BringToFront();
            loader.Location = new Point(this.Width / 2 - loader.Width / 2, this.Height / 2 - loader.Height / 2);
            this.btn_register.Enabled = false;
            String t = await Task.Run(() => this.addToDatabase());
            this.btn_register.Enabled = true;
            this.Controls.Remove(loader);
            if (t.Equals("success"))
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }

        }

        private String addToDatabase()
        {
            if (this.doValidate())
            {


                userModel.username = txt_username.Text.Trim();
                userModel.email = txt_email.Text.Trim();
                userModel.firstname = txt_firstname.Text.Trim();
                userModel.lastname = txt_lastname.Text.Trim();

                usersCredentialModel.email = txt_email.Text.Trim();
                usersCredentialModel.password = txt_password.Text.Trim();
                usersCredentialModel.username = txt_username.Text.Trim();

                if (db.Users.Find(txt_email.Text.Trim()) == null)
                {
                    db.Users.Add(userModel);
                    db.UsersCredentials.Add(usersCredentialModel);
                    db.SaveChanges();
                    return "success";
                }
                else
                {
                    MessageBox.Show("User with email Already Exists");
                    return "faliure";

                }

            }
            else
            {
                return "faliure";

            }
        }

        private Boolean validateFields()
        {
            this.showErrors();
            if(txt_username.Text.Trim().Equals("") || txt_email.Text.Trim().Equals("") ||
               txt_firstname.Text.Trim().Equals("") || txt_lastname.Text.Trim().Equals("") ||
               txt_password.Text.Trim().Equals("") || txt_confirmpassword.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please Fill All Requried Fields!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private Boolean validatePassword()
        {
            if (txt_password.Text.Trim().Equals(txt_confirmpassword.Text.Trim()) && !txt_password.Text.Trim().Equals("")
                && !txt_confirmpassword.Text.Trim().Equals(""))
            {
                PictureBox pictureBox = Controls.Find("ptx_" + txt_confirmpassword.Name, true).FirstOrDefault() as PictureBox;
                Controls.Remove(pictureBox);
                return true;
            }
            else
            {
                PictureBox error = errorHandling.addPasswordErrorIcon(txt_confirmpassword.Name, txt_confirmpassword.Location.X + 255, txt_confirmpassword.Location.Y + 2);
                this.Controls.Add(error);
                MessageBox.Show("Passwords do not match!");
                return false;
            }
        }

        private void showErrors()
        {
            foreach (Control contorls in this.Controls)
            {
                if (contorls is TextBox)
                {
                    if (contorls.Text.Trim().Equals(""))
                    {
                        PictureBox pictureBox = Controls.Find("ptx_" + contorls.Name, true).FirstOrDefault() as PictureBox;
                        if(pictureBox == null)
                        {
                            PictureBox error = errorHandling.addErrorIcon(contorls.Name, contorls.Location.X + 255, contorls.Location.Y + 2);
                            this.Controls.Add(error);    
                        }
                    }
                    else
                    {
                        PictureBox pictureBox = Controls.Find("ptx_" + contorls.Name, true).FirstOrDefault() as PictureBox;
                        Controls.Remove(pictureBox);
                    }

                }

            } 
        }

        private Boolean doValidate()
        {
            if (this.validateFields())
            {
                if (this.validatePassword())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        


    }
}
