using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public class Validations
    {
        public enum ValueType { Alphabet, AlphabetSpecialChar, AlphaNumeric, AlphaNumericSpecialChar, Numeric, Percentage, Money, Integer, DateTime, Email, EmailFromSpecialDomain, MobileNumber, Fax_PhoneNumber, Prefix, PIN, Password, PairValueAEMR, PairValueYN, PairValue01 }

        public static bool ValidateDataType(string ValueTobeChecked, ValueType valuetype, bool isBlankAllowed, string FieldName, out string ErrorString)
        {
            ErrorString = "";

            if (!isBlankAllowed && (ValueTobeChecked == null ? "" : ValueTobeChecked).Trim().Length == 0)
            {
                ErrorString = "Value should not be blank for " + FieldName;
                return false;
            }

            if (ValueTobeChecked != null && ValueTobeChecked.Trim().Length > 0)
            {
                string scripttag = ValueTobeChecked.Replace(" ", "").Trim().ToUpper();
                if (scripttag.ToUpper().Contains("<SCRIPT") || scripttag.Contains("<META") || scripttag.Contains("<IFRAME"))
                {
                    ErrorString = "<script>, <meta>, <iframe> are not allowd text for " + FieldName;
                    return false;
                }

                #region Alphabet
                if (valuetype == ValueType.Alphabet)
                {
                    Regex regx = new Regex(@"^[a-zA-Z\s]+$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only alphabet value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region AlphaNumeric
                else if (valuetype == ValueType.AlphaNumeric)
                {
                    Regex regx = new Regex(@"^[a-zA-Z0-9\s,]*$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only alphabet & numeric value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region AlphaNumericSpecialChar
                else if (valuetype == ValueType.AlphaNumericSpecialChar)
                {
                    Regex regx = new Regex(@"^[a-zA-Z0-9\s\^${}[]().+?|-&%#]+$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only alphabet numeric & special value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Numeric
                else if (valuetype == ValueType.Numeric)
                {
                    Regex regx = new Regex(@"^\d{0,18}.\d{0,18}$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only numeric value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Percentage
                else if (valuetype == ValueType.Percentage)
                {
                    Regex regx = new Regex(@"^\d{0,18}.\d{0,2}$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only numeric value with 2 decimal is applicable for " + FieldName;
                        return false;
                    }
                    else if (Convert.ToDouble(ValueTobeChecked) > 100)
                    {
                        ErrorString = "Value should be less than 100 " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Money
                else if (valuetype == ValueType.Money)
                {
                    Regex regx = new Regex(@"^\d{0,18}.\d{0,2}$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only numeric value with 2 decimal is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Integer
                else if (valuetype == ValueType.Integer)
                {
                    Regex regx = new Regex(@"^[0-9]+$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only alphabet value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Alphabet
                else if (valuetype == ValueType.DateTime)
                {
                    DateTime datetime;
                    if (!DateTime.TryParseExact(ValueTobeChecked, new[] { "yyyy-MM-dd", "MM/dd/yy", "dd-MMM-yy", "dd/MM/yyyy", "MM/dd/yyyy", "dd-MM-yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out datetime))
                    {
                        if (!DateTime.TryParse(ValueTobeChecked, out datetime))
                        {
                            ErrorString = "Enter valid date " + FieldName;
                            return false;
                        }
                    }
                }
                #endregion

                #region Email
                else if (valuetype == ValueType.Email)
                {
                    Regex regx = new Regex(@"^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only valid email address is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Mobile Number
                else if (valuetype == ValueType.MobileNumber)
                {
                    Regex regx = new Regex(@"^([0-9]{10,20})+$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Mobile numer should be 10 - 20 digit " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region Alphabet
                else if (valuetype == ValueType.Fax_PhoneNumber)
                {
                    Regex regx = new Regex(@"^([0-9]{7,13})+$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only alphabet value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region PIN
                else if (valuetype == ValueType.PIN)
                {
                    Regex regx = new Regex(@"^([0-9]{6,6})+$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Only alphabet value is applicable for " + FieldName;
                        return false;
                    }
                }
                #endregion

                else if (valuetype == ValueType.PIN)
                {
                    Regex regx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

                    if (!regx.IsMatch(ValueTobeChecked))
                    {
                        ErrorString = "Password should contain one of each for small, capital, spaecial, numeric chrecter and length should be between 8 - 15" + FieldName;
                        return false;
                    }
                }

                #region PairValueAEMR
                else if (valuetype == ValueType.PairValueAEMR)
                {
                    if (ValueTobeChecked.Trim().ToUpper() != "M" && ValueTobeChecked.Trim().ToUpper() != "R"
                         && ValueTobeChecked.Trim().ToUpper() != "A" && ValueTobeChecked.Trim().ToUpper() != "E")
                    {
                        ErrorString = "Invalid type of the user " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region PairValueYN
                else if (valuetype == ValueType.PairValueYN)
                {
                    if (ValueTobeChecked.Trim().ToUpper() != "Y" && ValueTobeChecked.Trim().ToUpper() != "N" &&
                        ValueTobeChecked.Trim().ToUpper() != "YES" && ValueTobeChecked.Trim().ToUpper() != "NO" && ValueTobeChecked.Trim().ToUpper() != "")
                    {
                        ErrorString = "Only Yes / No is allowed for " + FieldName;
                        return false;
                    }
                }
                #endregion

                #region PairValue01
                else if (valuetype == ValueType.PairValue01)
                {
                    if (ValueTobeChecked.Trim().ToUpper() != "0" && ValueTobeChecked.Trim().ToUpper() != "1")
                    {
                        ErrorString = "Only Yes / No is allowed for " + FieldName;
                        return false;
                    }
                }
                #endregion
            }

            return true;
        }

        public static string ConvertToDateReturn_ddMMyyyy_blank(string ValueTobeChecked)
        {
            string ErrorString = "";

            if (ValueTobeChecked != null && ValueTobeChecked.Trim().Length > 0 && ValidateDataType(ValueTobeChecked.Trim(), ValueType.DateTime, false, "", out ErrorString))
            {
                return Convert.ToDateTime(ValueTobeChecked.Trim()).ToString("dd/MM/yyyy").Replace("-", "/");
            }

            return "";
        }

        public static bool CheckIntegerArray(string ValueTobeChecked, ValueType valuetype, bool isBlankAllowed, string FieldName, out string ErrorString)
        {
            ErrorString = "";

            if (!isBlankAllowed && (ValueTobeChecked == null ? "" : ValueTobeChecked).Trim().Length == 0)
            {
                ErrorString = "Value should not be blank for " + FieldName;
                return false;
            }

            if (ValueTobeChecked != null && ValueTobeChecked.Trim().Length > 0)
            {
                string scripttag = ValueTobeChecked.Replace(" ", "").Trim().ToUpper();
                if (scripttag.ToUpper().Contains("<SCRIPT") || scripttag.Contains("<META") || scripttag.Contains("<IFRAME"))
                {
                    ErrorString = "<script>, <meta>, <iframe> are not allowd text for " + FieldName;
                    return false;
                }
                else
                {
                    string[] strarray = ValueTobeChecked.Split(',');

                    if (valuetype == ValueType.Numeric)
                    {
                        Regex regx = new Regex(@"^\d{0,18}.\d{0,18}$");

                        foreach (string str in strarray)
                        {
                            if (!regx.IsMatch(str))
                            {
                                ErrorString = "Only numeric value is applicable for " + FieldName;
                                return false;
                            }
                        }
                    }
                    else if (valuetype == ValueType.Integer)
                    {
                        Regex regx = new Regex(@"^[0-9]+$");

                        foreach (string str in strarray)
                        {
                            if (!regx.IsMatch(str))
                            {
                                ErrorString = "Only integer value is applicable for " + FieldName;
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}