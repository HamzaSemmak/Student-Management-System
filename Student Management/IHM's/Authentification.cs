using log4net;
using Student_Management.Modules.Config;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using System;
using System.Windows.Forms;

namespace Student_Management.IHM_s
{
    public partial class Authentification : Form
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(Program));
        public UsersController User;
        public ResponseStatus ResponseStatus;
        public Authentification()
        {
            InitializeComponent();
            User = new UsersController();
            ResponseStatus = new ResponseStatus();
            label2.Text = $"CopyRight © {DateTime.Now.Year} All Right Reserved";
            guna2TextBox1.Select();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            authentification();
        }

        private void authentification()
        {
            string message = string.Empty;
            try
            {
                if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
                {
                    message = "There is an Empty field";
                    Program.LaunchAlertForm(message);
                }
                else
                {
                    int status = User.Authentification(guna2TextBox1.Text, guna2TextBox2.Text);
                    if (status == ResponseStatus.ResponseCodeIncorrectUserName)
                    {
                        message = $"Error {status} : UserName is incorrect, Please try again";
                        Program.LaunchAlertForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodeIncorrectPassword)
                    {
                        message = $"Error {status} : Password is is incorrect, Please try again";
                        Program.LaunchAlertForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodeLockedAccount)
                    {
                        message = $"Error {status} : Your account is locked, contact your administrator";
                        Program.LaunchAlertForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodeAuth)
                    {
                        this.Hide();
                        Program.LaunchPrincipalForm();
                        logger.Info("Logged Successfly, Information : \n{Info: " + status + ", UserName: " + guna2TextBox1.Text + ", Password : " + guna2TextBox2.Text + "}");
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Program.LaunchAlertForm(ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }
    }
}