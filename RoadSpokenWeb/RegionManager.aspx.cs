using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class RegionManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRegion();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrError.Text = "";
        dvError.Visible = false;

        MasterUpdate objMasterUpdate = new MasterUpdate();


        objMasterUpdate.SaveRegionMaster(Convert.ToInt32(hdnuserid.Value), txtName.Text);



        txtName.Text = "";
        FillRegion();
        dvAddEdit.Visible = false;
        dvUserList.Visible = true;

    }


    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        dvAddEdit.Visible = true;
        dvUserList.Visible = false;
    }

    protected void gvRegion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                var userObj = objMasterUpdate.GetRegionById(Convert.ToInt32(e.CommandArgument));
                txtName.Text = userObj.RegionName;

                hdnuserid.Value = userObj.Id.ToString();
                dvUserList.Visible = false;
                dvAddEdit.Visible = true;
            }

            if (e.CommandName == "DeleteItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                objMasterUpdate.DeleteRegionById(Convert.ToInt32(e.CommandArgument));
                FillRegion();
            }
        }
        catch { }
    }


    protected void gvRegion_OnPageindexchanging(object sender, GridViewPageEventArgs e)
    {
        gvRegion.PageIndex = e.NewPageIndex;
        FillRegion();
    }

    public void FillRegion()
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
       gvRegion.DataSource = objMasterUpdate.GetRegionList();
       gvRegion.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegionManager.aspx");
    }
}