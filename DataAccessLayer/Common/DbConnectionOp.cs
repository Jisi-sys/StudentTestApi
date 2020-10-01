using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Common
{
    public class DbConnectionOp
    {

        protected string ConnectionString { get; set; } //Set Property

        public DbConnectionOp() //Created Object for ConnectionString to connect with appsettings.Json
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            this.ConnectionString = configuration.GetSection("ConnectionStrings").GetSection("DBConnectionString").Value;
        }


        private SqlConnection GetConnection() //Checks the connection in this function 
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString); //creating an object for sqlconnection
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }



        public SqlDataReader GetDataReader(string procedureName, List<SqlParameter> parameters) //Reader 
        {
            SqlDataReader dataReader;

            try
            {
                SqlConnection connection = GetConnection();
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataReader;
        }


        public int ExecuteNonQuery(string procedureName, List<SqlParameter> parameters) //ExecuteNonQuery
        {
            int rows;

            try
            {
                SqlConnection connection = GetConnection();
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    rows = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }
    }
}
    