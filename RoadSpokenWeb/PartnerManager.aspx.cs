using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.IO;

public partial class PartnerManager : System.Web.UI.Page
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
        string strExtension = "";
        string strImageName = "";
        string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "FileUpload\\";

        if (fl.HasFile)
        {
            try
            {
                strExtension = System.IO.Path.GetExtension(fl.PostedFile.FileName);
                //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
                if (IsValidFile(ref fl))
                {
                    strImageName = Guid.NewGuid().ToString() + strExtension;
                    fl.PostedFile.SaveAs(path + strImageName);
                    //UploadFileToService(path + strImageName, strImageName);
                }
            }
            catch { }
        }

        objMasterUpdate.SavePartner(Convert.ToInt32(hdnuserid.Value), txtName.Text, strImageName, txtURL.Text);

           

            txtName.Text = "";
            txtURL.Text = "";
            FillUser();
            dvAddEdit.Visible = false;
            dvUserList.Visible = true;
        
    }
    public bool IsValidFile(ref System.Web.UI.WebControls.FileUpload FileName)
    {
        bool functionReturnValue = false;
        string strExtension = null;
        strExtension = Path.GetExtension(FileName.PostedFile.FileName).ToLower().Trim();

        if ((strExtension.ToLower().Trim() == ".jpg" | strExtension.ToLower().Trim() == ".jpeg" | strExtension.ToLower().Trim() == ".gif" | strExtension.ToLower().Trim() == ".bmp" | strExtension.ToLower().Trim() == ".png" | strExtension.ToLower().Trim() == ".JPG" | strExtension.ToLower().Trim() == ".JPEG" | strExtension.ToLower().Trim() == ".GIF" | strExtension.ToLower().Trim() == ".BMP" | strExtension.ToLower().Trim() == ".PNG"))
        {
            functionReturnValue = true;
        }
        else
        {
            functionReturnValue = false;
        }

        return functionReturnValue;
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
                var userObj = objMasterUpdate.GetPartnerById(Convert.ToInt32(e.CommandArgument));
                txtName.Text = userObj.PartnerName;
                txtURL.Text = userObj.PartnerURL;
                hdnuserid.Value = userObj.Id.ToString();
                dvUserList.Visible = false;
                dvAddEdit.Visible = true;
            }

            if (e.CommandName == "DeleteItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                objMasterUpdate.DeletePartnerById(Convert.ToInt32(e.CommandArgument));
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
        gvUser.DataSource = objMasterUpdate.GetAllPartners().ToList();
        gvUser.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PartnerManager.aspx");
    }
}