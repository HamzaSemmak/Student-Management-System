using log4net;
using log4net.Repository.Hierarchy;
using Student_Management.Modules.Config;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management.IHM_s
{
    public partial class PrincipalForm : Form
    {
        public static DateTime dateTime = DateTime.Now;
        public UsersController User;
        public ResponseStatus ResponseStatus;

        public PrincipalForm()
        {
            InitializeComponent();
            User = new UsersController();
            ResponseStatus = new ResponseStatus();
            label1.Text = $"{dateTime.DayOfWeek}, {dateTime.Day} {dateTime.ToString("MMMM")} {dateTime.Year}";
            label2.Text = $"{User.GetUserInformation()[0]}";
            label3.Text = $"{User.GetUserInformation()[1]}";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LaunchPrincipalForm();
        }
    }
}