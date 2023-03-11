using log4net;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Student_Management.IHM_s.ComposentsGraphique.Formers
{
    public partial class UpdateFormers : UserControl
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(CreateFormer));
        public UsersController UserController;
        public ResponseStatus ResponseStatus;
        public FormersController FormersController;
        public Control[] AllFields;
        public string FormerMatricule = String.Empty;

        public UpdateFormers(string Matricule)
        {
            InitializeComponent();
            FormerMatricule = Matricule;
            UserController = new UsersController();
            ResponseStatus = new ResponseStatus();
            FormersController = new FormersController();
            InitFormersInformation(FormerMatricule);
            AllFields = new Control[]
            {
                NameField,
                PassworField,
                ConfirmPasswordField,
                PhoneField,
                CityField,
                FormerTpeField,
                UserRoleField
            };
            GetAllFormerTpeField();
            GetUserRole();
        }

        private void InitFormersInformation(string Matricule)
        {
            List<Users> user = FormersController.getFormerByMatricule(Matricule);
            NameField.Text = user[0].Name;
            PassworField.Text = user[0].Password;
            ConfirmPasswordField.Text = user[0].Password;
            PhoneField.Text = user[0].Phone;
            CityField.Text = user[0].City;
            FormerTpeField.Text = user[0].FormerType;
            UserRoleField.Text = user[0].UserType;
        }

        private void GetAllFormerTpeField()
        {
            foreach (string item in FormersController.getAllFormersType())
            {
                FormerTpeField.Items.Add(item);
            }
        }

        private void GetUserRole()
        {
            UserRoleField.Items.Add("User");
            UserRoleField.Items.Add("Admin");
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            InitFormersInformation(FormerMatricule);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            UpdateFormer();
        }

        private void UpdateFormer()
        {
            int status;
            try
            {
                string message;
                if (NameField.Text == "" || ConfirmPasswordField.Text == "" || PassworField.Text == ""
                        || PhoneField.Text == "" || CityField.Text == "" 
                        || FormerTpeField.Text == "" || FormerTpeField.Text == ""
                    )
                {
                    message = "Ther is an Empty Field, Please Try Again";
                    Program.LaunchUnhandleExceptionForm(message);
                }
                else if (ConfirmPasswordField.Text != PassworField.Text)
                {
                    message = "Password and Confirm Password dons't match, Please Try Again";
                    Program.LaunchUnhandleExceptionForm(message);
                }
                else if (ConfirmPasswordField.TextLength > 10 || PassworField.TextLength > 10)
                {
                    message = "Password length must not be bigger than 10 characters, Please Try Again";
                    Program.LaunchUnhandleExceptionForm(message);
                }
                else if (ConfirmPasswordField.TextLength < 6 || PassworField.TextLength < 6)
                {
                    message = "Password length must be bigger than 6 characters, Please Try Again";
                    Program.LaunchUnhandleExceptionForm(message);
                }
                else
                {
                    Users users = new Users()
                    {
                        /* Matricule => GenerateMatricule */
                        Matricule = "",
                        /* Name => */
                        Name = Convert.ToString(NameField.Text),
                        /* Password => */
                        Password = Convert.ToString(PassworField.Text),
                        /* Phone => */
                        Phone = Convert.ToString(PhoneField.Text),
                        /* DateNaissance => */
                        DateNaissance = "",
                        /* Age => */
                        Age = 0,
                        /* city => */
                        City = Convert.ToString(CityField.Text),
                        /* FormerType => */
                        FormerType = Convert.ToString(FormerTpeField.Text),
                        /* UserType => */
                        UserType = Convert.ToString(UserRoleField.Text),

                    };
                    status = FormersController.UpdateFormer(users, FormerMatricule);
                    if (status == ResponseStatus.ResponseCodeFormerIsUpdatedSuccessfly)
                    {
                        message = "Success : Former Updated Succefly";
                        Program.LaunchHandleExceptionForm(message);
                    }
                    else
                    {
                        message = "Error : there is somethingwrong...";
                        Program.LaunchUnhandleExceptionForm(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LaunchUnhandleExceptionForm(ex.Message.ToString());
            }
        }
    }
}
