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
        public int UserID;

        public PrincipalForm()
        {
            InitializeComponent();
            User = new UsersController();
            ResponseStatus = new ResponseStatus();
            OnLoad();
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

        private void OnLoad()
        {
            label1.Text = $"{dateTime.DayOfWeek}, {dateTime.Day} {dateTime.ToString("MMMM")} {dateTime.Year}";
            UserID = Convert.ToInt32(User.GetUserID());
            label2.Text = $"{User.UserLocked(UserID)[0]}";
            label3.Text = $"{User.UserLocked(UserID)[1]}";
            this.guna2DataGridView1.RowTemplate.Height = 40;
            User.GetUsersWithLimit(guna2DataGridView1, 7);
        }
    }
}