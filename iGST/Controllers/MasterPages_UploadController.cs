using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Common;
using BusinessObjects;
using System.Data;
using System.Web.Script.Serialization;

namespace iGST.Controllers
{
    public partial class UploadController : Controller
    {
        string ErrorMessage = "";

        [Route("openimageupload")]
        [ValidateUserSession]
        public ActionResult UploadImageOpen(string type, string ParentId)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                //List<ProductOrganiztionImageInfo> list = iGstSvc.GetList_ProductOrganizationImage("", OrganizationProductId, "", "", "");
                ViewBag.type = type;
                ViewBag.ParentId = ParentId;
                //if (list != null && list.Count > 0 && list[0].ImageId > 0)
                //{
                return PartialView("~/Views/MasterPages/UploadImage.cshtml");
                //}
                //else
                //{
                //    list = new List<ProductOrganiztionImageInfo>();
                //    ProductOrganiztionImageInfo obj = new ProductOrganiztionImageInfo();
                //    obj.FileData = new byte[] { };
                //    obj.FileName = "";
                //    obj.FileType = "";
                //    list.Add(obj);

                //    return PartialView("~/Views/MasterPages/UploadImage.cshtml", list);
                //}

            }

        }

        [Route("upload")]
        [ValidateUserSession]
        public ActionResult ExcelUpload(string type)
        {
            return PartialView("~/Views/MasterPages/ExcelUpload.cshtml", type);
        }

        [Route("uploadfile")]
        [ValidateUserSession]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile(string type, bool Overwrite)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                bool bReturn = false;
                var result = new { Success = "False", Message = "Error Message" };

                string OrganizationCode = "1";

                if (((UserInfo)Session["UserDetails"]).UserType == "R")
                {
                    OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode;
                }

                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        System.Data.DataSet ds = GetDataSet_Excel(Request.Files[0], out ErrorMessage);

                        if (ErrorMessage.Trim().Length > 0)
                        {
                            result = new { Success = "False", Message = ErrorMessage };
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }

                        bool flag = false;

                        if (type == "C")
                        {
                            new Bill_Svc.BillServiceClient().Upload_Customer("C", Overwrite, ds, OrganizationCode, ((UserInfo)Session["UserDetails"]).UserCode, out bReturn, out ErrorMessage);
                            if (ErrorMessage.Trim().Length == 0) { flag = true; }
                        }
                        else if (type == "S")
                        {
                            new Bill_Svc.BillServiceClient().Upload_Customer("S", Overwrite, ds, OrganizationCode, ((UserInfo)Session["UserDetails"]).UserCode, out bReturn, out ErrorMessage);
                            if (ErrorMessage.Trim().Length == 0) { flag = true; }
                        }
                        else if (type == "CA")
                            flag = new Master_Svc.MasterServiceClient().Upload_Category(Overwrite, ds, ((UserInfo)Session["UserDetails"]).UserCode, out bReturn, out ErrorMessage);
                        else if (type == "P")
                            flag = new Product_Svc.ProductServiceClient().Upload_Product(Overwrite, ds, ((UserInfo)Session["UserDetails"]), out bReturn, out ErrorMessage);
                        else if (type == "BT")
                            flag = new Master_Svc.MasterServiceClient().Upload_BankTransaction(Overwrite, false, ds, "", OrganizationCode, ((UserInfo)Session["UserDetails"]), out bReturn, out ErrorMessage);

                        if (ErrorMessage.Trim().Length > 0)
                        {
                            result = new { Success = "False", Message = ErrorMessage };
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else if (!flag)
                        {
                            result = new { Success = "False", Message = "No upload activity has been processed." };
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = new { Success = "False", Message = "Error in Uploading file. Please cocntact with the authority" + ex.Message };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                result = new { Success = "True", Message = "Data uploaded" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateUserSession]
        public System.Data.DataSet GetDataSet_Excel(HttpPostedFileBase file, out string ErrorMessage)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                System.Data.DataSet ds = null;
                ErrorMessage = "";

                try
                {
                    System.Data.OleDb.OleDbConnection conn = null;
                    System.Data.OleDb.OleDbCommand cmd = null;
                    System.Data.OleDb.OleDbDataAdapter da = null;
                    string TableName = "", ConnectionString = "";

                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadDataPath"].ToString()), fileName);

                    switch (System.IO.Path.GetExtension(path).Trim().ToUpper())
                    {
                        case ".XLS":
                            ConnectionString = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConnStr"].ConnectionString, path);
                            break;
                        case ".XLSX":
                            ConnectionString = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel07ConnStr"].ConnectionString, path);
                            break;
                        default:
                            ErrorMessage = "Please select a valid excel file";
                            break;
                    }

                    if (ErrorMessage.Trim().Length == 0)
                    {
                        try
                        {
                            try
                            {
                                file.SaveAs(path);
                            }
                            catch (Exception ex)
                            {
                                Common.ErrorLog.LogErrors_Comments(ex.Message, "Master Page Controller", "Excel upload - Saveas", "Error");
                                ErrorMessage = "Problem in saving file:" + ex.Message;
                                return null;
                            }

                            try
                            {
                                conn = new System.Data.OleDb.OleDbConnection(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel15ConnStr"].ConnectionString, path));
                                conn.Open();
                            }
                            catch
                            {
                                try
                                {
                                    conn = new System.Data.OleDb.OleDbConnection(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel14ConnStr"].ConnectionString, path));
                                    conn.Open();
                                }
                                catch
                                {
                                    try
                                    {
                                        conn = new System.Data.OleDb.OleDbConnection(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel13ConnStr"].ConnectionString, path));
                                        conn.Open();
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            conn = new System.Data.OleDb.OleDbConnection(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel07ConnStr"].ConnectionString, path));
                                            conn.Open();
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                conn = new System.Data.OleDb.OleDbConnection(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConnStr"].ConnectionString, path));
                                                conn.Open();
                                            }
                                            catch (Exception ex)
                                            {
                                                Common.ErrorLog.LogErrors_Comments(ex.Message, "Master Page Controller", "Excel upload - Connection", "Error");
                                                ErrorMessage = "Problem in saving file:" + ex.Message;
                                                return null;
                                            }
                                        }
                                    }
                                }

                            }

                            try
                            {
                                TableName = conn.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                                cmd = new System.Data.OleDb.OleDbCommand(@"SELECT * FROM [" + TableName + "]", conn);

                                da = new System.Data.OleDb.OleDbDataAdapter(cmd);
                                ds = new System.Data.DataSet();
                                da.Fill(ds);
                                da.Dispose();
                                cmd.Dispose();
                                conn.Dispose();
                                return ds;
                            }
                            catch (Exception ex)
                            {
                                Common.ErrorLog.LogErrors_Comments(ex.Message, "Master Page Controller", "Excel upload - GetDataSet", "Error");
                                ErrorMessage = "Inner Problem in reading file";
                                return null;
                            }
                        }
                        catch (Exception ex)
                        {
                            Common.ErrorLog.LogErrors_Comments(ex.Message, "Master Page Controller", "Excel upload - GetDataSet", "Error");
                            ErrorMessage = "Problem in reading file";
                            return null;
                        }
                    }

                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Problem in reading file" + ex.Message;
                    return null;
                }

                return null;
            }
        }
    }
}