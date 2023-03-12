using Student_Management.Modules.DB_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.Modules.StudentModel.Model
{
    public class Students : DB
    {
        protected string matricule;
        protected string name;
        protected string dateNaissance;
        protected int age;
        protected string city;
        protected string adress;
        protected string gender;


        public string Matricule { get => matricule; set => matricule = value; }
        public string Name { get => name; set => name = value; }
        public string DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public int Age { get => age; set => age = value; }
        public string City { get => city; set => city = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Gender { get => gender; set => gender = value; }

        public Students() 
        {
        
        }

        public Students(string matricule, string name, string dateNaissance, int age, string city, string adress, string gender)
        {
            this.matricule = matricule;
            this.name = name;
            this.dateNaissance = dateNaissance;
            this.age = age;
            this.city = city;
            this.adress = adress;
            this.gender = gender;
        }
    }
}
