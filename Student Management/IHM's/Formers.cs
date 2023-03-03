using log4net;
using Student_Management.Modules.Config;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using Student_Management.Modules.UserModel.Model;
using System;
using System.Windows.Forms;

namespace Student_Management.IHM_s
{
    public partial class Formers : Form
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(Formers));
        public bool Access = true;
        public UsersController UserController;
        public ResponseStatus ResponseStatus;
        public Control[] AllFormersControls;
        public Control[] CreateFormersControls;

        public Formers()
        {
            InitializeComponent();
            UserController = new UsersController();
            ResponseStatus = new ResponseStatus();
            InitializeFormers();
            AllFormersControls = new Control[]
            {
                this.AllFormers,
                this.guna2DataGridView1
            };
            CreateFormersControls = new Control[]
            {
                this.CreateFormers,
                this.label2,
                this.label3,
                this.label4,
                this.label5,
                this.label6,
                this.label7,
                this.label9,
                this.label10,
                this.label11,
                this.guna2Button4,
                this.guna2Button5,
                this.NameField,
                this.PassworField,
                this.ConfirmPasswordField,
                this.PhoneField,
                this.YearField,
                this.MonthField,
                this.DayField,
                this.CityField,
                this.FormerTpeField,
                this.UserRoleField
            };

            toggleControls(CreateFormersControls, false);
        }

        private void toggleControls(Control[] controls, bool toggle)
        {
            if(toggle == true)
            {
                foreach(Control control in controls) 
                {
                    control.Show();
                }
            }
            else
            {
                foreach (Control control in controls)
                {
                    control.Hide();
                }
            }
        }

        private void InitializeFormers()
        {
            label1.Text = $"{Program.CurrentDate()}";
            this.guna2DataGridView1.RowTemplate.Height = 40;
            this.CreateFormers.Hide();
            ListOfUsers(Access);
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
        private void ListOfUsers(bool redirect)
        {
            if (!redirect)
                return;

            foreach (Users Rows in UserController.AllUsers())
            {
                this.guna2DataGridView1.Rows.Add(Rows.Matricule, Rows.Name, Rows.Phone, Rows.DateNaissance, Rows.FormerType);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            int Position = this.guna2DataGridView1.CurrentRow.Index;
            string Matricule = this.guna2DataGridView1.Rows[Position].Cells[0].Value.ToString();

            MessageBox.Show($"Matricule : {Matricule}");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            toggleControls(AllFormersControls, false);
            toggleControls(CreateFormersControls, true);
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            toggleControls(AllFormersControls, true);
            toggleControls(CreateFormersControls, false);
        }
    }
}
