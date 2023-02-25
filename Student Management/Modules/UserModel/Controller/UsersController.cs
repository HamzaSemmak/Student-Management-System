using System;
using Student_Management.Modules.UserModel.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TheArtOfDevHtmlRenderer.Adapters.Entities;

namespace Student_Management.Modules.UserModel.Controller
{
    public class UsersController : Users
    {
        public UsersController() { }
        public string GenerateMatricule(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

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
            OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            CloseConnection();
            return Convert.ToInt32(Output.Value);
        }

        public int UserID()
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = "Select ID_User from LockedUser";
            int ID = 0;
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.Read())
            {
                ID = Convert.ToInt32(this.sqlDataReader["ID"]);
            }
            CloseConnection();
            return ID;
        }

        public string[] GetUserInformation()
        {
            string[] User = new string[2];
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Select * from Users where ID = {UserID()}";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if (this.sqlDataReader.Read())
            {
                User[0] = Convert.ToString(this.sqlDataReader["Name"]);
                User[1] = Convert.ToString(this.sqlDataReader["UserType"]);
            }
            CloseConnection();

            return User;
        }

        public void Disconnected()
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            string Query = $"Delete from LockedUser where ID_User = {UserID()}";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            Program.logger.Info("Test Passed Successfly");
            Program.logger.Info($"ID : {UserID()} ");
            CloseConnection();
        }
    }
}
