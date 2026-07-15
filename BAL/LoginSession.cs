using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class LoginSession
    {
        public static int TempId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["tempid"] != null)
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["tempid"]);
                else
                    return 0;
            }
            set
            {
                System.Web.HttpContext.Current.Session["tempid"] = value;
            }
        }
        public static string action
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_action"] != null)
                    return Convert.ToString(System.Web.HttpContext.Current.Session["_action"]);
                else
                    return "";
            }
            set
            {
                System.Web.HttpContext.Current.Session["_action"] = value;
            }
        }

        public static string GlobalErr
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_GlobalErr"] != null)
                    return Convert.ToString(System.Web.HttpContext.Current.Session["_GlobalErr"]);
                else
                    return "";
            }
            set
            {
                System.Web.HttpContext.Current.Session["_GlobalErr"] = value;
            }
        }

        public static bool issuperadmin
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_issuperadmin"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["_issuperadmin"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["_issuperadmin"] = value;
            }
        }

        public static bool isadmin
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_isadmin"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["_isadmin"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["_isadmin"] = value;
            }
        }

        public static userInfo userInfo
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["userinfo"] != null)
                    return System.Web.HttpContext.Current.Session["userinfo"] as userInfo;
                else
                    return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["userinfo"] = value;
            }
        }
    }

    public class userInfo
    {
        public int? UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserLastName { get; set; }
        public string UserRole { get; set; }

    }
}
