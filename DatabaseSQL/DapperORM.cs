using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DatabaseSQL
{
    public class DapperORM
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=DapperDB;Integrated Security=true";

        public static void ExecuteWithoutReturn<T>(string procedureName, DynamicParameters param)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        //DapperORM.ExecuteReturnScalar<int>(..,...)
        public static List<T> ReturnList<T>(string procedureName, DynamicParameters param)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                return sqlConnection.Query<T>(procedureName, param: CommandType.StoredProcedure).ToList();
            }
        }
    }
}