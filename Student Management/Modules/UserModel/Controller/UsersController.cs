using System;
using Student_Management.Modules.UserModel.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TheArtOfDevHtmlRenderer.Adapters.Entities;
using System.Collections;
using System.Linq;
using Guna.UI2.WinForms;

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

        public Users GetUsersWithLimit(Guna2DataGridView DataGridView, int CountOfRows)
        {
            Users users = new Users();
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select Top {CountOfRows} * from Users";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.HasRows)
            {
                while(this.sqlDataReader.Read())
                {
                    DataGridView.Rows.Add(this.sqlDataReader["Matricule"], this.sqlDataReader["Name"], this.sqlDataReader["FormerType"], this.sqlDataReader["DateNaissance"]);
                }
            }
            this.CloseConnection();
            return users;
        }
    }
}
