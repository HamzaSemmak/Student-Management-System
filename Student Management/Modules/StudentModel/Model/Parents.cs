using Student_Management.Modules.DB_Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.Modules.StudentModel.Model
{
    public class Parents : Students
    {
        protected string fatherName;
        protected string fatherCIN;
        protected string fatherDateNaissance;
        protected string motherName;
        protected string motherCIN;
        protected string motherDateNaissance;

        public string FatherName { get => fatherName; set => fatherName = value; }
        public string FatherCIN { get => fatherCIN; set => fatherCIN = value; }
        public string FatherDateNaissance { get => fatherDateNaissance; set => fatherDateNaissance = value; }
        public string MotherName { get => motherName; set => motherName = value; }
        public string MotherCIN { get => motherCIN; set => motherCIN = value; }
        public string MotherDateNaissance { get => motherDateNaissance; set => motherDateNaissance = value; }

        public Parents() 
        { 

        }

        public Parents(string fatherName, string fatherCIN, string fatherDateNaissance, string motherName, string motherCIN, string motherDateNaissance)
        {
            this.FatherName = fatherName;
            this.fatherCIN = fatherCIN;
            this.fatherDateNaissance = fatherDateNaissance;
            this.motherName = motherName;
            this.motherCIN = motherCIN;
            this.motherDateNaissance = motherDateNaissance;
        }
    }
}
