using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Common;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Data.OleDb;
using BusinessObjects;
using Databaselayer;

namespace iGST_Svc
{
    public sealed partial class wscalls
    {
        #region Email Log
        public static bool Save_EmailLog(string MailOption, string Subject, string From, string To, string CC, string Body, string ErrorMessage, string InnerException, string StackStress, int UserId)
        {
            string errormsg = "";

            using (DBHelper dbhlper = new DBHelper("spSaveEmailLog", true))
            {
                #region MyRegion
                DBHelper.AddPparameter("@UserID", UserId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@MailOption", MailOption, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Subject", Subject, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Body", Body, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@From", From, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@TO", To, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CC", CC, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ErrorMessage", ErrorMessage, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@InnerException", InnerException, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@StackStress", StackStress, DBHelper.param_types.Varchar);
                #endregion

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion
    }
}