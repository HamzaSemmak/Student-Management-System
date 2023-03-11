using Student_Management.Modules.UserModel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Student_Management.Modules.UserModel.Controller
{
    public class FormersController : Users
    {

        public FormersController() { }

        public int CreateUser(Users user)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand()
            {
                CommandText = "CreateUser",
                Connection = this.sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter Matricule = new SqlParameter()
            {
                ParameterName = "@matricule",
                SqlDbType = SqlDbType.VarChar,
                Value = user.Matricule,
                Direction = ParameterDirection.Input
            };
            SqlParameter Name = new SqlParameter()
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.VarChar,
                Value = user.Name,
                Direction = ParameterDirection.Input
            };
            SqlParameter Password = new SqlParameter()
            {
                ParameterName = "@password",
                SqlDbType = SqlDbType.VarChar,
                Value = user.Password,
                Direction = ParameterDirection.Input
            };
            SqlParameter Phone = new SqlParameter()
            {
                ParameterName = "@phone",
                SqlDbType = SqlDbType.VarChar,
                Value = user.Phone,
                Direction = ParameterDirection.Input
            };
            SqlParameter DateNaissance = new SqlParameter()
            {
                ParameterName = "@dateNaissance",
                SqlDbType = SqlDbType.VarChar,
                Value = user.DateNaissance,
                Direction = ParameterDirection.Input
            };
            SqlParameter Age = new SqlParameter()
            {
                ParameterName = "@age",
                SqlDbType = SqlDbType.Int,
                Value = user.Age,
                Direction = ParameterDirection.Input
            };
            SqlParameter City = new SqlParameter()
            {
                ParameterName = "@city",
                SqlDbType = SqlDbType.VarChar,
                Value = user.City,
                Direction = ParameterDirection.Input
            };
            SqlParameter FormerType = new SqlParameter()
            {
                ParameterName = "@formerType",
                SqlDbType = SqlDbType.VarChar,
                Value = user.FormerType,
                Direction = ParameterDirection.Input
            };
            SqlParameter UserType = new SqlParameter()
            {
                ParameterName = "@userType",
                SqlDbType = SqlDbType.VarChar,
                Value = user.UserType,
                Direction = ParameterDirection.Input
            };
            SqlParameter Output = new SqlParameter()
            {
                ParameterName = "@msg",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                Matricule, Name, Password, Phone, DateNaissance, Age, City, FormerType, UserType, Output
            };
            foreach (SqlParameter Params in sqlParameters)
            {
                this.sqlCommand.Parameters.Add(Params);
            }
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

            return Convert.ToInt32(Output.Value);
        }

        public int CountOfFormersTypes()
        {
            int Count = 0;
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select Count(*) from FormersType";
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

        public List<string> getAllFormersType()
        {
            List<string> FormerType = new List<string>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = "Select * from FormersType";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if (this.sqlDataReader.HasRows)
            {
                while (this.sqlDataReader.Read())
                {
                    FormerType.Add(Convert.ToString(this.sqlDataReader["type"]));
                }
            }
            this.CloseConnection();
            return FormerType;
        }

        public List<Users> getFormerByMatricule(string Matricule)
        {
            List<Users> Users = new List<Users>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from Users where Matricule = '{Matricule}'";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if (this.sqlDataReader.HasRows)
            {
                while (this.sqlDataReader.Read())
                {
                    Users.Add(new Users(
                        Convert.ToString(this.sqlDataReader["Matricule"]),
                        Convert.ToString(this.sqlDataReader["Name"]),
                        Convert.ToString(this.sqlDataReader["Password"]),
                        Convert.ToString(this.sqlDataReader["Phone"]),
                        Convert.ToString(this.sqlDataReader["DateNaissance"]),
                        Convert.ToInt32(this.sqlDataReader["Age"]),
                        Convert.ToString(this.sqlDataReader["city"]),
                        Convert.ToString(this.sqlDataReader["FormerType"]),
                        Convert.ToString(this.sqlDataReader["UserType"])
                    ));
                }
            }
            this.CloseConnection();
            return Users;
        }

        public StringBuilder FormerCreated(Users user)
        {
            StringBuilder Former = new StringBuilder();
            Former.AppendLine($"Matricule : {user.Matricule} ");
            Former.AppendLine($"Name : {user.Name} ");
            Former.AppendLine($"Password : {user.Password} ");
            Former.AppendLine($"DateNaissance : {user.DateNaissance} ");
            Former.AppendLine($"Age : {user.Age} ");
            Former.AppendLine($"City : {user.City} ");
            Former.AppendLine($"Former Type : {user.FormerType} ");
            Former.AppendLine($"User Type : {user.UserType} ");
            return Former;
        }

        public int DeleteFormer(string FormerMatricule)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand()
            {
                CommandText = "DeleteFormer",
                Connection = this.sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter Matricule = new SqlParameter()
            {
                ParameterName = "@matricule",
                SqlDbType = SqlDbType.VarChar,
                Value = FormerMatricule,
                Direction = ParameterDirection.Input
            };
            SqlParameter Output = new SqlParameter()
            {
                ParameterName = "@msg",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            this.sqlCommand.Parameters.Add(Matricule);
            this.sqlCommand.Parameters.Add(Output);
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

            return Convert.ToInt32(Output.Value);
        }

        public int UpdateFormer(Users user, string FormerMatricule)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Update Users Set" +
                $" Name = '{user.Name}', Password = '{user.Password}', Phone = '{user.Phone}'," +
                $" City = '{user.City}', FormerType = '{user.FormerType}', UserType = '{user.UserType}' " +
                $" where Matricule = '{FormerMatricule}'";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

            return 40319;
        }
    }
}
