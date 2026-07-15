using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.IO;

public partial class CreateAdd : System.Web.UI.Page
{
    public string javascrpt = "";
    public string javascrpt2 = "";
    public string javascrpt3 = "";
    public string lat1 = "40.81380923056958";
    public string lng1 = "-74.02587890625";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillUser();
            FillRegion();
            FillHighway();
            txtStartDate.Attributes.Add("ReadOnly", "ReadOnly");
            txtEndDate.Attributes.Add("ReadOnly", "ReadOnly");
            txtAddress.Attributes.Add("readonly", "readonly");
            txtLongitude.Attributes.Add("readonly", "readonly");
            txtLatitude.Attributes.Add("readonly", "readonly");
        }

    }
    private void FillRegion()
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
        ddlRegion.DataSource = objMasterUpdate.GetRegionList().ToList();
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "Id";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select Interstate", "-1"));
    }
    protected void FillHighway()
    {
        MasterUpdate m = new MasterUpdate();
        var d = m.GetAllHighways();
        ddlHighwayName.DataSource = d;
        ddlHighwayName.DataTextField = "HighwayName";
        ddlHighwayName.DataValueField = "id";
        ddlHighwayName.DataBind();
        ddlHighwayName.Items.Insert(0, new ListItem("Select", "-1"));
        
    }
    protected void FillWaypoint(int highwayid,int regionid)
    {
        MasterUpdate m = new MasterUpdate();
        if (highwayid > 0 && regionid > 0)
        {
            var d = m.GetAllIntermediatePoint().Where(x => x.Highway == highwayid && x.RegionId == regionid).ToList();
            foreach (var dd in d)
            {
                if (Convert.ToString(dd.PointTitle).Trim() == "")
                {
                    dd.PointTitle = dd.PointTitle2;
                }
            }
            //ddlWaypoint.DataSource = d;
            //ddlWaypoint.DataTextField = "pointtitle";
            //ddlWaypoint.DataValueField = "id";
            //ddlWaypoint.DataBind();
            //ddlWaypoint.Items.Insert(0, new ListItem("Select", "-1"));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrError.Text = "";
        dvError.Visible = false;
        string strImageName = "";
        string strExtension = "";
       
        string strAudioExtension = "";
        string strAudioName = "";

        string strIntroAudioExtension = "";
        string strIntroAudioName = "";

        string strConAudioExtension = "";
        string strConAudioName = "";

        string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "FileUpload\\";

        if (imageUpload.HasFile)
        {
            try
            {
                strExtension = System.IO.Path.GetExtension(imageUpload.PostedFile.FileName);
                //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
                if (IsValidFile(ref imageUpload))
                {
                    strImageName = Guid.NewGuid().ToString() + strExtension;
                    imageUpload.PostedFile.SaveAs(path + strImageName);
                    //UploadFileToService(path + strImageName, strImageName);
                }
            }
            catch { }
        }

       
        if (audioUpload.HasFile)
        {
            try
            {
                strAudioExtension = System.IO.Path.GetExtension(audioUpload.PostedFile.FileName);
                //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
                if (IsValidAudioFile(ref audioUpload))
                {
                    strAudioName = Guid.NewGuid().ToString() + strAudioExtension;
                    audioUpload.PostedFile.SaveAs(path + strAudioName);
                    //UploadFileToService(path + strImageName, strImageName);
                }
            }
            catch { }
        }

        if (flIntro.HasFile)
        {
            try
            {
                strIntroAudioExtension = System.IO.Path.GetExtension(flIntro.PostedFile.FileName);
                //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
                if (IsValidAudioFile(ref flIntro))
                {
                    strIntroAudioName = Guid.NewGuid().ToString() + strIntroAudioExtension;
                    flIntro.PostedFile.SaveAs(path + strIntroAudioName);
                    //UploadFileToService(path + strImageName, strImageName);
                }
            }
            catch { }
        }

        if (flConclu.HasFile)
        {
            try
            {
                strConAudioExtension = System.IO.Path.GetExtension(flConclu.PostedFile.FileName);
                //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
                if (IsValidAudioFile(ref flConclu))
                {
                    strConAudioName = Guid.NewGuid().ToString() + strConAudioExtension;
                    flConclu.PostedFile.SaveAs(path + strConAudioName);                    
                }
            }
            catch { }
        }


        MasterUpdate objMasterUpdate = new MasterUpdate();
        string startDate = "";
        string endDate = "";
        if (txtStartDate.Text.Contains('/'))
        {
            string[] dispDate = txtStartDate.Text.Split('/');

            startDate = dispDate[1] + "/" + dispDate[0] + "/" + dispDate[2];

        }
        else
        {
            string[] dispDate = txtStartDate.Text.Split('-');

            startDate = dispDate[1] + "/" + dispDate[0] + "/" + dispDate[2];

        }
        if (txtEndDate.Text.Contains('/'))
        {
            string[] expDate = txtEndDate.Text.Split('/');
            endDate = expDate[1] + "/" + expDate[0] + "/" + expDate[2];
        }
        else
        {
            string[] expDate = txtEndDate.Text.Split('-');
            endDate = expDate[1] + "/" + expDate[0] + "/" + expDate[2];
        }

        startDate = hdnStartDate2.Value;
        endDate = hdnEndDate2.Value;
        var startposixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
        var endposixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
        var starttime = startposixTime.AddMilliseconds(Convert.ToDouble(startDate));
        var endtime = endposixTime.AddMilliseconds(Convert.ToDouble(endDate));
        objMasterUpdate.SaveAd(Convert.ToInt32(hdnId.Value), ddlHighway.SelectedValue, Convert.ToInt32(ddlHighwayName.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue), 0, txtPromotext.Text, strImageName, strAudioName, "", Convert.ToInt32(txtRadius.Text),
              //Convert.ToDateTime(starttime).AddHours(Convert.ToInt32(ddlHour.SelectedValue)).AddMinutes(Convert.ToInt32(ddlMinute.SelectedValue)),
                //    Convert.ToDateTime(endtime).AddHours(Convert.ToInt32(ddlEndHour.SelectedValue)).AddMinutes(Convert.ToInt32(ddlEndMinute.SelectedValue))
                  starttime,endtime,txtLatitude.Text,txtLongitude.Text,txtAddress.Text,txtTTS.Text,strConAudioName,strIntroAudioName
                    );


        txtPromotext.Text = "";
        txtRadius.Text = "";
        ddlHighway.SelectedIndex = 0;
        ddlHighwayName.SelectedIndex = 0;
        ddlRegion.SelectedIndex = 0;
        txtAddress.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtPromotext.Text = "";
        txtTTS.Text = "";
       
        hdnStartDate2.Value = "";
        hdnEndDate2.Value = "";
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        ddlEndHour.SelectedIndex = 0;
        ddlEndMinute.SelectedIndex = 0;
        ddlHour.SelectedIndex = 0;
        ddlMinute.SelectedIndex = 0;

        lnkaudioUpload.Text = "";
        imgBtn2.Visible = false;
        lnkIntroUoload.Text = "";
        imgbtnIntro.Visible = false;
        lnkConUpload.Text = "";
        imgbtnCon.Visible = false;
        lnkimageUpload.Text = "";
        imgBtn4.Visible = false;

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

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        dvAddEdit.Visible = true;
        dvUserList.Visible = false;
    }
    protected void ddlRegion_SelectedIndex(object sender, EventArgs e)
    {
        FillWaypoint(Convert.ToInt32(ddlHighwayName.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue));
    }
    protected void ddlHighwayName_SelectedIndex(object sender, EventArgs e)
    {
        FillWaypoint(Convert.ToInt32(ddlHighwayName.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue));
    }
    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //try
        //{
            if (e.CommandName == "EditItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                var userObj = objMasterUpdate.GetAdById(Convert.ToInt32(e.CommandArgument));
                txtPromotext.Text = userObj.AdText;
                ddlHighway.SelectedValue = userObj.HighwayRun.ToString();
                ddlHighwayName.SelectedValue = userObj.Highway.ToString();
                ddlRegion.SelectedValue = userObj.RegionId.ToString();
                FillWaypoint(Convert.ToInt32(ddlHighwayName.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue));
                //ddlWaypoint.SelectedValue = userObj.WayPointId.ToString();
                hdnId.Value = userObj.Id.ToString();
                txtPromotext.Text = userObj.AdText;
                txtRadius.Text =Convert.ToString( userObj.Radius);
                //txtStartDate.Text = userObj.StartTime.Value.ToString("MM/dd/yyyy");
                //txtEndDate.Text = userObj.EndTime.Value.ToString("MM/dd/yyyy");
                javascrpt = "markersLat.push(" + userObj.PointLatitude + ");markersLong.push(" + userObj.PointLongitude + ");";
                txtAddress.Text = userObj.PointAddress;
                txtLatitude.Text = userObj.PointLatitude;
                txtLongitude.Text = userObj.PointLongitude;
                txtTTS.Text = userObj.TTS;
                var startdate = userObj.StartTime.Value;

                var enddate = userObj.EndTime.Value;
                hdnStartDate.Value = startdate.Day.ToString() + "/" + startdate.Month.ToString() + "/" + startdate.Year.ToString() + "/" + startdate.Hour.ToString() + "/" + startdate.Minute.ToString() + "/" + startdate.Second.ToString();
                hdnEndDate.Value = enddate.Day.ToString() + "/" + enddate.Month.ToString() + "/" + enddate.Year.ToString() + "/" + enddate.Hour.ToString() + "/" + enddate.Minute.ToString() + "/" + enddate.Second.ToString();
                if (userObj.AudioFile != null && userObj.AudioFile != "")
                {
                    lnkaudioUpload.Text = userObj.AudioFile;
                    lnkaudioUpload.Visible = true;
                    imgBtn2.Visible = true;
                }

                if (userObj.Imagefile != null && userObj.Imagefile != "")
                {
                    lnkimageUpload.Text = userObj.Imagefile;
                    lnkimageUpload.Visible = true;
                    imgBtn4.Visible = true;
                }

                if (userObj.IntroMusic != null && userObj.IntroMusic != "")
                {
                    lnkIntroUoload.Text = userObj.IntroMusic;
                    lnkIntroUoload.Visible = true;
                    imgbtnIntro.Visible = true;
                }
                if (userObj.ConclusionMusic != null && userObj.ConclusionMusic != "")
                {
                    lnkConUpload.Text = userObj.ConclusionMusic;
                    lnkConUpload.Visible = true;
                    imgbtnCon.Visible = true;
                }

                dvUserList.Visible = false;
                dvAddEdit.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), " FillDateTime", "FillDateTime()", true);
            }

            if (e.CommandName == "DeleteItem")
            {
                MasterUpdate objMasterUpdate = new MasterUpdate();
                objMasterUpdate.DeleteAdById(Convert.ToInt32(e.CommandArgument));
                FillUser();
            }
        //}
        //catch { }
    }


    protected void gvUser_OnPageindexchanging(object sender, GridViewPageEventArgs e)
    {
        gvUser.PageIndex = e.NewPageIndex;
        FillUser();
    }

    public void FillUser()
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
        gvUser.DataSource = objMasterUpdate.GetAllAdBy().ToList();
        gvUser.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("createadd.aspx");
    }

    protected void lnkaudioUpload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkaudioUpload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkIntroUoload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkIntroUoload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkConUpload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkConUpload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
   
    protected void lnkimageUpload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkimageUpload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
        //ClientScript.RegisterStartupScript( GetType(), "openwindow", "<script type=text/javascript> window.open('FileUpload/" + AudioUrl + "'); </script>");
    }
   
    protected void imgBtn2_Click(object sender, EventArgs e)
    {
        lnkaudioUpload.Text = "";
        imgBtn2.Visible = false;
    }
    protected void imgbtnIntro_Click(object sender, EventArgs e)
    {
        lnkIntroUoload.Text = "";
        imgbtnIntro.Visible = false;
    }
    protected void imgbtnCon_Click(object sender, EventArgs e)
    {
        lnkConUpload.Text = "";
        imgbtnCon.Visible = false;
    }
   
    protected void imgBtn4_Click(object sender, EventArgs e)
    {
        lnkimageUpload.Text = "";
        imgBtn4.Visible = false;

    }
}