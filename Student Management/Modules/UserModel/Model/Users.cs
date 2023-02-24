using log4net;
using Student_Management.Modules.DB_Connection;
using System.Collections;

namespace Student_Management.Modules.UserModel.Model
{
    public class Users : DB
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(Users));
        protected string matricule;
        protected string name;
        protected string password;
        protected string phone;
        protected string dateNaissance;
        protected int age;
        protected string adress;
        protected string formerType;
        protected string userType;
        protected string status;
        protected int checks;

        public string Matricule { get => matricule; set => matricule = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        public string DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public int Age { get => age; set => age = value; }
        public string Adress { get => adress; set => adress = value; }
        public string FormerType { get => formerType; set => formerType = value; }
        public string UserType { get => userType; set => userType = value; }
        public string Status { get => status; set => status = value; }
        public int Checks { get => checks; set => checks = value; }

        public Users()
        { 
            //
        }
        public Users(string Matricule, string Name, string Password, string Phone, string DateNaissance, int Age, string Adress, string FormerType, string UserType)
        {
            this.matricule = Matricule;
            this.name = Name;
            this.password = Password;
            this.phone = Phone;
            this.DateNaissance = DateNaissance;
            this.age = Age;
            this.adress = Adress;
            this.formerType = FormerType;
            if (!string.IsNullOrEmpty(UserType))
                this.userType = "User";
            else
                this.userType = UserType;
            this.status = "InLocked";
            this.checks = 1;
        }

        public Users(string Name, string Password)
        {
            this.name = Name;
            this.password = Password;
        }
    }
}
