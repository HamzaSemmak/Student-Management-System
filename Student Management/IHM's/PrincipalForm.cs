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
        public bool Access = true;
        public UsersController UserController;
        public ResponseStatus ResponseStatus;
        public int UserID;
        public string UserName;
        public string UserFormerType;

        public PrincipalForm()
        {
            InitializeComponent();
            UserController = new UsersController();
            ResponseStatus = new ResponseStatus();
            UserID = Convert.ToInt32(UserController.GetUserID());
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
            label1.Text = $"{Program.CurrentDate()}";
            this.guna2DataGridView1.RowTemplate.Height = 40;
            ListOfUsers(Access);
            InitializeInformation(Access);
        }

        private void ListOfUsers(bool redirect)
        {
            if (!redirect)
                return;

            foreach (Users Rows in UserController.GetUsersWithLimit(7))
            {
                this.guna2DataGridView1.Rows.Add(Rows.Matricule, Rows.Name, Rows.FormerType, Rows.Phone);
            }
        }

        private void InitializeInformation(bool redirect)
        {
            if(!redirect)
                return;

            //Hide Button View All formers && Check Type Of Uesr
            guna2Button4.Hide();
            CheckUserIfAdmin();

            //User Locked UserName && Type Former :
            label2.Text = $"{UserController.UserLocked(UserID)[0]}";
            label3.Text = $"{UserController.UserLocked(UserID)[1]}";

            //Count Of Formers :
            label5.Text = $"{UserController.GetNumbersOfUser()}";
            FormersProgressBar.Value = PercentOfMembers(UserController.GetNumbersOfUser());

            //Formers With Types : 
            foreach(Users Item in UserController.GetUserByColumn("FormerType", "Directeur of School"))
            {
                label11.Text = $"{Item.Name}";
                label12.Text = $"{Item.FormerType}";
            }

            foreach (Users Item in UserController.GetUserByColumn("FormerType", "Director assistance"))
            {
                label14.Text = $"{Item.Name}";
                label13.Text = $"{Item.FormerType}";
            }

            foreach (Users Item in UserController.GetUserByColumn("FormerType", "General guard"))
            {
                label16.Text = $"{Item.Name}";
                label15.Text = $"{Item.FormerType}";
            }

            foreach (Users Item in UserController.GetUserByColumn("FormerType", "Teacher"))
            {
                label18.Text = $"{Item.Name}";
                label17.Text = $"{Item.FormerType}";
            }
        }

        private void CheckUserIfAdmin()
        {
            try
            {
                int status = UserController.CheckUserIfAdmin(UserID);
                if(status == ResponseStatus.ResponseCodeUserIsUndefiend)
                {
                    logger.Error("User is Undefiened Please Check Again");
                }
                else if(status == ResponseStatus.ResponseCodeUserIsAdmin)
                {
                    guna2Button4.Show();
                    logger.Info("Type of User is Admin...");
                }
                else if(status == ResponseStatus.ResponseCodeUserIsNotAdmin)
                {
                    guna2Button4.Hide();
                    logger.Info("Type of User is Normal User...");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error : {ex.Message} ");
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LaunchFormerForm();
        }
    }
}