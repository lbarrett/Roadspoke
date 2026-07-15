using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //if (LoginSession.userInfo == null)
        //    Response.Redirect("login.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (LoginSession.userInfo == null)
        //    Response.Redirect("login.aspx");

        //else
        //{

        //    if (LoginSession.isadmin)
        //    {
        //        ulmanageadmin.Visible = false;
        //    }
        //    else
        //    {
        //        ulmanageadmin.Visible = true;
        //    }

        //    Session.Timeout = 1000;
        //}

    }


    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();

        LoginSession.userInfo = null;
        Response.Redirect("login.aspx");
    }
}
