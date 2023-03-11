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

namespace Student_Management.IHM_s.ComposentsGraphique.Formers
{
    public partial class ShowFormerDetails : UserControl
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(ShowFormerDetails));
        public string FormerMatricule = String.Empty;
        public FormersController FormersController;
        public UsersController UserController;
        public ResponseStatus ResponseStatus;
        public UpdateFormers UpdateFormers;

        public ShowFormerDetails(Users user)
        {
            InitializeComponent();
            FormerMatricule = user.Matricule.ToString();
            FormersController = new FormersController();
            UserController = new UsersController();
            ResponseStatus = new ResponseStatus();
            UpdateFormers = new UpdateFormers(FormerMatricule);
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
            if (!(sender as Control).Enabled)
                return;

            DeleteFormer();
        }
        public void DeleteFormer()
        {
            int index, status;
            string message;
            string Msg = "Do you really want to delete this former.";
            using (ConfirmExceptionForm _ConfirmExceptionForm = new ConfirmExceptionForm(Msg))
            {
                _ConfirmExceptionForm.ShowDialog();
                if (_ConfirmExceptionForm.delete == 0)
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }
            }

            if (index == 1)
            {
                try
                {
                    status = FormersController.DeleteFormer(FormerMatricule);
                    if (status == ResponseStatus.ResponseCodeDeleteFormerAuthentificate)
                    {
                        message = "Former is Deleted Successfly.";
                        Program.LaunchHandleExceptionForm(message);
                        this.Hide();
                        Program.LaunchAuthentification();
                    }
                    else if (status == ResponseStatus.ResponseCodeFormerIsUndefiened)
                    {
                        message = "Former is Undefiened, Please try again.";
                        Program.LaunchUnhandleExceptionForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodeDeleteFormer)
                    {
                        message = "Former is Deleted Successfly.";
                        logger.Info($"Success : {message} ");
                        hideFormAndControllUser();
                    }
                }
                catch(Exception ex)
                {
                    logger.Error($"Error : {ex.Message} ");
                }
                finally
                {
                    Program.LaunchFormerForm();
                }
            }
        }

        private void hideFormAndControllUser()
        {
            Form _F = this.FindForm();
            _F.Close();
            _F.Dispose();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Update
            guna2Panel1.Controls.Clear();
            guna2Panel1.Controls.Add(UpdateFormers);
        }
    }
}
