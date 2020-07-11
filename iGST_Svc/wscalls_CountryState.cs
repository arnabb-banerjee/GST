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
        #region Coutry & State List
        public static List<CountryInfo> GetList_Country()
        {
            using (DBHelper dbhlper = new DBHelper("GetCountryStateList"))
            {
                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CountryInfo> list = new List<CountryInfo>();
                        CountryInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CountryInfo();
                            obj.CountryID = dr["ID"].ToString();
                            obj.CountryName = dr["Name"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }

                return null;
            }
        }

        public static List<StateInfo> GetList_State(string CountryID)
        {
            if (!string.IsNullOrEmpty(CountryID))
            {
                using (DBHelper dbhlper = new DBHelper("GetCountryStateList"))
                {
                    DBHelper.AddPparameter("@CountryID", CountryID);

                    using (DataSet ds = DBHelper.Execute_Query())
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            List<StateInfo> list = new List<StateInfo>();
                            StateInfo obj = null;

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                obj = new StateInfo();
                                obj.StateID = dr["ID"].ToString();
                                obj.StateName = dr["Name"].ToString();

                                list.Add(obj);
                            }

                            return list;
                        }
                    }
                }
            }

            return null;
        }
        #endregion
    }
}