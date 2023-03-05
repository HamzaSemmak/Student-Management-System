using log4net;
using Student_Management.IHM_s.ComposentsGraphique.Formers;
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
        public CreateFormer CreateFormer;

        public Formers()
        {
            InitializeComponent();
            UserController = new UsersController();
            ResponseStatus = new ResponseStatus();
            CreateFormer = new CreateFormer();
            InitializeFormers();
        }

        private void InitializeFormers()
        {
            label1.Text = $"{Program.CurrentDate()}";
            this.guna2DataGridView1.RowTemplate.Height = 40;
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

        private void ThrowUserControl(Control control)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(control);
            control.Dock = DockStyle.Fill; 
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ThrowUserControl(CreateFormer);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ThrowUserControl(AllFormers);
        }

        private void guna2DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            int Position = this.guna2DataGridView1.CurrentRow.Index;
            string Matricule = this.guna2DataGridView1.Rows[Position].Cells[0].Value.ToString();

            MessageBox.Show($"Matricule : {Matricule}");
        }
    }
}
