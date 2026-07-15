using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.IO;

public partial class EditRouteManager : System.Web.UI.Page
{
    public string javascrpt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["RouteId"] != null)
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                var pointObj = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(Request.QueryString["RouteId"]));

                txtAddress.Text = pointObj.PointAddress;
                txtLatitude.Text = pointObj.PointLatitude;
                txtLongitude.Text = pointObj.PointLongitude;
                javascrpt = "markersLat.push(" + pointObj.PointLatitude + ");markersLong.push(" + pointObj.PointLongitude + ");";
                txtNewsLink.Text = pointObj.NewsLink;
                txtPointTitle.Text = pointObj.PointTitle;
                txtNewsText.Text = pointObj.NewsText;
                hdnuserid.Value = pointObj.Id.ToString();
            }
            txtAddress.Attributes.Add("readonly", "readonly");
            txtLongitude.Attributes.Add("readonly", "readonly");
            txtLatitude.Attributes.Add("readonly", "readonly");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrError.Text = "";
        dvError.Visible = false;

        string strImageName = "";
        string strExtension = "";
        string strVideoExtension = "";
        string strVideoName = "";
        string strAudioExtension = "";
        string strAudioName = "";
        string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "FileUpload\\";

        if (imageUpload.HasFile)
        {
            try
            {
                strExtension = System.IO.Path.GetExtension(imageUpload.PostedFile.FileName);
      
                if (IsValidFile(ref imageUpload))
                {
                    strImageName = Guid.NewGuid().ToString() + strExtension;
                    imageUpload.PostedFile.SaveAs(path + strImageName);
            
                }
            }
            catch { }
        }

        if (videoUpload.HasFile)
        {
            try
            {
                strVideoExtension = System.IO.Path.GetExtension(videoUpload.PostedFile.FileName);
              
                if (IsValidVideoFile(ref videoUpload))
                {
                    strVideoName = Guid.NewGuid().ToString() + strVideoExtension;
                    videoUpload.PostedFile.SaveAs(path + strVideoName);
             
                }
            }
            catch { }
        }

        if (audioUpload.HasFile)
        {
            try
            {
                strAudioExtension = System.IO.Path.GetExtension(audioUpload.PostedFile.FileName);
        
                if (IsValidAudioFile(ref audioUpload))
                {
                    strAudioName = Guid.NewGuid().ToString() + strAudioExtension;
                    audioUpload.PostedFile.SaveAs(path + strAudioName);
          
                }
            }
            catch { }
        }


        MasterUpdate objMasterUpdate = new MasterUpdate();
       // objMasterUpdate.SaveIntermediatePoint(Convert.ToInt32(hdnuserid.Value), txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNewsText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text,0);
        txtAddress.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtNewsLink.Text = "";
        txtNewsText.Text = "";
        txtPointTitle.Text = "";
        Response.Redirect("IntermediatePointManager.aspx");
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

    public bool IsValidVideoFile(ref System.Web.UI.WebControls.FileUpload FileName)
    {
        bool functionReturnValue = false;
        string strExtension = null;
        strExtension = Path.GetExtension(FileName.PostedFile.FileName).ToLower().Trim();


        if ((strExtension.ToLower().Trim() == ".mpg" | strExtension.ToLower().Trim() == ".mov" | strExtension.ToLower().Trim() == ".wmv" | strExtension.ToLower().Trim() == ".rm" | strExtension.ToLower().Trim() == ".mkv" | strExtension.ToLower().Trim() == ".mp4" | strExtension.ToLower().Trim() == ".mxf" | strExtension.ToLower().Trim() == ".m4v" | strExtension.ToLower().Trim() == "avi" | strExtension.ToLower().Trim() == ".mps"))
        {
            functionReturnValue = true;
        }
        else
        {
            functionReturnValue = false;
        }

        return functionReturnValue;
    }

    public bool IsValidAudioFile(ref System.Web.UI.WebControls.FileUpload FileName)
    {
        bool functionReturnValue = false;
        string strExtension = null;
        strExtension = Path.GetExtension(FileName.PostedFile.FileName).ToLower().Trim();


        if ((strExtension.ToLower().Trim() == ".mp3" | strExtension.ToLower().Trim() == ".3ga" | strExtension.ToLower().Trim() == ".cda" | strExtension.ToLower().Trim() == ".moi" | strExtension.ToLower().Trim() == ".mv3" | strExtension.ToLower().Trim() == ".rec" | strExtension.ToLower().Trim() == ".m4a" | strExtension.ToLower().Trim() == ".sng" | strExtension.ToLower().Trim() == "ove" | strExtension.ToLower().Trim() == ".m4p"))
        {
            functionReturnValue = true;
        }
        else
        {
            functionReturnValue = false;
        }

        return functionReturnValue;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IntermediatePointManager.aspx");
    }

}