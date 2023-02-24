using System;
using Student_Management.Modules.UserModel.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Student_Management.Modules.UserModel.Controller
{
    public class UsersController : Users
    {
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

        public int Create(Users Object)
        {
            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand()
            {
                CommandText = "CreateUser",
                Connection = this.sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter Name = new SqlParameter()
            {
                ParameterName = "@Name",
                SqlDbType = SqlDbType.VarChar,
                Value = Object.Name,
                Direction = ParameterDirection.Input
            };
            SqlParameter Password = new SqlParameter()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                Value = Object.Password,
                Direction = ParameterDirection.Input
            };
            SqlParameter UserType = new SqlParameter()
            {
                ParameterName = "@UserType",
                SqlDbType = SqlDbType.VarChar,
                Value = Object.UserType,
                Direction = ParameterDirection.Input
            };
            SqlParameter Output = new SqlParameter()
            {
                ParameterName = "@msg",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            this.sqlCommand.Parameters.Add(Name);
            this.sqlCommand.Parameters.Add(Password);
            this.sqlCommand.Parameters.Add(UserType);
            this.sqlCommand.Parameters.Add(Output);
            this.OpenConnection();
            this.sqlCommand.ExecuteNonQuery();
            CloseConnection();

            return Convert.ToInt32(Output.Value);

        }

        public void AllUsers()
        {
            this.sqlConnection = new SqlConnection(ConnectionString);
            string Query = "Select * from Users";
            this.sqlCommand = new SqlCommand(Query, this.sqlConnection);
            this.OpenConnection();
            this.sqlDataReader = this.sqlCommand.ExecuteReader();
            if(this.sqlDataReader.Read())
            {
                //Here Hamza Semmak......
            }
        }

    }
}
