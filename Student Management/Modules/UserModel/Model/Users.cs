using log4net;
using Student_Management.Modules.DB_Connection;
using System.Collections;

namespace Student_Management.Modules.UserModel.Model
{
    public class Users : DB
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(Users));
        protected string name;
        protected string password;
        protected string userType;
        protected string status;
        protected int checks;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public string Password 
        { 
            get => password; 
            set => password = value; 
        }
        public string Status 
        { 
            get => status; 
            set => status = value; 
        }
        public int Checks 
        { 
            get => checks; 
            set => checks = value; 
        }
        public string UserType 
        {
            get => userType; 
            set => userType = value; 
        }

        public Users()
        {
            //
        }

        public Users(string Name, string Password, string UserType) 
        {
            this.Name = Name;
            this.Password = Password;
            if (UserType == "")
                this.UserType = "User";
            else 
                this.UserType = UserType;
            this.Checks = 1;
            this.status = "InLocked"; // Or Inlocked
        }
    }
}
