using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class UserManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillUser();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrError.Text = "";
        dvError.Visible = false;

        MasterUpdate objMasterUpdate = new MasterUpdate();

        var RegisterEmail = objMasterUpdate.IsAlreadyRegister(txtEmail.Text);
        if (!RegisterEmail)
        {

            if (ddlRole.SelectedValue == "3")
            {
                objMasterUpdate.SaveUserMaster(Convert.ToInt32(hdnuserid.Value), Convert.ToInt32(ddlRole.SelectedValue), txtEmail.Text, txtPassword.Text,"","");

            }
            else
            {
                objMasterUpdate.SaveUserMaster(Convert.ToInt32(hdnuserid.Value), Convert.ToInt32(ddlRole.SelectedValue), txtEmail.Text, txtPassword.Text,"","");
            }

            txtEmail.Text = "";
            txtPassword.Text = "";
            ddlRole.SelectedIndex = 0;
            FillUser();
            dvAddEdit.Visible = false;
            dvUserList.Visible = true;
        }
    }


    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        dvAddEdit.Visible = true;
        dvUserList.Visible = false;
    }

    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                var userObj = objMasterUpdate.GetUserMasterById(Convert.ToInt32(e.CommandArgument));
                txtEmail.Text = userObj.Email;
                txtPassword.Text = userObj.UserPassword;


                if (userObj.RoleId != null)
                {
                    if (ddlRole.Items.FindByValue(Convert.ToString(userObj.RoleId)) != null)
                    {
                        ddlRole.SelectedValue = userObj.RoleId.ToString();
                    }
                }

                hdnuserid.Value = userObj.Id.ToString();
                dvUserList.Visible = false;
                dvAddEdit.Visible = true;
            }

            if (e.CommandName == "DeleteItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                objMasterUpdate.DeleteUserById(Convert.ToInt32(e.CommandArgument));
                FillUser();
            }
        }
        catch { }
    }


    protected void gvUser_OnPageindexchanging(object sender, GridViewPageEventArgs e)
    {
        gvUser.PageIndex = e.NewPageIndex;
        FillUser();
    }

    public void FillUser()
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
        gvUser.DataSource = objMasterUpdate.GetAllUserMasterList().Where(x => x.RoleId != 1).ToList();
        gvUser.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserManager.aspx");
    }
}