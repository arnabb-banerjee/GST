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
        #region Static Value Related
        public static List<StaticValuInfo> GetList_StaticValue(string Key)
        {
            List<StaticValuInfo> list = new List<StaticValuInfo>();
            using (DBHelper dbhlper = new DBHelper("GetStaticValueList"))
            {
                DBHelper.AddPparameter("@Key", Key);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        StaticValuInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new StaticValuInfo();
                            obj.Id = dr["ID"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            obj.Key = dr["Key"].ToString();
                            obj.Value = dr["Value"].ToString();
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);

                            list.Add(obj);
                        }
                    }
                }
            }

            return list;
        }
        #endregion
    }
}