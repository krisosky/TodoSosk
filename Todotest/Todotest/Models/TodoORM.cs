using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Todotest.Models
{
    
    public class TodoORM
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TodoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       
        //opening an sql connection to the db to access stored procedures
        public static void ExecutedWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
                }
  
        }

        public static T ExecutedReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }
        //TodoORM.ReturnList<todo> <= IEnumerable<todo>
        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);

            }
        }
    }
}