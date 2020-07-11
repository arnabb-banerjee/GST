using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;

namespace Databaselayer
{
    public class MailHelper
    {
        MailMessage msg = null;
        SmtpClient smtp = null;

        public MailHelper()
        {
            msg = new MailMessage();
        }

        public MailHelper(string MailOption, string Subject, string Body, string FromEmailID, string FromName, string ToList, string ToDisplayList, string CCList, string CCDisplayList)
        {
            msg = new MailMessage();
            msg.From = new MailAddress(FromEmailID, FromName);
            smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["EmailHostIP"].ToString(),
                                     Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EmailHostPort"]));

            ToList = ToList.Trim().Replace(",", ";");
            CCList = CCList.Trim().Replace(",", ";");
            ToDisplayList = ToDisplayList.Trim().Replace(",", ";");
            CCDisplayList = CCDisplayList.Trim().Replace(",", ";");

            string[] aToList = ToList.Split(';');
            string[] aCCList = ToList.Split(';');
            string[] aToDisplayList = ToDisplayList.Split(';');
            string[] aCCDisplayList = CCDisplayList.Split(';');
            string displayname = "";

            #region To List
            if (aToList != null && aToList.Length > 0)
            {
                for (int i = 0; i < aToList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(aToList[i].Trim()))
                    {
                        if (aToDisplayList != null && aToDisplayList.Length > i)
                        {
                            displayname = aToDisplayList[i];
                            if (!msg.To.Contains(new MailAddress(aToList[i].ToString(), displayname)) && !msg.CC.Contains(new MailAddress(aToList[i].ToString(), displayname))) { msg.To.Add(new MailAddress(aToList[i].ToString(), displayname)); }
                        }
                        else
                        {
                            if (!msg.To.Contains(new MailAddress(aToList[i].ToString())) && !msg.CC.Contains(new MailAddress(aToList[i].ToString()))) { msg.To.Add(new MailAddress(aToList[i].ToString())); }
                        }
                    }
                }
            }
            #endregion

            #region CC List
            if (aCCList != null && aCCList.Length > 0)
            {
                for (int i = 0; i < aCCList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(aCCList[i].Trim()))
                    {
                        if (aCCDisplayList != null && aCCDisplayList.Length > i)
                        {
                            displayname = aCCDisplayList[i];
                            if (!msg.To.Contains(new MailAddress(aCCList[i].ToString(), displayname)) && !msg.CC.Contains(new MailAddress(aCCList[i].ToString(), displayname))) { msg.CC.Add(new MailAddress(aCCList[i].ToString(), displayname)); }
                        }
                        else
                        {
                            if (!msg.To.Contains(new MailAddress(aCCList[i].ToString())) && !msg.CC.Contains(new MailAddress(aCCList[i].ToString()))) { msg.CC.Add(new MailAddress(aCCList[i].ToString())); }
                        }
                    }
                }
            } 
            #endregion

            try
            {
                msg.IsBodyHtml = true;
                msg.Body = Body;
                smtp.Send(msg);

                iGST_Svc.wscalls.Save_EmailLog(MailOption, Subject, FromEmailID, ToList, CCList, Body, "", "", "", 0);
            }
            catch (Exception ex)
            {
                iGST_Svc.wscalls.Save_EmailLog(MailOption, Subject, FromEmailID, ToList, CCList, Body, ex.Message, ex.InnerException.ToString(), ex.StackTrace, 0);
            }
        }

        public void AddTo(string EmailID, string Name)
        {
            if (!msg.To.Contains(new MailAddress(EmailID, Name)) && !msg.CC.Contains(new MailAddress(EmailID, Name))) { msg.To.Add(new MailAddress(EmailID, Name)); }
        }

        public void AddCC(string EmailID, string Name)
        {
            if (!msg.To.Contains(new MailAddress(EmailID, Name)) && !msg.CC.Contains(new MailAddress(EmailID, Name))) { msg.CC.Add(new MailAddress(EmailID, Name)); }
        }

        public void AddAttachment(string FileName)
        {
            msg.Attachments.Add(new Attachment(FileName));
        }

        public void Send(string html)
        {
            msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["BigBookSenderEmailID"].ToString(),
                                  System.Configuration.ConfigurationManager.AppSettings["BigBookSenderEmailDisplayName"].ToString());

            smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["EmailHostIP"].ToString(),
                                     Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EmailHostPort"]));

            msg.IsBodyHtml = true;
            msg.Body = html;
            smtp.Send(msg);
        }

        public bool Send(string html, string FromEmailID, string FromName)
        {
            try
            {
                msg.From = new MailAddress(FromEmailID, FromName);
                smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["EmailHostIP"].ToString(),
                                         Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EmailHostPort"]));

                msg.IsBodyHtml = true;
                msg.Body = html;
                smtp.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetBody(string option)
        {
            //switch:
            //case:

            return "";
        }
    }
}