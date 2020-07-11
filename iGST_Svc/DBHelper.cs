using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Databaselayer
{
    public sealed class DBHelper : IDisposable
    {
        public DBHelper() { }
        static string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ToString();
        static SqlParameter param = null;
        static SqlCommand cmd = null;
        static SqlTransaction sqlTrans = null;
        public enum param_types { Int, BigInt, Varchar, Date, DateTime, Text, Image, Structured, Numeric, NVarchar }
        public enum param_direction { Output, Input }

        public static DataSet Execute_Query()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                da.SelectCommand = cmd;
                da.Fill(ds);

                //Common.ErrorLog.LogSQLErrors_Comments(cmd);
            }
            catch (SqlException sqlerror)
            {
                throw sqlerror;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }

            return ds;
        }

        public static bool Execute_NonQuery(out string errormsg)
        {
            errormsg = "";
            bool flag = false;

            try
            {
                Common.ErrorLog.LogSQLErrors_Comments(cmd);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    flag = true;
                    sqlTrans.Commit();
                }
                else
                {
                    errormsg = cmd.Parameters[cmd.Parameters.Count - 1].Value.ToString();
                    sqlTrans.Rollback();
                    Common.ErrorLog.LogSQLErrors_Comments(null, errormsg);
                }
            }
            catch (SqlException ex)
            {
                errormsg = ex.Message;
                Common.ErrorLog.LogSQLErrors_Comments(null, "", ex);
                return false;
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                Common.ErrorLog.LogSQLErrors_Comments(null, "", ex);
                return false;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }

            return flag;
        }

        public static void AddPparameter(string name, object value)
        {
            cmd.Parameters.Add(new SqlParameter(name, value));
        }

        public static void AddPparameter(string name, object value, param_types type)
        {
            param = new SqlParameter(name, value);

            switch (type)
            {
                case param_types.Int: param.SqlDbType = SqlDbType.Int; break;
                case param_types.BigInt: param.SqlDbType = SqlDbType.BigInt; break;
                case param_types.Varchar: param.SqlDbType = SqlDbType.VarChar; break;
                case param_types.NVarchar: param.SqlDbType = SqlDbType.NVarChar; break;
                case param_types.Date: param.SqlDbType = SqlDbType.Date; break;
                case param_types.DateTime: param.SqlDbType = SqlDbType.DateTime; break;
                case param_types.Image: param.SqlDbType = SqlDbType.Image; break;
                case param_types.Structured: param.SqlDbType = SqlDbType.Structured; break;
                case param_types.Text: param.SqlDbType = SqlDbType.Text; break;
                default: param.SqlDbType = SqlDbType.VarChar; break;
            }

            cmd.Parameters.Add(param);
        }

        public static void AddPparameter(string name, object value, param_types type, int size, param_direction direction)
        {
            param = new SqlParameter(name, value);

            switch (type)
            {
                case param_types.Int: param.SqlDbType = SqlDbType.Int; break;
                case param_types.BigInt: param.SqlDbType = SqlDbType.BigInt; break;
                case param_types.Varchar: param.SqlDbType = SqlDbType.VarChar; break;
                case param_types.Date: param.SqlDbType = SqlDbType.Date; break;
                case param_types.DateTime: param.SqlDbType = SqlDbType.DateTime; break;
                case param_types.Image: param.SqlDbType = SqlDbType.Image; break;
                case param_types.Structured: param.SqlDbType = SqlDbType.Structured; break;
                case param_types.Text: param.SqlDbType = SqlDbType.Text; break;
                case param_types.Numeric: param.SqlDbType = SqlDbType.Decimal; break;
                default: param.SqlDbType = SqlDbType.VarChar; break;
            }

            if (direction == param_direction.Output)
            {
                param.Direction = ParameterDirection.Output;
            }

            param.Size = size;

            cmd.Parameters.Add(param);
        }

        public static SqlParameterCollection GetCurrentParameters()
        {
            return cmd.Parameters;
        }

        public static object GetCurrentParameterValue(int index)
        {
            return cmd.Parameters[index];
        }

        public void Dispose()
        {
            sqlTrans = null;

            if (cmd != null)
            {
                if (cmd.Connection != null)
                {
                    cmd.Connection.Close();
                }

                cmd.Dispose();
            }
        }

        public DBHelper(string sqlprocedure, bool isRequiredTransaction = false)
        {
            try
            {
                //_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ToString();

                if (cmd != null)
                {
                    cmd = null;
                }

                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sqlprocedure;
                cmd.CommandTimeout = 0;
                cmd.Connection = new SqlConnection(_ConnectionString);

                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

                cmd.Connection.Open();

                if (isRequiredTransaction)
                {
                    sqlTrans = cmd.Connection.BeginTransaction();
                    cmd.Transaction = sqlTrans;
                }
            }
            catch (Exception ex)
            {
                Common.ErrorLog.LogSQLErrors_Comments(null, "Problem in DBHelperConstructor", ex);
            }
        }

        public DataSet ExecuteQuery(string sqlprocedure, List<Parameter> ParameterList)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlCommand cmdd = new SqlCommand();
                cmdd.Connection = new SqlConnection(_ConnectionString);
                cmdd.CommandType = CommandType.StoredProcedure;
                cmdd.CommandText = sqlprocedure;
                cmdd.CommandTimeout = 0;

                if (ParameterList != null && ParameterList.Count > 0)
                {
                    foreach (Parameter param in ParameterList)
                    {
                        cmdd.Parameters.Add(sqlParameter(param.name, param.value));
                    }
                }

                if (cmdd.Connection.State == ConnectionState.Open)
                {
                    cmdd.Connection.Close();
                }

                cmdd.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmdd;
                da.Fill(ds);
            }
            catch (SqlException sqlerror)
            {
                Common.ErrorLog.LogSQLErrors_Comments(null, "Problem in DBHelperConstructor", sqlerror);
                throw sqlerror;
            }
            catch (Exception error)
            {
                Common.ErrorLog.LogSQLErrors_Comments(null, "Problem in DBHelperConstructor", error);
                throw error;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                //cmd = null;
            }

            return ds;
        }

        public class Parameter
        {
            public SqlDbType datatype { get; set; }
            public object value { get; set; }
            public string name { get; set; }
            public ParameterDirection direction { get; set; }

            public Parameter() { }
            public Parameter(string _name, object _value)
            {
                this.name = _name;
                this.value = _value;
            }
            public Parameter(string _name, object _value, SqlDbType _datatype, ParameterDirection _direction)
            {
                this.datatype = _datatype;
                this.name = _name;
                this.value = _value;
                this.direction = _direction;
            }
        }

        public static SqlParameter sqlParameter(string _name, object _value)
        {
            return new SqlParameter(_name, _value);
        }

        public static SqlParameter sqlParameter(string _name, object _value, SqlDbType _datatype)
        {
            SqlParameter obj = new SqlParameter(_name, _value);
            obj.SqlDbType = _datatype;
            return obj;
        }

        public static SqlParameter sqlParameter(string _name, object _value, SqlDbType _datatype, ParameterDirection _direction)
        {
            SqlParameter obj = new SqlParameter(_name, _value);
            obj.SqlDbType = _datatype;
            obj.Direction = _direction;
            return obj;
        }

        #region Multiple Transaction
        public static bool Execute_NonQuery_DoNotCloseConnectionOnSuccess(out string errormsg)
        {
            errormsg = "";

            try
            {
                Common.ErrorLog.LogSQLErrors_Comments(cmd);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    errormsg = cmd.Parameters[cmd.Parameters.Count - 1].Value.ToString();
                    sqlTrans.Rollback();
                    Common.ErrorLog.LogSQLErrors_Comments(null, errormsg);
                    cmd.Connection.Close();
                    cmd.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                Common.ErrorLog.LogSQLErrors_Comments(null, "", ex);
                cmd.Connection.Close();
                cmd.Dispose();
                return false;
            }

            return true;
        }

        public static bool CloseConnection(bool CommitTransaction)
        {
            if (CommitTransaction) { sqlTrans.Commit(); }
            cmd.Connection.Close();
            cmd.Dispose();

            return true;
        }

        public static void ClearParameters()
        {
            cmd.Parameters.Clear();
        }

        public static void SetSQLCommandText(string CommandText)
        {
            cmd.CommandText = CommandText;
        }
        #endregion
    }
}