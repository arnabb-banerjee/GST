using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Common
{
    public class GeneralBinds
    {
        public static string ListApplicationType(string SelectedItem)
        {
            System.Text.StringBuilder sbList = new System.Text.StringBuilder();

            sbList.AppendLine("<option value=\"\">Select one</option>");
            sbList.AppendLine("<option value=\"inter\" " + ("INTER" == SelectedItem.Trim().ToUpper()? "selected=\"selected\"":"") + " >Outside state</option>");
            sbList.AppendLine("<option value=\"inter\" " + ("INTRA" == SelectedItem.Trim().ToUpper()? "selected=\"selected\"":"") + " >Inside state</option>");
            sbList.AppendLine("<option value=\"\" ></option>");

            return sbList.ToString();
        }


    }
}
