using Student_Management.Modules.StudentModel.Model;
using Student_Management.Modules.UserModel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.Modules.StudentModel.Controller
{
    public class StudentsController : Students
    {
        public StudentsController() 
        {
            
        }

        public int CountOfStudents()
        {
            int Count = 0;
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select Count(*) from Students";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if (this.sqlDataReader.HasRows)
            {
                if (this.sqlDataReader.Read())
                {
                    Count = Convert.ToInt32(this.sqlDataReader[0]);
                }
            }
            else
            {
                Count = 0;
            }
            this.CloseConnection();
            return Count;
        }

        public List<Students> GetStudentsWithLimit(int CountOfRows)
        {
            List<Students> Students = new List<Students>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select Top {CountOfRows} * from Students";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if (this.sqlDataReader.HasRows)
            {
                while (this.sqlDataReader.Read())
                {
                    Students.Add(
                        new Students(
                            Convert.ToString(this.sqlDataReader["Matricule"]),
                            Convert.ToString(this.sqlDataReader["Name"]),
                            Convert.ToString(this.sqlDataReader["DateNaissance"]),
                            Convert.ToInt32(this.sqlDataReader["Age"]),
                            Convert.ToString(this.sqlDataReader["City"]),
                            Convert.ToString(this.sqlDataReader["Adresse"]),
                            Convert.ToString(this.sqlDataReader["Gender"])
                        )
                    );
                }
            }
            this.CloseConnection();
            return Students;
        }

        public List<Students> AllStudents()
        {
            List<Students> Students = new List<Students>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from Students";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if (this.sqlDataReader.HasRows)
            {
                while (this.sqlDataReader.Read())
                {
                    Students.Add(
                        new Students(
                            Convert.ToString(this.sqlDataReader["Matricule"]),
                            Convert.ToString(this.sqlDataReader["Name"]),
                            Convert.ToString(this.sqlDataReader["DateNaissance"]),
                            Convert.ToInt32(this.sqlDataReader["Age"]),
                            Convert.ToString(this.sqlDataReader["City"]),
                            Convert.ToString(this.sqlDataReader["Adresse"]),
                            Convert.ToString(this.sqlDataReader["Gender"])
                        )
                    );
                }
            }
            this.CloseConnection();
            return Students;
        }
    }
}
