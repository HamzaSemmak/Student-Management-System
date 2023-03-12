using Guna.UI2.WinForms;
using log4net;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.StudentModel.Controller;
using Student_Management.Modules.StudentModel.Model;
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
    public partial class StudentsForm : Form
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(StudentsForm));
        public StudentsController StudentsController;
        public bool Access = true;
        public StudentsForm()
        {
            InitializeComponent();
            StudentsController = new StudentsController();
            ListOfUsers(Access);
            InitializeStudents();
        }

        private void ListOfUsers(bool redirect)
        {
            if (!redirect)
                return;

            this.guna2DataGridView1.Rows.Clear();
            foreach (Students Rows in StudentsController.AllStudents())
            {
                this.guna2DataGridView1.Rows.Add(Rows.Matricule, Rows.Name, Rows.DateNaissance, Rows.Age, Rows.City);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LaunchStudentsForm();
        }

        private void InitializeStudents()
        {
            this.guna2DataGridView1.RowTemplate.Height = 40;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LaunchPrincipalForm();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }
    }
}
