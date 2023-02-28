using log4net;
using log4net.Repository.Hierarchy;
using Student_Management.Modules.Config;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using Student_Management.Modules.UserModel.Model;
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
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(PrincipalForm));
        public static DateTime dateTime = DateTime.Now;
        public UsersController User;
        public ResponseStatus ResponseStatus;
        public int UserID;

        public PrincipalForm()
        {
            InitializeComponent();
            User = new UsersController();
            ResponseStatus = new ResponseStatus();
            InitializePrincipalForm();
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

        private void InitializePrincipalForm()
        {
            label1.Text = $"{dateTime.DayOfWeek}, {dateTime.Day} {dateTime.ToString("MMMM")} {dateTime.Year}";
            UserID = Convert.ToInt32(User.GetUserID());
            //User Locked UserName && Type Former :
            label2.Text = $"{User.UserLocked(UserID)[0]}";
            label3.Text = $"{User.UserLocked(UserID)[1]}";
            //Count Of Formers :
            label5.Text = $"{User.GetNumbersOfUser()}";
            FormersProgressBar.Value = PercentOfMembers(User.GetNumbersOfUser());
            this.guna2DataGridView1.RowTemplate.Height = 40;
            ListOfUsers(true);
        }

        private void ListOfUsers(bool redirect)
        {
            if (!redirect)
                return;

            foreach (Users Rows in User.GetUsersWithLimit(7))
            {
                this.guna2DataGridView1.Rows.Add(Rows.Matricule, Rows.Name, Rows.FormerType, Rows.Phone);
            }
        }
        private int PercentOfMembers(int Count)
        {
            int Percent = 0;
            if (Count > 0 && Count < 25)
                Percent = 25;
            else if (Count > 25 && Count < 50)
                Percent = 45;
            else if (Count > 50 && Count < 75)
                Percent = 65;
            else if (Count > 75 && Count <= 100)
                Percent = 80;
            else if (Count > 100)
                Percent = 95;

            return Percent;
        }

    }
}