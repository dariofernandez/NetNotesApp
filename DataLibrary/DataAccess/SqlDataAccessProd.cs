using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataLibrary.Models;



namespace DataLibrary.DataAccess
{
    public class SqlDataAccessProd
    {
        public static string GetConnectionString(string connectionName = "SunshineNew")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            //return "Data Source=TOBYLAPTOP\\SQLEXPRESS;Initial Catalog=DarioDB;Integrated Security=False;User ID=dario;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //return "Server=tcp:dario-server.database.windows.net,1433;Initial Catalog=DarioDB;Persist Security Info=False;User ID=dario;Password=Betfer123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        }

        //  T is the model to load into
        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static ViewNotesModel LoadRecord(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                ViewNotesModel record = cnn.QuerySingle<ViewNotesModel>(sql);
                return record;
            }
        }





        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);   //will return # of recs
            }
        }

        public static int DeleteData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql);   //will return # of recs
            }
        }


        // Dapper ->  https://github.com/StackExchange/Dapper
        public static int CountData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                // recordCount
                return cnn.ExecuteScalar<int>(sql);   //will return # of recs
            }
        }

        // stored procedure
        // Dapper ->  https://github.com/StackExchange/Dapper
        public static void RunStoredProcedure(string sqlSP, DynamicParameters dynaP)
        {
            var p = new DynamicParameters();
            p.Add("@a", 11);
            p.Add("@b", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            IDbConnection cnn = new SqlConnection(GetConnectionString());
            //cnn.Execute("spMagicProc", p, commandType: CommandType.StoredProcedure);
            cnn.Execute(sqlSP, dynaP, commandType: CommandType.StoredProcedure);

            int b = p.Get<int>("@b");
            int c = p.Get<int>("@c");
        }


        public static List<T> LoadSP1<T>(string sqlSP, Guid RequestId)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //result is list of Custom Object
                return cnn.Query<T>(sqlSP, new { RequestId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static List<T> LoadSP2<T>(string sqlSP, DynamicParameters dynaP)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //result is list of Custom Object
                return cnn.Query<T>(sqlSP, dynaP, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static T LoadOneRecord<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                T record = cnn.QuerySingle<T>(sql);
                return record;
            }
        }


    }


    public class ResultObjectProd
    {
        public int? count { get; set; }
    }

}

