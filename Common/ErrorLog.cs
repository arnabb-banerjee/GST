using System;
using System.IO;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class ErrorLog
    {
        public static void LogErrors_Comments(string StringTobeLogged, string PageName, string Method, string TypeOfLog)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("-------------------START (" + (TypeOfLog == "C" ? "Comment" : "Error") + ")-------------" + DateTime.Now);
                sb.AppendLine("Page :" + PageName);
                sb.AppendLine(StringTobeLogged);
                sb.AppendLine("-------------------END-------------" + DateTime.Now);
                sb.AppendLine("");
                sb.AppendLine("");

                WriteFile(sb.ToString());
            }
            catch
            {

            }
        }

        public static void LogSQLErrors_Comments(System.Data.SqlClient.SqlCommand cmd = null, string ErrorMessage = "", Exception ex = null, System.Data.SqlClient.SqlException sqlex = null)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("-------------------START-------------" + DateTime.Now + Environment.NewLine);

                if (cmd != null)
                {
                    if (cmd.CommandType == System.Data.CommandType.StoredProcedure)
                    {
                        sb.AppendLine(cmd.CommandText);

                        if (cmd.Parameters != null)
                        {
                            foreach (System.Data.SqlClient.SqlParameter param in cmd.Parameters)
                            {
                                sb.AppendLine("" + param.ParameterName + ": " + param.Value + ", ");
                            }
                        }


                        sb.AppendLine(Environment.NewLine);
                    }
                }
                else
                {

                }

                if (ErrorMessage != "")
                {
                    sb.AppendLine("Error Message: " + ErrorMessage + Environment.NewLine);
                }

                if (sqlex != null)
                {
                    sb.AppendLine("SQL Exception: " + sqlex.Message + Environment.NewLine);
                    sb.AppendLine("StackTrace: " + sqlex.StackTrace + Environment.NewLine);
                    sb.AppendLine("InnerException: " + sqlex.InnerException + Environment.NewLine);

                }
                if (ex != null)
                {
                    sb.AppendLine("General Exception: " + ex.Message + Environment.NewLine);
                    sb.AppendLine("StackTrace: " + ex.StackTrace + Environment.NewLine);
                    sb.AppendLine("InnerException: " + ex.InnerException + Environment.NewLine);

                }

                sb.AppendLine("-------------------END-------------" + DateTime.Now);

                WriteFile(sb.ToString());
            }
            catch { }
        }

        public static void LogSQLErrors_CommentsOleDB(System.Data.OleDb.OleDbCommand cmd = null, string ErrorMessage = "", Exception ex = null, System.Data.OleDb.OleDbException sqlex = null)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("-------------------START-------------" + DateTime.Now + Environment.NewLine);

                if (cmd != null)
                {
                    if (cmd.CommandType == System.Data.CommandType.StoredProcedure)
                    {
                        sb.AppendLine(cmd.CommandText);

                        if (cmd.Parameters != null)
                        {
                            foreach (System.Data.OleDb.OleDbParameter param in cmd.Parameters)
                            {
                                sb.AppendLine("" + param.ParameterName + ": " + param.Value + ", ");
                            }
                        }


                        sb.AppendLine(Environment.NewLine);
                    }
                }
                else
                {

                }

                if (ErrorMessage != "")
                {
                    sb.AppendLine("Error Message: " + ErrorMessage + Environment.NewLine);
                }

                if (sqlex != null)
                {
                    sb.AppendLine("SQL Exception: " + sqlex.Message + Environment.NewLine);
                    sb.AppendLine("StackTrace: " + sqlex.StackTrace + Environment.NewLine);
                    sb.AppendLine("InnerException: " + sqlex.InnerException + Environment.NewLine);

                }
                if (ex != null)
                {
                    sb.AppendLine("General Exception: " + ex.Message + Environment.NewLine);
                    sb.AppendLine("StackTrace: " + ex.StackTrace + Environment.NewLine);
                    sb.AppendLine("InnerException: " + ex.InnerException + Environment.NewLine);

                }

                sb.AppendLine("-------------------END-------------" + DateTime.Now);

                WriteFile(sb.ToString());
            }
            catch { }
        }

        public static void WriteFile(string text)
        {
            //string path = "~/logfiles/" + "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            //using (FileStream fs = !File.Exists(path) ? File.Create(path) : File.Open(path, FileMode.Append))
            //using (StreamWriter writer = new StreamWriter(fs))
            //{
            //    writer.WriteLine(text);
            //}
        }
    }
}
