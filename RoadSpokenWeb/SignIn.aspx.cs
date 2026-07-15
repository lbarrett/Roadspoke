using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text.ToLower() == "admin")
        {
            Response.Redirect("indexInner.html");
        }
        else
        {
            ltrMsg.Text = "<span style='color:red;'>Wrong Password!</span>";
        }
    }
}