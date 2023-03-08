using log4net;
using Student_Management.Modules.LoggerManager;
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

namespace Student_Management.IHM_s.ComposentsGraphique.Formers
{
    public partial class ShowFormerDetails : UserControl
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(ShowFormerDetails));
        public string FormerMatricule = String.Empty;
        public ShowFormerDetails(Users user)
        {
            InitializeComponent();
            FormerMatricule = user.Matricule.ToString();
            SetFormerInformation(user);
        }

        private void SetFormerInformation(Users user)
        {
            this.Matricule.Text = user.Matricule.ToString();
            this.UserName.Text = user.Name.ToString();
            this.Password.Text = HashPassword(12);
            this.Phone.Text = user.Phone.ToString();
            this.DateNaissance.Text = user.DateNaissance.ToString();
            this.Age.Text = user.Age.ToString();
            this.FormerType.Text = user.FormerType.ToString();
            this.UserType.Text = user.FormerType.ToString();
        }

        private string HashPassword(int Lenght)
        {
            string Password = string.Empty;
            for(int i = 0; i  < Lenght; i++)
            {
                Password = "**********";
            }
            return Password;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //delete
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Update
        }
    }
}
