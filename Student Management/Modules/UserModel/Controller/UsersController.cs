using System;
using Student_Management.Modules.UserModel.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TheArtOfDevHtmlRenderer.Adapters.Entities;
using System.Collections;
using System.Linq;
using Guna.UI2.WinForms;
using System.Collections.Generic;

namespace Student_Management.Modules.UserModel.Controller
{
    public class UsersController : Users
    {
        public UsersController() { }

        public int Authentification(string newName, string newPassword)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand()
            {
                CommandText = "Authentification",
                Connection = this.sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter P1 = new SqlParameter()
            {
                ParameterName = "@UserName",
                SqlDbType = SqlDbType.VarChar,
                Value = newName,
                Direction = ParameterDirection.Input
            };
            SqlParameter P2 = new SqlParameter()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                Value = newPassword,
                Direction = ParameterDirection.Input
            };
            SqlParameter Output = new SqlParameter()
            {
                ParameterName = "@msg",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            this.sqlCommand.Parameters.Add(P1);
            this.sqlCommand.Parameters.Add(P2);
            this.sqlCommand.Parameters.Add(Output);
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            this.CloseConnection();
            return Convert.ToInt32(Output.Value);
        }

        public int GetUserID()
        {
            int ID = 0;
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from LockedUser;";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.Read())
            {
                ID = Convert.ToInt32(this.sqlDataReader["ID"]);
            }
            this.CloseConnection();

            return ID;
        }

        public string[] UserLocked(int ID)
        {
            string[] Result = new string[2];
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from Users where ID = {ID}";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.Read())
            {
                Result[0] = Convert.ToString(this.sqlDataReader["Name"]);
                Result[1] = Convert.ToString(this.sqlDataReader["FormerType"]);
            }
            this.CloseConnection();

            return Result;
        }

        public void forgetUser()
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Delete from LockedUser;";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            this.CloseConnection();
        }
        public List<Users> AllUsers()
        {
            List<Users> Users = new List<Users>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from Users";
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
                        Convert.ToString(this.sqlDataReader["Adresse"]),
                        Convert.ToString(this.sqlDataReader["FormerType"]),
                        Convert.ToString(this.sqlDataReader["UserType"])
                    ));
                }
            }
            this.CloseConnection();
            return Users;
        }


        public List<Users> GetUsersWithLimit(int CountOfRows)
        {
            List<Users> Users = new List<Users>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select Top {CountOfRows} * from Users";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.HasRows)
            {
                while(this.sqlDataReader.Read())
                {
                    Users.Add(new Users(
                        Convert.ToString(this.sqlDataReader["Matricule"]),
                        Convert.ToString(this.sqlDataReader["Name"]),
                        Convert.ToString(this.sqlDataReader["Password"]),
                        Convert.ToString(this.sqlDataReader["Phone"]),
                        Convert.ToString(this.sqlDataReader["DateNaissance"]),
                        Convert.ToInt32(this.sqlDataReader["Age"]),
                        Convert.ToString(this.sqlDataReader["Adresse"]),
                        Convert.ToString(this.sqlDataReader["FormerType"]),
                        Convert.ToString(this.sqlDataReader["UserType"])
                    ));
                }
            }
            this.CloseConnection();
            return Users;
        }

        public List<Users> GetUserByColumn(string Column, string Value)
        {
            List<Users> Users = new List<Users>();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from Users Where {Column} = '{Value}'";
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
                        Convert.ToString(this.sqlDataReader["Adresse"]),
                        Convert.ToString(this.sqlDataReader["FormerType"]),
                        Convert.ToString(this.sqlDataReader["UserType"])
                    ));
                }
            }
            this.CloseConnection();

            return Users;
        }

        public int GetNumbersOfUser()
        {
            int Count = 0;
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select Count(*) from Users";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.HasRows) 
            {
                if(this.sqlDataReader.Read())
                {
                    Count = Convert.ToInt32(this.sqlDataReader[0]);
                }
            }
            else
            {
                Count = 0;
            }
            return Count;
        }

        public int CheckUserIfAdmin(int ID)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand()
            {
                CommandText = "CheckUserIfAdmin",
                Connection = this.sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter Param1 = new SqlParameter()
            {
                ParameterName = "@ID",
                SqlDbType = SqlDbType.Int,
                Value = ID,
                Direction = ParameterDirection.Input
            };
            SqlParameter Output = new SqlParameter()
            {
                ParameterName = "@msg",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            this.sqlCommand.Parameters.Add(Param1);
            this.sqlCommand.Parameters.Add(Output);
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

            return Convert.ToInt32(Output.Value);
        }
    }
}
