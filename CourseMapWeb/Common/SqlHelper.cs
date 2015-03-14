using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CourseMapWeb.Common
{
    public static  class  SqlHelper
    {
        private const string ConnectionKey = "CourseMapDataModelKey";
        public static void ExcecuteNonQuery(string proc, CommandType cmdType, IEnumerable<SqlParameter> parameter = null)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionKey].ToString());
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            var cmd = new SqlCommand(proc, con);
            if (cmdType == CommandType.Text)
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                if (cmdType == CommandType.StoredProcedure)
                {

                    if (parameter == null)
                    {
                    }
                    else
                    {
                        AddParameter(cmd, parameter);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                }
            }

            cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private static void AddParameter(SqlCommand cmd, IEnumerable<SqlParameter> param)
        {
            foreach (var par in param)
            {
                if (par.Direction == ParameterDirection.InputOutput && par.Value == null)
                {
                    par.Value = DBNull.Value;
                }
                cmd.Parameters.Add(par);
            }
        }

        public static DataTable DataTable(string procs, CommandType cmdType, IEnumerable<SqlParameter> param = null)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionKey].ToString());
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            var dt = new DataTable();
            var da = new SqlDataAdapter(procs, con);
            if (cmdType == CommandType.Text)
            {
                da.SelectCommand.CommandType = CommandType.Text;
            }
            else
            {

                if (param == null)
                {
                }
                else
                {
                    AttachParameter(da, param);
                }
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return dt;

        }

        private static void AttachParameter(SqlDataAdapter da, IEnumerable<SqlParameter> param)
        {
            foreach (var par in param)
            {
                if (par.Direction == ParameterDirection.InputOutput && par.Value == null)
                {
                    par.Value = DBNull.Value;
                }
                da.SelectCommand.Parameters.Add(par);
            }
        }

        public static DataSet DataSet(string procs, CommandType cmdType, IEnumerable<SqlParameter> param = null)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionKey].ToString());
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            var ds = new DataSet();
            var da = new SqlDataAdapter(procs, con);
            if (cmdType == CommandType.Text)
            {
                da.SelectCommand.CommandType = CommandType.Text;
            }
            else
            {

                if (param == null)
                {
                }
                else
                {
                    AttachParameter(da, param);
                }
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            return ds;

        }
    }
}
