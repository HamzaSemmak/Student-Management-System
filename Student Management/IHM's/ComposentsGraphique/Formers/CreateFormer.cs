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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management.IHM_s.ComposentsGraphique.Formers
{
    public partial class CreateFormer : UserControl
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(CreateFormer));
        public UsersController UserController;
        public ResponseStatus ResponseStatus;
        public FormersController FormersController;
        public Control[] AllFields;

        public CreateFormer()
        {
            InitializeComponent();
            UserController = new UsersController();
            ResponseStatus = new ResponseStatus();
            FormersController = new FormersController();
            AllFields = new Control[]
            {
                NameField,
                PassworField,
                ConfirmPasswordField,
                PhoneField,
                CityField,
                FormerTpeField,
                UserRoleField,
                YearField,
                DayField,
                MonthField
            };
            GetAllFormerTpeField();
            GetUserRole();
            GetYearField();
            GetMonthField();
            GetDaysField();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            foreach (Control Field in AllFields)
            {
                Field.Text = null;
            }
        }

        private void GetAllFormerTpeField()
        {
            foreach(string item in FormersController.getAllFormersType())
            {
                FormerTpeField.Items.Add(item);
            }
        }

        private void GetUserRole()
        {
            UserRoleField.Items.Add("User");
            UserRoleField.Items.Add("Admin");
        }

        private void GetYearField()
        {
            DateTime CurrentYear = DateTime.Now;
            for(int i = CurrentYear.Year - 65; i <= CurrentYear.Year; i++)
            {
                YearField.Items.Add(i.ToString());
            }
        }

        private void GetMonthField()
        {
            int CountOfMonth = 12;
            for(int i = 1; i <= CountOfMonth; i++)
            {
                MonthField.Items.Add(i.ToString());
            }
        }

        private void GetDaysField()
        {
            int CountOfDays = 31;
            for(int i = 1;i <= CountOfDays; i++)
            {
                DayField.Items.Add(i.ToString());
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            ClearAllFields();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if(!(sender as Control).Enabled)
                return;

            CreateUser();
        }

        private void CreateUser()
        {
            DateTime CurrentDate = DateTime.Now;
            int status;
            try
            {
                string message;
                if (NameField.Text == "" || ConfirmPasswordField.Text == "" || PassworField.Text == ""
                        || PhoneField.Text == "" || CityField.Text == "" || YearField.Text == ""
                        || MonthField.Text == "" || DayField.Text == "" || FormerTpeField.Text == ""
                        || FormerTpeField.Text == ""
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
                        DateNaissance = Convert.ToString($"{YearField.Text}-{MonthField.Text}-{DayField.Text}"),
                        /* Age => */
                        Age = Convert.ToInt32(CurrentDate.Year - Convert.ToInt32(YearField.Text)),
                        /* city => */
                        City = Convert.ToString(CityField.Text),
                        /* FormerType => */
                        FormerType = Convert.ToString(FormerTpeField.Text),
                        /* UserType => */
                        UserType = Convert.ToString(UserRoleField.Text),

                    };
                    status = FormersController.CreateUser(users);
                    if (status == ResponseStatus.ResponseCodeMatriculeExists)
                    {
                        message = "The Matricule is Already Exists, Please Try Again";
                        Program.LaunchUnhandleExceptionForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodeNameExists)
                    {
                        message = "The Name is Already Exists, Please Try Again";
                        Program.LaunchUnhandleExceptionForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodePasswordExists)
                    {
                        message = "The Password is Already Exists, Please Try Again";
                        Program.LaunchUnhandleExceptionForm(message);
                    }
                    else if (status == ResponseStatus.ResponseCodeCreateUserSuccefly)
                    {
                        message = "User Createad successfly.";
                        ClearAllFields();
                        Program.LaunchHandleExceptionForm(message);

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error : {ex.Message}");
                Program.LaunchUnhandleExceptionForm(Convert.ToString(ex.Message));
            }
        }
    }
}
