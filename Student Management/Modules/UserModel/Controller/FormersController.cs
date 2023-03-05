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
        internal readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public FormersController() { }

        private string GenerateMatricule(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

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
                Value = GenerateMatricule(10).ToString(),
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
    }
}
