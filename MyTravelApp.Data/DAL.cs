using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.Configuration;
using Dapper;

namespace MyTravelApp.Data
{
    public class DAL
    {
        private readonly string _connString;
        public DAL(string connString)
        {
            _connString = connString;
        }


        public IEnumerable<T> ExecuteSelectCommand<T>(string procname, object parameters = null)
        {
            using IDbConnection connection = new SqlConnection(_connString);
            return connection.Query<T>(procname, parameters);
        }


        public T ExecuteSelectOneRowCommand<T>(string procname, object parameters)
        {
            using(var db = new SqlConnection(_connString))
            {
                return db.QueryFirstOrDefault<T>(procname, parameters, commandType: CommandType.StoredProcedure);
            }
        }





        //public DataSet ExecuteSelect(string procname, SqlParameter[] param)
        //{
        //    using(SqlConnection conn = new SqlConnection(_connString))
        //    {
        //        using(SqlCommand cmd = new SqlCommand(procname, conn))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            if(param != null)
        //            {
        //                cmd.Parameters.AddRange(param);
        //            }

        //            using(SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                DataSet dt = new DataSet();
        //                sda.Fill(dt);
        //                return dt;
        //            }
        //        }
        //    }
        //}
    }
}
