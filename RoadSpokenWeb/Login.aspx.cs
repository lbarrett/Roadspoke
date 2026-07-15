using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
       var logiInfo= objMasterUpdate.LoginUser(txtEmail.Text, txtPassword.Text);
       if (logiInfo != null)
       {
           Session.Timeout = 1000;
           userInfo uinfo = new userInfo();
           uinfo.UserID = logiInfo.Id;
           uinfo.Email = logiInfo.Email;
           LoginSession.userInfo = uinfo;
           Response.Redirect("UserManager.aspx");
       }
       else
       {
           ltrMsg.Text = "<span style='color:red'>Invalid User Name Or Password!</span>";
       }
    }
}