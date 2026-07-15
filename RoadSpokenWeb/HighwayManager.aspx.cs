using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class HighwayManager : System.Web.UI.Page
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

       
                objMasterUpdate.SaveHighway(Convert.ToInt32(hdnuserid.Value), txtName.Text);

           

            txtName.Text = "";
            FillUser();
            dvAddEdit.Visible = false;
            dvUserList.Visible = true;
        
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
                var userObj = objMasterUpdate.GetHighwayById(Convert.ToInt32(e.CommandArgument));
                txtName.Text = userObj.HighwayName;
              
                hdnuserid.Value = userObj.Id.ToString();
                dvUserList.Visible = false;
                dvAddEdit.Visible = true;
            }

            if (e.CommandName == "DeleteItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                objMasterUpdate.DeleteHighwayById(Convert.ToInt32(e.CommandArgument));
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
        gvUser.DataSource = objMasterUpdate.GetAllHighways().ToList();
        gvUser.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HighwayManager.aspx");
    }
}