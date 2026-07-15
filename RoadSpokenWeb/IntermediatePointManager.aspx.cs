using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.IO;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Drawing;
using System.Net;
using System.Xml;
using Newtonsoft.Json;
using System.Web.Services;
using DAL;
using System.Text;


public partial class IntermediatePointManager : System.Web.UI.Page
{
    public string javascrpt = "";
    public string javascrpt2 = "";
    public string javascrpt3 = "";
    public string lat1 = "40.81380923056958";
    public string lng1 = "-74.02587890625";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetCountry("24.507143283102852", "79.82666015625");
        GetRegion("", "");
        if (!IsPostBack)
        {
            FillHighway();
            FillRegion();
            
            imgBtn4.Visible = false;
            if (Request.QueryString["RouteId"] != null)
            {
                ddlDay.Enabled = true;
                MasterUpdate objMasterUpdate = new MasterUpdate();
              int id1 = 0;
                int id2 = 0;
                int routeid = Convert.ToInt32(Request.QueryString["RouteId"]);
                hdnRouteId.Value = Request.QueryString["RouteId"];
                if (routeid > 0)
                {
                    var dpoints = objMasterUpdate.GetIntermediatePointById(routeid);
                    if (dpoints != null)
                    {
                        ddlMainhighway.SelectedValue = dpoints.Highway.ToString();
                    }
                  
                    if (IsOdd(Convert.ToInt32(dpoints.Sorting)))
                    {
                        id1 = routeid;
                        var dpoints2 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(dpoints.Sorting + 1),Convert.ToInt32(ddlMainhighway.SelectedValue));
                        if (dpoints2 != null)
                            id2 = (int)dpoints2.Id;
                    }
                    else
                    {
                        var dpoints1 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(dpoints.Sorting - 1), Convert.ToInt32(ddlMainhighway.SelectedValue));
                        if (dpoints1 != null)
                            id1 = (int)dpoints1.Id;
                        id2 = routeid;
                    }
              
                    
                }
                var pointObj1 = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(id1));
                var pointObj2 = objMasterUpdate.GetIntermediatePointById(id2);
                FillForm(pointObj1,pointObj2);   
            }
            else
            {
                FillAngle(); 
            }
            txtAddress.Attributes.Add("readonly", "readonly");
            txtLongitude.Attributes.Add("readonly", "readonly");
            txtLatitude.Attributes.Add("readonly", "readonly");
            txtAddress2.Attributes.Add("readonly", "readonly");
            txtLongitude2.Attributes.Add("readonly", "readonly");
            txtLatitude2.Attributes.Add("readonly", "readonly");
            FillIntermediatePoint();    

        }
        FillWayPoints(Convert.ToInt32(ddlMainhighway.SelectedValue));
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

    private void FillAllPointBySorting(int sorting)
    {
         MasterUpdate objMasterUpdate = new MasterUpdate();
        
                int id1 = 0;
                int id2 = 0;
                int routeid = Convert.ToInt32(sorting);
                if (routeid > 0)
                {
                    var dpoints = objMasterUpdate.GetIntermediatePointById(routeid);

                    if (IsOdd(Convert.ToInt32(dpoints.Sorting)))
                    {
                        id1 = routeid;
                        var dpoints2 = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(dpoints.Sorting + 1));
                        if (dpoints2 != null)
                            id2 = (int)dpoints2.Sorting;
                    }
                    else
                    {
                        var dpoints1 = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(dpoints.Sorting - 1));
                        if (dpoints1 != null)
                            id1 = (int)dpoints1.Sorting;
                        id2 = routeid;
                    }
                }
                var pointObj1 = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(id1));
                var pointObj2 = objMasterUpdate.GetIntermediatePointById(id2);
                FillForm(pointObj1,pointObj2);
    }

    private void FillWayPoints(int highwayPoint)
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
        if (highwayPoint != -1)
        {
            var getWayPoints = objMasterUpdate.GetAllRoutePointsByHighway2(Convert.ToInt32(ddlMainhighway.SelectedValue));

            foreach (var getPoint in getWayPoints)
            {
                if (!string.IsNullOrEmpty(getPoint.PointLatitude1) && !string.IsNullOrEmpty(getPoint.PointLongitude1))
                {
                    javascrpt3 += "markersLat3.push(" + getPoint.PointLatitude1 + ");markersLong3.push(" + getPoint.PointLongitude1 + ");";
                }

                else
                {
                    javascrpt3 += "markersLat3.push(" + getPoint.PointLatitude2 + ");markersLong3.push(" + getPoint.PointLongitude2 + ");";
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "CreateMap3", "CreateMap3();", true);
 
    }

    private void FillForm(IntermediatePoint pointObj,IntermediatePoint pointObj2)
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
       
        //if (pointObj.Sorting == 1)
        //{
        //    // leftLinkBtn.Enabled = false;
        //}
        rightImg.Visible = true;
        dvMiddle.Visible = true;
       // int RoutePointCount = objMasterUpdate.GetAllIntermediatePoint().Count();
        int RoutePointCount = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue)).Count();
        if (Convert.ToInt32(pointObj.Sorting) == RoutePointCount)
        {
            // rightLinkBtn.Enabled = false;
        }
        ddlHighwayName.SelectedValue = pointObj.Highway!=null?pointObj.Highway.ToString():"-1";
        ddlRegion.SelectedValue = pointObj.RegionId != null ? pointObj.RegionId.ToString() : "-1";
        hdnSortingValue2.Value = pointObj.Sorting.ToString();
        hdnSortingValue.Value = pointObj.Sorting.ToString();
        txtSorting.Text = pointObj.Sorting.ToString();
        txtAddress.Text = pointObj.PointAddress;
        txtLatitude.Text = pointObj.PointLatitude;
        txtLongitude.Text = pointObj.PointLongitude;
        txtPromotext.Text = pointObj.PromoText;
        chkFrequency.Checked = false;
        if (pointObj.Frequency != null && pointObj.Frequency != "")
        {
            if (pointObj.Frequency == chkFrequency.Text)
            {
                chkFrequency.Checked = true;
            }
            //ddlFrequency.SelectedValue = pointObj.Frequency;
        }

        if (pointObj.DayFullName != null && pointObj.DayFullName != "")
        {
            ddlDay.SelectedValue = pointObj.DayFullName;
        }
        javascrpt = "markersLat.push(" + pointObj.PointLatitude + ");markersLong.push(" + pointObj.PointLongitude + ");";
        
        txtNewsLink.Text = pointObj.NewsLink;
        
        if (pointObj.IntroductoryMusicFile != null && pointObj.IntroductoryMusicFile != "")
        {
            lnkflIntroductory.Text = pointObj.IntroductoryMusicFile;
            lnkflIntroductory.Visible = true;
            imgBtn1.Visible = true;
        }
        if (pointObj.NewsAudio != null && pointObj.NewsAudio != "")
        {
            lnkaudioUpload.Text = pointObj.NewsAudio;
            lnkaudioUpload.Visible = true;
            imgBtn2.Visible = true;
        }
        if (pointObj.NewsVideo != null && pointObj.NewsVideo != "")
        {
            lnkvideoUpload.Text = pointObj.NewsVideo;
            lnkvideoUpload.Visible = true;
            imgBtn3.Visible = true;
        }
        if (pointObj.NewsImage != null && pointObj.NewsImage != "")
        {
            lnkimageUpload.Text = pointObj.NewsImage;
            lnkimageUpload.Visible = true;
            imgBtn4.Visible = true;
        }
        if (pointObj.ConclusionAudio != null && pointObj.ConclusionAudio != "")
        {
            lnkflConclusion.Text = pointObj.ConclusionAudio;
            lnkflConclusion.Visible = true;
            imgflConclusion.Visible = true;
        }

       
        if (pointObj.HighwayRuns != null)
        {
            if (ddlHighway.Items.FindByValue(Convert.ToString(pointObj.HighwayRuns)) != null)
            {
                ddlHighway.SelectedValue = pointObj.HighwayRuns;
                if (pointObj.HighwayRuns == "Northbound/Southbound")
                {
                    lblText1.Text = "Text for TTS NorthBound (▲)";
                    lblText2.Text = "Text for TTS SouthBound (▼)";


                }
                else
                {
                    lblText1.Text = "Text for TTS EastBound (►)";
                    lblText2.Text = "Text for TTS WestBound (◄)";

                    lblDirection1.Text = "Eastbound Point ►";
                    lbldirection2.Text = "Westbound Point ◄";

                }
            }
        }

        FillAngle();
        if (pointObj.Angle != null)
        {
            if (ddlAngle.Items.FindByValue(Convert.ToString(pointObj.Angle)) != null)
            {
                ddlAngle.SelectedValue = pointObj.Angle;
                imgAngle1.ImageUrl = "Images\\uptemp.png" + "?time2=" + DateTime.Now.ToString();
            }
        }
        
        txtPointTitle.Text = pointObj.PointTitle;
        
        txtNorthBoundText.Text = pointObj.NewsText;
      
        hdnuserid.Value = pointObj.Id.ToString();
        txtSorting.Text = pointObj.Sorting.ToString();


        if (pointObj2 != null)
        {
            txtSouthBoundText.Text = pointObj2.SouthBoundText;
            txtAddress2.Text = pointObj2.PointAddress2;
            txtLatitude2.Text = pointObj2.PointLatitude2;
            txtLongitude2.Text = pointObj2.PointLongitude2;
            javascrpt2 = "markersLat2.push(" + pointObj2.PointLatitude2 + ");markersLong2.push(" + pointObj2.PointLongitude2 + ");";
            txtSorting2.Text = pointObj2.Sorting.ToString();
            txtPointTitle2.Text = pointObj2.PointTitle2;
            txtPromotext2.Text = pointObj2.PromoText2;
            txtNewsLink2.Text = pointObj2.NewsLink2;
            if (pointObj.Angle2 != null)
            {
                if (ddlAngle2.Items.FindByValue(Convert.ToString(pointObj.Angle2)) != null)
                {
                    ddlAngle2.SelectedValue = pointObj.Angle2;
                    imgAngle2.ImageUrl = "Images\\uptemp2.png" + "?time2=" + DateTime.Now.ToString();
                }
            }
            if (pointObj2.IntroductoryMusicFile2 != null && pointObj2.IntroductoryMusicFile2 != "")
            {
                lnkflIntroductory2.Text = pointObj2.IntroductoryMusicFile2;
                lnkflIntroductory2.Visible = true;
                imgBtn5.Visible = true;
            }
            if (pointObj2.NewsAudio2 != null && pointObj2.NewsAudio2 != "")
            {
                lnkaudioUpload2.Text = pointObj2.NewsAudio2;
                lnkaudioUpload2.Visible = true;
                imgBtn6.Visible = true;
            }
            if (pointObj2.NewsVideo2 != null && pointObj2.NewsVideo2 != "")
            {
                lnkvideoUpload2.Text = pointObj2.NewsVideo2;
                lnkvideoUpload2.Visible = true;
                imgBtn7.Visible = true;
            }
            if (pointObj2.NewsImage2 != null && pointObj2.NewsImage2 != "")
            {
                lnkimageUpload2.Text = pointObj2.NewsImage2;
                lnkimageUpload2.Visible = true;
                imgBtn8.Visible = true;
            }
            if (pointObj.ConclusionAudio2 != null && pointObj.ConclusionAudio2 != "")
            {
                lnkflConclusion2.Text = pointObj.ConclusionAudio2;
                lnkflConclusion2.Visible = true;
                imgflConclusion2.Visible = true;
            }
        }
        else
        {
            txtSorting2.Text =Convert.ToString( Convert.ToInt32(txtSorting.Text)+1);
        }
        dvAddEdit.Visible = true;
        dvIntermediateList.Visible = false;
    }
    private void CreateMapDirectory(string country,string region)
    {
        string conPath = country; // your code goes here
        string regPath = region; // your code goes here

        bool exists = System.IO.Directory.Exists(Server.MapPath("Map\\" + conPath));

        if (!exists)
            System.IO.Directory.CreateDirectory(Server.MapPath("Map\\" + conPath));

        exists = System.IO.Directory.Exists(Server.MapPath("Map\\" + conPath + "\\" + regPath));

        if (!exists)
            System.IO.Directory.CreateDirectory(Server.MapPath("Map\\" + conPath + "\\" + regPath));
    }

    protected void FillAngle()
    {
        for (int i = 0; i <= 350; i = i + 10)
        {
            int count = ddlAngle.Items.Count;
            ddlAngle.Items.Insert(count, new ListItem(i.ToString(), i.ToString()));
            ddlAngle2.Items.Insert(count, new ListItem(i.ToString(), i.ToString()));
        }
        ddlAngle.Items.Insert(0, new ListItem("Select", "-1"));
        ddlAngle2.Items.Insert(0, new ListItem("Select", "-1"));
    }

    protected void FillHighway()
    {
        MasterUpdate m = new MasterUpdate();
        var d= m.GetAllHighways();
        ddlHighwayName.DataSource = d;
        ddlHighwayName.DataTextField = "HighwayName";
        ddlHighwayName.DataValueField = "id";
        ddlHighwayName.DataBind();
        ddlHighwayName.Items.Insert(0, new ListItem("Select", "-1"));

        ddlMainhighway.DataSource = d;
        ddlMainhighway.DataTextField = "HighwayName";
        ddlMainhighway.DataValueField = "id";
        ddlMainhighway.DataBind();
        if(d.Count==0)
        ddlMainhighway.Items.Insert(0, new ListItem("Select", "-1"));


        if (LoginSession.userInfo != null)
        {
            var d1 = m.GetSelectedHighway(Convert.ToInt32(LoginSession.userInfo.UserID));
            if (d1 != null)
            {
                ddlMainhighway.SelectedValue = d1.HighwayId.ToString();
                ddlHighwayName.SelectedValue = ddlMainhighway.SelectedValue;
            }
        }
        else
        {
             var d1 = m.GetSelectedHighway(0);
             if (d1 != null)
             {
                 ddlMainhighway.SelectedValue = d1.HighwayId.ToString();
                 ddlHighwayName.SelectedValue = ddlMainhighway.SelectedValue;
             }
             else
             {
                 var all = m.GetAllIntermediatePoint();
                 if (all.Count > 0)
                 {
                     var single = all.Last();
                     if (single != null)
                     {
                         ddlMainhighway.SelectedValue = single.Highway.ToString();
                         ddlHighwayName.SelectedValue = single.Highway.ToString();
                     }
                 }
             }
        }

        ddlHighwayName.Enabled = false;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveRoute();
    }
    public bool SaveRoute()
    {
        ltrError.Text = "";
        dvError.Visible = false;

        string strImageName = "";
        string strExtension = "";
        string strVideoExtension = "";
        string strVideoName = "";
        string strAudioExtension = "";
        string strAudioName = "";
        string strIntroAudioExtension = "";
        string strIntroAudioName = "";
        string strImageName2 = "";
        string strExtension2 = "";
        string strVideoExtension2 = "";
        string strVideoName2 = "";
        string strAudioExtension2 = "";
        string strAudioName2 = "";
        string strIntroAudioExtension2 = "";
        string strIntroAudioName2 = "";

        string strConcAudioExtension = "";
        string strConcAudioName = "";

        string strConcAudioExtension2 = "";
        string strConcAudioName2 = "";

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

        if (videoUpload.HasFile)
        {
            try
            {
                strVideoExtension = System.IO.Path.GetExtension(videoUpload.PostedFile.FileName);
                //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
                if (IsValidVideoFile(ref videoUpload))
                {
                    strVideoName = Guid.NewGuid().ToString() + strVideoExtension;
                    videoUpload.PostedFile.SaveAs(path + strVideoName);
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

        if (flIntroductory.HasFile)
        {
            try
            {
                strIntroAudioExtension = System.IO.Path.GetExtension(flIntroductory.PostedFile.FileName);
                if (IsValidAudioFile(ref flIntroductory))
                {
                    strIntroAudioName = Guid.NewGuid().ToString() + strIntroAudioExtension;
                    flIntroductory.PostedFile.SaveAs(path + strIntroAudioName);
                }
            }
            catch { }
        }




        if (imageUpload2.HasFile)
        {
            try
            {
                strExtension2 = System.IO.Path.GetExtension(imageUpload2.PostedFile.FileName);               
                if (IsValidFile(ref imageUpload2))
                {
                    strImageName2 = Guid.NewGuid().ToString() + strExtension2;
                    imageUpload2.PostedFile.SaveAs(path + strImageName2);                    
                }
            }
            catch { }
        }

        if (videoUpload2.HasFile)
        {
            try
            {
                strVideoExtension2 = System.IO.Path.GetExtension(videoUpload2.PostedFile.FileName);                
                if (IsValidVideoFile(ref videoUpload2))
                {
                    strVideoName2 = Guid.NewGuid().ToString() + strVideoExtension2;
                    videoUpload2.PostedFile.SaveAs(path + strVideoName2);                    
                }
            }
            catch { }
        }

        if (audioUpload2.HasFile)
        {
            try
            {
                strAudioExtension2 = System.IO.Path.GetExtension(audioUpload2.PostedFile.FileName);                
                if (IsValidAudioFile(ref audioUpload2))
                {
                    strAudioName2 = Guid.NewGuid().ToString() + strAudioExtension2;
                    audioUpload2.PostedFile.SaveAs(path + strAudioName2);                    
                }
            }
            catch { }
        }

        if (flIntroductory2.HasFile)
        {
            try
            {
                strIntroAudioExtension2 = System.IO.Path.GetExtension(flIntroductory2.PostedFile.FileName);
                if (IsValidAudioFile(ref flIntroductory2))
                {
                    strIntroAudioName2 = Guid.NewGuid().ToString() + strIntroAudioExtension2;
                    flIntroductory2.PostedFile.SaveAs(path + strIntroAudioName2);
                }
            }
            catch { }
        }
        if (flConclusion.HasFile)
        {
            try
            {
                strConcAudioExtension = System.IO.Path.GetExtension(flConclusion.PostedFile.FileName);
                if (IsValidAudioFile(ref flConclusion))
                {
                    strConcAudioName = Guid.NewGuid().ToString() + strConcAudioExtension;
                    flConclusion.PostedFile.SaveAs(path + strConcAudioName);
                }
            }
            catch { }
        }

        if (flConclusion2.HasFile)
        {
            try
            {
                strConcAudioExtension2 = System.IO.Path.GetExtension(flConclusion2.PostedFile.FileName);
                if (IsValidAudioFile(ref flConclusion2))
                {
                    strConcAudioName2 = Guid.NewGuid().ToString() + strConcAudioExtension2;
                    flConclusion2.PostedFile.SaveAs(path + strConcAudioName2);
                }
            }
            catch { }
        }


        MasterUpdate objMasterUpdate = new MasterUpdate();
        int RoutePointCount = 0;//objMasterUpdate.GetAllIntermediatePoint().Count();
        RoutePointCount = Convert.ToInt32(txtSorting.Text);
        int RoutePointCount2 = 0;//objMasterUpdate.GetAllIntermediatePoint().Count();
        RoutePointCount2 = Convert.ToInt32(txtSorting2.Text);
        if (hdnregion.Value == null || hdnregion.Value == "")
        {
            hdnregion.Value = GetRegion(txtLatitude.Text, txtLongitude.Text);
        }
        int id1 = 0;
        int id2 = 0;
        int isonemile1 = 0;
        int isonemile2 = 0;
        if (Convert.ToInt32(hdnuserid.Value) > 0)
        {
            var dpoints = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(hdnuserid.Value));
            
            if (IsOdd(Convert.ToInt32(dpoints.Sorting)))
            {
                id1 = Convert.ToInt32(hdnuserid.Value);
                var dpoints2 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(dpoints.Sorting + 1), Convert.ToInt32(ddlMainhighway.SelectedValue));
                if (dpoints2 != null)
                {
                    id2 = (int)dpoints2.Id;
                    isonemile1 = dpoints2.UpdatedFromOnePoint??0;
                }
            }
            else
            {
                var dpoints1 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(dpoints.Sorting - 1), Convert.ToInt32(ddlMainhighway.SelectedValue));
                if (dpoints1 != null)
                {
                    id1 = (int)dpoints1.Id;
                    isonemile2 = dpoints1.UpdatedFromOnePoint ?? 0;
                }
                id2 = Convert.ToInt32(hdnuserid.Value);
            }

            if (lnkflIntroductory.Text == "" && strIntroAudioName == "")
            {

                strIntroAudioName = "no";

            }
            if (lnkflConclusion.Text == "" && strConcAudioName == "")
            {

                strConcAudioName = "no";

            }
            if (lnkaudioUpload.Text == "" && strAudioName == "")
            {

                strAudioName = "no";

            }
            if (lnkvideoUpload.Text == "" && strVideoName == "")
            {
                strVideoName = "no";
            }
            if (lnkimageUpload.Text == "" && strImageName == "")
            {
                strImageName = "no";

            }
            if (hdn2.Value != "1")
            {
                if (lnkflIntroductory2.Text == "" && strIntroAudioName2 == "")
                {
                    strIntroAudioName2 = "no";
                }
                if (lnkaudioUpload2.Text == "" && strAudioName2 == "")
                {
                    strAudioName2 = "no";
                }
                if (lnkvideoUpload2.Text == "" && strVideoName2 == "")
                {
                    strVideoName2 = "no";
                }
                if (lnkimageUpload2.Text == "" && strImageName2 == "")
                {
                    strImageName2 = "no";
                }
                if (lnkflConclusion2.Text == "" && strConcAudioName2 == "")
                {

                    strConcAudioName2 = "no";

                }
            }
        }
        IntermediatePoint d1 = new IntermediatePoint();
        IntermediatePoint d2 = new IntermediatePoint();
        string frqncy = "Always";
        if (chkFrequency.Checked)
        {
            frqncy = chkFrequency.Text;
        }
        if (id1 == 0)
        {
            d1= objMasterUpdate.SaveIntermediatePoint(id1, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, (RoutePointCount), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, hdnDay.Value, frqncy);
        }
        else if (hdnDay.Value == "Monday")
        {
             d1 = objMasterUpdate.SaveIntermediatePoint(id1, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, (RoutePointCount), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
               "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, hdnDay.Value, frqncy);
             objMasterUpdate.UpdateFrequency(Convert.ToInt32(d1.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), frqncy);
             objMasterUpdate.UpdateLocation(Convert.ToInt32(d1.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), txtLatitude.Text, txtLongitude.Text, txtAddress.Text,1);
        }

        string country = GetCountry(txtLatitude.Text, txtLongitude.Text);
        if (id2 == 0)
        {
             d2 = objMasterUpdate.SaveIntermediatePoint(id2, "", "", "", "", "", "", "", "", "", (RoutePointCount2), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                 txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, hdnDay.Value, frqncy);
        }
        else if (hdnDay.Value == "Monday")
        {
             d2 = objMasterUpdate.SaveIntermediatePoint(id2, "", "", "", "", "", "", "", "", "", (RoutePointCount2), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, hdnDay.Value, frqncy);

             objMasterUpdate.UpdateFrequency(Convert.ToInt32(d2.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), frqncy);
             objMasterUpdate.UpdateLocation(Convert.ToInt32(d2.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), txtLatitude2.Text, txtLongitude2.Text, txtAddress2.Text, 2);
        }
        if (id1 == 0 || Convert.ToString(ViewState["Middle"]) == "1" || (isonemile1 == 1 && isonemile2== 1))
        {
            if (Convert.ToString(ViewState["Middle"]) == "1")
            {
                Savenewchild(Convert.ToInt32(d1.Sorting),Convert.ToInt32(ddlHighwayName.SelectedValue));
            }
            
                objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                  "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[1].Value, frqncy);

                objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                  "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[2].Value, frqncy);

                objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                  "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[3].Value, frqncy);

                objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                  "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[4].Value, frqncy);

                objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                  "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[5].Value, frqncy);

                objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
                  "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[6].Value, frqncy);



                objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                    txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[1].Value, frqncy);

                objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                    txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[2].Value, frqncy);
                objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                    txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[3].Value, frqncy);
                objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                    txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[4].Value, frqncy);
                objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                    txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[5].Value, frqncy);
                objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
                    txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, ddlDay.Items[6].Value, frqncy);

                objMasterUpdate.UpdateOnepointMile(d1.Id, d2.Id);
            
        }
        else if (hdnDay.Value!="Monday")
        {
            if (d1.Id == 0)
            {
                 d1 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(txtSorting.Text), Convert.ToInt32(ddlMainhighway.SelectedValue));
                 objMasterUpdate.UpdateFrequency(Convert.ToInt32(d1.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), frqncy);
                 objMasterUpdate.UpdateLocation(Convert.ToInt32(d1.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), txtLatitude.Text, txtLongitude.Text, txtAddress.Text, 1);
            }
            if (d2.Id == 0)
            {
                d2 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(txtSorting2.Text), Convert.ToInt32(ddlMainhighway.SelectedValue));
                objMasterUpdate.UpdateFrequency(Convert.ToInt32(d2.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), frqncy);
                objMasterUpdate.UpdateLocation(Convert.ToInt32(d2.Sorting), Convert.ToInt32(ddlMainhighway.SelectedValue), txtLatitude2.Text, txtLongitude2.Text, txtAddress2.Text, 2);
            }
            objMasterUpdate.SaveIntermediatePointChild(id1, d1.Id, 0, txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, Convert.ToInt32(d1.Sorting), ddlHighway.SelectedValue, "", strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue, txtPromotext.Text, hdnregion.Value,
             "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, hdnDay.Value, frqncy);

            objMasterUpdate.SaveIntermediatePointChild(id2, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(d2.Sorting), ddlHighway.SelectedValue, txtSouthBoundText.Text, "", ddlAngle.SelectedValue, ddlAngle2.SelectedValue, "", hdnregion.Value,
            txtAddress2.Text, txtLongitude2.Text, txtLatitude2.Text, txtNewsLink2.Text, strImageName2, strVideoName2, strAudioName2, txtPointTitle2.Text, strIntroAudioName2, txtPromotext2.Text, hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), strConcAudioName, strConcAudioName2, Convert.ToInt32(ddlRegion.SelectedValue), 0, hdnDay.Value, frqncy);
        }
  
        

      //  CreateMapDirectory(country, hdnregion.Value);
        txtAddress.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtNewsLink.Text = "";
        txtNorthBoundText.Text = "";
        txtPointTitle.Text = "";
        txtSouthBoundText.Text = "";
        ddlHighway.SelectedIndex = 0;
        txtPromotext.Text = "";
        hdnregion.Value = "";

        hdnuserid.Value = "0";
        txtAddress2.Text = "";
        txtLatitude2.Text = "";
        txtLongitude2.Text = "";
        txtNewsLink2.Text = "";        
        txtPointTitle2.Text = "";                
        txtPromotext2.Text = "";
      

        FillIntermediatePoint();
        dvAddEdit.Visible = false;
        dvIntermediateList.Visible = true;
        ddlAngle.SelectedIndex = 0;
        ddlAngle2.SelectedIndex = 0;
       
            lnkflIntroductory.Text = "";
            lnkflIntroductory.Visible = false;
       
            lnkaudioUpload.Text = "";
            lnkaudioUpload.Visible = false;
        
            lnkvideoUpload.Text ="";
            lnkvideoUpload.Visible = false;
            
                lnkimageUpload.Text = "";
                lnkimageUpload.Visible = false;


                lnkflIntroductory2.Text = "";
                lnkflIntroductory2.Visible = false;

                lnkaudioUpload2.Text = "";
                lnkaudioUpload2.Visible = false;

                lnkvideoUpload2.Text = "";
                lnkvideoUpload2.Visible = false;

                lnkimageUpload2.Text = "";
                lnkimageUpload2.Visible = false;

                lnkflConclusion.Text = "";
                lnkflConclusion.Visible = false;
                lnkflConclusion2.Text = "";
                lnkflConclusion2.Visible = false;


                imgBtn1.Visible = false;
                imgBtn2.Visible = false;
                imgBtn3.Visible = false;
                imgBtn4.Visible = false;
                imgBtn5.Visible = false;
                imgBtn6.Visible = false;
                imgBtn7.Visible = false;
                imgBtn8.Visible = false;
                imgflConclusion.Visible = false;
                imgflConclusion2.Visible = false;

                hdn2.Value = "";
                ViewState["Middle"] = "";
                dvMiddle.Visible = false;
                return true;
    }
    public  bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
    protected void btnSaveCopy_Click(object sender, EventArgs e)
    {
       // ltrError.Text = "";
       // dvError.Visible = false;

       // string strImageName = "";
       // string strExtension = "";
       // string strVideoExtension = "";
       // string strVideoName = "";
       // string strAudioExtension = "";
       // string strAudioName = "";
       // string strIntroAudioExtension = "";
       // string strIntroAudioName = "";
       // string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "FileUpload\\";

       // if (imageUpload.HasFile)
       // {
       //     try
       //     {
       //         strExtension = System.IO.Path.GetExtension(imageUpload.PostedFile.FileName);
       //         //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
       //         if (IsValidFile(ref imageUpload))
       //         {
       //             strImageName = Guid.NewGuid().ToString() + strExtension;
       //             imageUpload.PostedFile.SaveAs(path + strImageName);
       //             //UploadFileToService(path + strImageName, strImageName);
       //         }
       //     }
       //     catch { }
       // }

       // if (videoUpload.HasFile)
       // {
       //     try
       //     {
       //         strVideoExtension = System.IO.Path.GetExtension(videoUpload.PostedFile.FileName);
       //         //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
       //         if (IsValidVideoFile(ref videoUpload))
       //         {
       //             strVideoName = Guid.NewGuid().ToString() + strVideoExtension;
       //             videoUpload.PostedFile.SaveAs(path + strVideoName);
       //             //UploadFileToService(path + strImageName, strImageName);
       //         }
       //     }
       //     catch { }
       // }

       // if (audioUpload.HasFile)
       // {
       //     try
       //     {
       //         strAudioExtension = System.IO.Path.GetExtension(audioUpload.PostedFile.FileName);
       //         //System.IO.Path.GetFileNameWithoutExtension(flUpload.PostedFile.FileName) + strExtension;
       //         if (IsValidAudioFile(ref audioUpload))
       //         {
       //             strAudioName = Guid.NewGuid().ToString() + strAudioExtension;
       //             audioUpload.PostedFile.SaveAs(path + strAudioName);
       //             //UploadFileToService(path + strImageName, strImageName);
       //         }
       //     }
       //     catch { }
       // }
       // if (flIntroductory.HasFile)
       // {
       //     try
       //     {
       //         strIntroAudioExtension = System.IO.Path.GetExtension(flIntroductory.PostedFile.FileName);
       //         if (IsValidAudioFile(ref flIntroductory))
       //         {
       //             strIntroAudioName = Guid.NewGuid().ToString() + strIntroAudioExtension;
       //             flIntroductory.PostedFile.SaveAs(path + strIntroAudioName);
       //         }
       //     }
       //     catch { }
       // }

       // MasterUpdate objMasterUpdate = new MasterUpdate();
       // int RoutePointCount = 0;//objMasterUpdate.GetAllIntermediatePoint().Count();
       // RoutePointCount = Convert.ToInt32(txtSorting.Text);
       // if (hdnregion.Value == null || hdnregion.Value=="")
       // {
       //     hdnregion.Value = GetRegion(txtLatitude.Text, txtLongitude.Text);
       // }
       //// objMasterUpdate.SaveIntermediatePoint(Convert.ToInt32(hdnuserid.Value), txtAddress.Text, txtLongitude.Text, txtLatitude.Text, txtNewsLink.Text, txtNorthBoundText.Text, strImageName, strVideoName, strAudioName, txtPointTitle.Text, (RoutePointCount), ddlHighway.SelectedValue, txtSouthBoundText.Text, strIntroAudioName, ddlAngle.SelectedValue, ddlAngle2.SelectedValue,txtPromotext.Text, hdnregion.Value );
       //  string country= GetCountry(txtLatitude.Text, txtLongitude.Text);
       // CreateMapDirectory(country, hdnregion.Value);
       // txtAddress.Text = "";
       // txtLatitude.Text = "";
       // txtLongitude.Text = "";
       // txtNewsLink.Text = "";
       // txtNorthBoundText.Text = "";
       // txtPointTitle.Text = "";
       // txtSouthBoundText.Text = "";
       // ddlHighway.SelectedIndex = 0;
       // txtPromotext.Text = "";
       // FillIntermediatePoint();
       // dvAddEdit.Visible = false;
       // hdnregion.Value = "";
       // dvIntermediateList.Visible = true;
       // ddlAngle.SelectedIndex = 0;
       // ddlAngle2.SelectedIndex = 0;
       // lnkflIntroductory.Text = "";
       // lnkflIntroductory.Visible = false;

       // lnkaudioUpload.Text = "";
       // lnkaudioUpload.Visible = false;

       // lnkvideoUpload.Text = "";
       // lnkvideoUpload.Visible = false;
     
       //     lnkimageUpload.Text = "";
       //     lnkimageUpload.Visible =false;

        SaveRoute();
     
        ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('IntermediatePointManager.aspx?RouteId=" + Session["tempid"] + "'); </script>");
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
        dvIntermediateList.Visible = false;
        MasterUpdate objMasterUpdate = new MasterUpdate();
        FillWayPoints(Convert.ToInt32(ddlMainhighway.SelectedValue));
        //int RoutePointCount = objMasterUpdate.GetAllIntermediatePoint().Count();
        var allroutes = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue));
        int RoutePointCount =allroutes.Count();
        if (IsOdd(RoutePointCount))
        {
            var pointObj1 = objMasterUpdate.GetRouteIdBySortingValue(RoutePointCount, Convert.ToInt32(ddlMainhighway.SelectedValue));
            FillForm(pointObj1, null);
            txtSorting2.Text = (RoutePointCount + 1).ToString();
            hdn2.Value = "1";
        }
        else
        {
            txtSorting.Text = (RoutePointCount + 1).ToString();
            txtSorting2.Text = (RoutePointCount + 2).ToString();
            hdn2.Value = "";
        }
        allroutes = allroutes.OrderBy(x => x.Sorting).ToList();
        if (allroutes.Count > 0)
        {
            if (IsOdd((int)allroutes.Last().Sorting))
            {
                //javascrpt = "markersLat.push(" + allroutes.Last().PointLatitude + ");markersLong.push(" + allroutes.Last().PointLongitude + ");";
                //javascrpt2 = "markersLat2.push(" + allroutes.Last().PointLatitude + ");markersLong2.push(" + allroutes.Last().PointLongitude + ");";
                lat1 = allroutes.Last().PointLatitude;
                lng1 = allroutes.Last().PointLongitude;
            }
            else
            {
                //javascrpt = "markersLat.push(" + allroutes.Last().PointLatitude2 + ");markersLong.push(" + allroutes.Last().PointLongitude2 + ");";
                //javascrpt2 = "markersLat2.push(" + allroutes.Last().PointLatitude2 + ");markersLong2.push(" + allroutes.Last().PointLongitude2 + ");";
                lat1 = allroutes.Last().PointLatitude2;
                lng1 = allroutes.Last().PointLongitude2;
            }

            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "load", "LoadMaps()", true);
        }
    }
    private void LoadDefaultMap(List<IntermediatePoint> allroutes)
    {
        allroutes = allroutes.OrderBy(x => x.Sorting).ToList();
        if (allroutes.Count > 0)
        {
            if (IsOdd((int)allroutes.Last().Sorting))
            {
                //javascrpt = "markersLat.push(" + allroutes.Last().PointLatitude + ");markersLong.push(" + allroutes.Last().PointLongitude + ");";
                //javascrpt2 = "markersLat2.push(" + allroutes.Last().PointLatitude + ");markersLong2.push(" + allroutes.Last().PointLongitude + ");";
                lat1 = allroutes.Last().PointLatitude;
                lng1 = allroutes.Last().PointLongitude;
            }
            else
            {
                //javascrpt = "markersLat.push(" + allroutes.Last().PointLatitude2 + ");markersLong.push(" + allroutes.Last().PointLongitude2 + ");";
                //javascrpt2 = "markersLat2.push(" + allroutes.Last().PointLatitude2 + ");markersLong2.push(" + allroutes.Last().PointLongitude2 + ");";
                lat1 = allroutes.Last().PointLatitude2;
                lng1 = allroutes.Last().PointLongitude2;
            }

            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "load", "LoadMaps()", true);
        }
    }
    protected void gvIntermediatePoint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lnkVideo = (LinkButton)e.Row.FindControl("lnkVideo");
        LinkButton lnkAudio=(LinkButton)e.Row.FindControl("lnkAudio");
        TextBox txtSorting = (TextBox)e.Row.FindControl("txtSorting");
        System.Web.UI.WebControls.Image imagPromo = (System.Web.UI.WebControls.Image)e.Row.FindControl("newsImage");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var watchVideo = DataBinder.Eval(e.Row.DataItem, "NewsVideo");
            var listenAudio = DataBinder.Eval(e.Row.DataItem, "NewsAudio");
            var promoImage = DataBinder.Eval(e.Row.DataItem, "NewsImage");
            var listenAudio2 = DataBinder.Eval(e.Row.DataItem, "NewsAudio2");
            var promoImage2 = DataBinder.Eval(e.Row.DataItem, "NewsImage2");
            var sortingNo = DataBinder.Eval(e.Row.DataItem, "Sorting");
            if (!string.IsNullOrEmpty(Convert.ToString(watchVideo)))
            {
                lnkVideo.Enabled = true;
            }
            else
            {
                lnkVideo.Enabled = false;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(listenAudio)) && IsOdd(Convert.ToInt32(sortingNo)))
            {
                lnkAudio.Enabled = true;
            }
            else if (!string.IsNullOrEmpty(Convert.ToString(listenAudio2)) && !IsOdd(Convert.ToInt32(sortingNo)))
            {
                lnkAudio.Enabled = true;
            }
            else
            {
                lnkAudio.Enabled = false;
            }
            if (string.IsNullOrEmpty(Convert.ToString(promoImage)) && IsOdd(Convert.ToInt32(sortingNo)))
            {
                imagPromo.ImageUrl = "FileUpload/No_image_available.jpg";

            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(promoImage2)) && !IsOdd(Convert.ToInt32(sortingNo)))
                {
                    imagPromo.ImageUrl = "FileUpload/No_image_available.jpg";

                }
            }
            if (sortingNo != null)
            {
                txtSorting.Text = sortingNo.ToString();
            }
        }
    }


    protected void gvIntermediatePoint_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditItem")
        {
            //ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('IntermediatePointManager.aspx?RouteId=" + e.CommandArgument + "'); </script>");
            Response.Redirect("IntermediatePointManager.aspx?RouteId=" + e.CommandArgument);
        }

        if (e.CommandName == "DeleteItem")
        {
            MasterUpdate objMasterUpdate = new MasterUpdate();
            objMasterUpdate.DeleteIntermediatePoint(Convert.ToInt32(e.CommandArgument),Convert.ToInt32(ddlMainhighway.SelectedValue));          
            FillIntermediatePoint();
        }


        if (e.CommandName == "WatchVideo")
        {
            MasterUpdate objMasterUpdate = new MasterUpdate();
            var pointObj = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(e.CommandArgument));
            var videoUrl = pointObj.NewsVideo;
            ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('FileUpload/" + videoUrl + "'); </script>");
        }

        if (e.CommandName == "ListenAudio")
        {
            MasterUpdate objMasterUpdate = new MasterUpdate();
            var pointObj = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(e.CommandArgument));
            if (!string.IsNullOrEmpty(Convert.ToString(pointObj.NewsAudio)) && IsOdd(Convert.ToInt32(pointObj.Sorting)))
            {
                var AudioUrl = pointObj.NewsAudio;
                ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('FileUpload/" + AudioUrl + "'); </script>");
            }
            else if (!string.IsNullOrEmpty(Convert.ToString(pointObj.NewsAudio2)) && !IsOdd(Convert.ToInt32(pointObj.Sorting)))
            {
                var AudioUrl = pointObj.NewsAudio2;
                ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('FileUpload/" + AudioUrl + "'); </script>");
            }
           
        }

    }


    protected void gvIntermediatePoint_OnPageindexchanging(object sender, GridViewPageEventArgs e)
    {
        gvIntermediatePoint.PageIndex = e.NewPageIndex;
        FillIntermediatePoint();
    }

    public void FillIntermediatePoint()
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
        var d = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue)).OrderBy(x => x.Sorting).ToList();
        if (rdbRoutePointType.SelectedValue == "WithTTS")
        {
            d = d.Where(x => (x.PointTitle2 != "" && x.Sorting % 2 == 0) || (x.PointTitle != "" && x.Sorting % 2 != 0)).ToList();
        }
        else if (rdbRoutePointType.SelectedValue == "WithoutTTS")
        {
            d = d.Where(x => (x.PointTitle2 == "" && x.Sorting % 2 == 0) || (x.PointTitle == "" && x.Sorting % 2 != 0)).ToList();
        }
        gvIntermediatePoint.DataSource = d;
        gvIntermediatePoint.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IntermediatePointManager.aspx");
    }

    protected void Sorting_Changed(object sender, EventArgs e)
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();
        TextBox txtSort = (TextBox)sender;
        var Id = txtSort.Attributes["CommandArgument"];
        //int RoutePointCount = objMasterUpdate.GetAllIntermediatePoint().Count();
        int RoutePointCount = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue)).Count();
        if (!string.IsNullOrEmpty(txtSort.Text) && Convert.ToInt32(txtSort.Text) <= RoutePointCount && Convert.ToInt32(txtSort.Text)!=0)
        {
            var getSortValue = objMasterUpdate.GetSortingValue(Convert.ToInt32(Id));
            var getRouteId = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(txtSort.Text), Convert.ToInt32(ddlMainhighway.SelectedValue));
            objMasterUpdate.SaveSortingNumber(Convert.ToInt32(getRouteId.Id), Convert.ToInt32(getSortValue.Sorting));
            objMasterUpdate.SaveSortingNumber(Convert.ToInt32(Id), Convert.ToInt32(txtSort.Text));       
        }
        FillIntermediatePoint();
    }

    protected void LeftBtnClick(object sender, EventArgs e)
    {
        SaveRoute();
        if (txtSorting.Text == "1")
        {
            Response.Redirect("IntermediatePointManager.aspx");
        }
        else
        {
            MasterUpdate objMasterUpdate = new MasterUpdate();
            if ((Request.QueryString["RouteId"]) != null)
            {
                var getSortingValue = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(Request.QueryString["RouteId"]));
                hdnSortingValue.Value = getSortingValue.Sorting.ToString();
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable 
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove 
                this.Request.QueryString.Remove("RouteId");
            }

            //else if ((Request.QueryString["RouteId"]) == null && hdnSortingValue.Value == "")
            //{
            //   // int RoutePoint = objMasterUpdate.GetAllIntermediatePoint().Count();
            //    int RoutePoint = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue)).Count();
            //    var getRoutePointValue = objMasterUpdate.GetRouteIdBySortingValue(RoutePoint, Convert.ToInt32(ddlMainhighway.SelectedValue));
            //    ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('IntermediatePointManager.aspx?RouteId=" + getRoutePointValue.Id + "'); </script>");
            //}
            hdnSortingValue.Value = txtSorting.Text;
            if (!string.IsNullOrEmpty(hdnSortingValue.Value))
            {
                hdnDay.Value = "Monday";
                ddlDay.SelectedValue = "Monday";
                if (hdnSortingValue.Value != "1" && hdnSortingValue.Value != "2")
                {
                    int add = 1;
                    if (IsOdd(Convert.ToInt32(hdnSortingValue.Value)))
                        add = 2;
                    var pointObj1 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(hdnSortingValue.Value) - add, Convert.ToInt32(ddlMainhighway.SelectedValue));
                    var pointObj2 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(hdnSortingValue.Value) - (add - 1), Convert.ToInt32(ddlMainhighway.SelectedValue));
                    FillForm(pointObj1, pointObj2);
                }
                else
                {
                    var pointObj1 = objMasterUpdate.GetRouteIdBySortingValue(1, Convert.ToInt32(ddlMainhighway.SelectedValue));
                    var pointObj2 = objMasterUpdate.GetRouteIdBySortingValue(2, Convert.ToInt32(ddlMainhighway.SelectedValue));
                    FillForm(pointObj1, pointObj2);
                }
            }
        }
        //else
        //{
        //    leftLinkBtn.Enabled = false;
        //}
    }

    protected void RightBtnClick(object sender, EventArgs e)
    {
        SaveRoute();
        MasterUpdate objMasterUpdate = new MasterUpdate();
        if ((Request.QueryString["RouteId"]) != null)
        {
            var getSortingValue = objMasterUpdate.GetIntermediatePointById(Convert.ToInt32(Request.QueryString["RouteId"]));
            hdnSortingValue.Value = getSortingValue.Sorting.ToString();
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable 
            isreadonly.SetValue(this.Request.QueryString, false, null);
            // remove 
            this.Request.QueryString.Remove("RouteId");
        }
        //int RoutePointCount = objMasterUpdate.GetAllIntermediatePoint().Count();
        int RoutePointCount = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue)).Count();
        hdnSortingValue.Value=txtSorting.Text;
        hdnDay.Value = "Monday";
        ddlDay.SelectedValue = "Monday";
        if (Convert.ToInt32(hdnSortingValue.Value) < RoutePointCount)
        {
            var pointObj = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(hdnSortingValue.Value) + 2, Convert.ToInt32(ddlMainhighway.SelectedValue));
            var pointObj2 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(hdnSortingValue.Value) + 3, Convert.ToInt32(ddlMainhighway.SelectedValue));
            if (pointObj != null)
            {
                FillForm(pointObj, pointObj2);
            }
            else
            {
                txtSorting.Text =Convert.ToString( Convert.ToInt32(txtSorting.Text) + 2);
                txtSorting2.Text =Convert.ToString( Convert.ToInt32(txtSorting.Text) + 1);
                dvAddEdit.Visible = true;
                dvIntermediateList.Visible = false;
                ddlDay.Enabled = false;
            }

        }
        else
        {
            txtSorting.Text = Convert.ToString(Convert.ToInt32(hdnSortingValue.Value) + 1);
            dvAddEdit.Visible = true;
            dvIntermediateList.Visible = false;
        }
          var allroutes = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue));
          LoadDefaultMap(allroutes);
 
    }

    protected void ddlHighwayRuns_Changed(object sender, EventArgs e)
    {
        if (ddlHighway.SelectedValue == "Northbound/Southbound")
        {
            lblText1.Text = "Text for TTS NorthBound";
            lblText2.Text = "Text for TTS SouthBound";
        }
        else
        {
            lblText1.Text = "Text for TTS EastBound";
            lblText2.Text = "Text for TTS WestBound";
        }
    }

    public static Bitmap RotateImage(System.Drawing.Image image, float angle)
    {
        return RotateImage(image, new PointF((float)image.Width / 2, (float)image.Height / 2), angle);
    }

    /// <summary>
    /// Creates a new Image containing the same image only rotated
    /// </summary>
    /// <param name="image">The <see cref="System.Drawing.Image"/> to rotate</param>
    /// <param name="offset">The position to rotate from.</param>
    /// <param name="angle">The amount to rotate the image, clockwise, in degrees</param>
    /// <returns>A new <see cref="System.Drawing.Bitmap"/> of the same size rotated.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown if <see cref="image"/> is null.</exception>
    public static Bitmap RotateImage(System.Drawing.Image image, PointF offset, float angle)
    {
        if (image == null)
            throw new ArgumentNullException("image");

        //create a new empty bitmap to hold rotated image
        Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
        rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //make a graphics object from the empty bitmap
        Graphics g = Graphics.FromImage(rotatedBmp);

        //Put the rotation point in the center of the image
        g.TranslateTransform(offset.X, offset.Y);

        //rotate the image
        g.RotateTransform(angle);

        //move the image back
        g.TranslateTransform(-offset.X, -offset.Y);

        //draw passed in image onto graphics object
        g.DrawImage(image, new PointF(0, 0));

        return rotatedBmp;
    }
    protected void ddlAngle_SelectedIndexChanged(object sender, EventArgs e)
    {
        string filaname = "";
        if (Convert.ToInt32(ddlAngle.SelectedValue) > 90 && Convert.ToInt32(ddlAngle.SelectedValue) <= 270)
        {
            filaname = Server.MapPath("Images\\down.png");
        }
        else
        {
            filaname = Server.MapPath("Images\\up.png");
        }
        System.Drawing.Bitmap a = new System.Drawing.Bitmap(filaname);
        System.Drawing.Image image = new System.Drawing.Bitmap(filaname);
        a = RotateImage(image, Convert.ToInt32(ddlAngle.SelectedValue));
        System.Drawing.Bitmap b = new System.Drawing.Bitmap(a);
        a.Dispose();
        b.Save(Server.MapPath("Images\\uptemp.png"), System.Drawing.Imaging.ImageFormat.Png);
        b.Dispose();
        imgAngle1.ImageUrl = "";

        imgAngle1.ImageUrl = "Images\\uptemp.png" + "?time=" + DateTime.Now.ToString();
        
         
    }
    protected void ddlAngle2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string filaname = "";
        if (Convert.ToInt32(ddlAngle2.SelectedValue) > 90 && Convert.ToInt32(ddlAngle2.SelectedValue) <= 270)
        {
            filaname = Server.MapPath("Images\\down.png");
        }
        else
        {
            filaname = Server.MapPath("Images\\up.png");
        }
        System.Drawing.Bitmap a = new System.Drawing.Bitmap(filaname);
        System.Drawing.Image image = new System.Drawing.Bitmap(filaname);
        a = RotateImage(image, Convert.ToInt32(ddlAngle2.SelectedValue));
        System.Drawing.Bitmap b = new System.Drawing.Bitmap(a);
        a.Dispose();
        b.Save(Server.MapPath("Images\\uptemp2.png"), System.Drawing.Imaging.ImageFormat.Png);
        b.Dispose();
        imgAngle2.ImageUrl = "";

        imgAngle2.ImageUrl = "Images\\uptemp2.png" + "?time2=" + DateTime.Now.ToString();

    }

    protected void ddlMainhighway_SelectedIndex(object sender, EventArgs e)
    {
         MasterUpdate m = new MasterUpdate();
         if (LoginSession.userInfo != null)
         {
             if (ddlMainhighway.SelectedValue != "-1")
             {
                 m.SelectHighway(Convert.ToInt32(ddlMainhighway.SelectedValue), Convert.ToInt32(LoginSession.userInfo.UserID));
             }

         }
         else
         {
             if (ddlMainhighway.SelectedValue != "-1")
             {
                 m.SelectHighway(Convert.ToInt32(ddlMainhighway.SelectedValue), 0);
             }
         }
        if (ddlMainhighway.SelectedValue != "-1")
        {
            ddlHighwayName.SelectedValue = ddlMainhighway.SelectedValue;
            ddlHighwayName.Enabled = false;
        }
        FillIntermediatePoint();
    }


    protected void lnkflIntroductory_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkflIntroductory.Text;
       // ClientScript.RegisterStartupScript(GetType(), "openwindow", "<script type=text/javascript> window.open('FileUpload/" + AudioUrl + "'); </script>");
        ScriptManager.RegisterStartupScript(this,this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');",true);
    }
    protected void lnkaudioUpload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkaudioUpload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkvideoUpload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkvideoUpload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkimageUpload_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkimageUpload.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
        //ClientScript.RegisterStartupScript( GetType(), "openwindow", "<script type=text/javascript> window.open('FileUpload/" + AudioUrl + "'); </script>");
    }

    protected void lnkflIntroductory2_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkflIntroductory2.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkaudioUpload2_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkaudioUpload2.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkvideoUpload2_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkvideoUpload2.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkimageUpload2_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkimageUpload2.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }

    protected void lnkflConclusion2_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkflConclusion2.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }
    protected void lnkflConclusion_Click(object sender, EventArgs e)
    {
        var AudioUrl = lnkflConclusion.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", " window.open('FileUpload/" + AudioUrl + "');", true);
    }

    protected void imgflConclusion_Click(object sender, EventArgs e)
    {
        lnkflConclusion.Text = "";
        imgflConclusion.Visible = false;

    }

    protected void imgflConclusion2_Click(object sender, EventArgs e)
    {
        lnkflConclusion2.Text = "";
        imgflConclusion2.Visible = false;

    }


    protected void imgBtn1_Click(object sender, EventArgs e)
    {
        lnkflIntroductory.Text = "";
        imgBtn1.Visible = false;
    }
    protected void imgBtn2_Click(object sender, EventArgs e)
    {
         lnkaudioUpload.Text="";
         imgBtn2.Visible = false;
    }
    protected void imgBtn3_Click(object sender, EventArgs e)
    {
         lnkvideoUpload.Text="";
         imgBtn3.Visible = false;
        
    }
    protected void imgBtn4_Click(object sender, EventArgs e)
    {
         lnkimageUpload.Text="";
         imgBtn4.Visible = false;
        
    }

    protected void imgBtn5_Click(object sender, EventArgs e)
    {
        lnkflIntroductory2.Text="";
        imgBtn5.Visible = false;
        
    }
    protected void imgBtn6_Click(object sender, EventArgs e)
    {
         lnkaudioUpload2.Text="";
         imgBtn6.Visible = false;
        
    }
    protected void imgBtn7_Click(object sender, EventArgs e)
    {
        lnkvideoUpload2.Text="";
        imgBtn7.Visible = false;
        
    }
    protected void imgBtn8_Click(object sender, EventArgs e)
    {
        lnkimageUpload2.Text="";
        imgBtn8.Visible = false;
        
    }


    [WebMethod]
    public static string GetRegion(string lat, string longi)
    {
        string city = "";
        try
        {
            string xmlStr="";
            using (var wc = new WebClient())
            {
              xmlStr = wc.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + longi + "&key=AIzaSyATOL3Yl2vqQubW44Vkrn4J24d5-6mIlpA");  //AIzaSyAIZLXRx3X9uplgcEwC9hFHw58Xrd0l5ls
              
            }
            var api = JsonConvert.DeserializeObject<APIAddress>(xmlStr);
            foreach (var d in api.results)
            {
                foreach (var p in d.address_components)
                {
                    foreach (var t in p.types)
                    {
                        if (t == "administrative_area_level_1")
                        {
                            city = p.long_name;
                        }
                        
                    }

                }
            }
        }
        catch (Exception ex) { }            
        return city;
    }

   
    public  string GetCountry(string lat, string longi)
    {
        string country = "";
        try
        {
            string xmlStr;
            using (var wc = new WebClient())
            {
                xmlStr = wc.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + longi + "&key=AIzaSyAIZLXRx3X9uplgcEwC9hFHw58Xrd0l5ls");
            }
            var api = JsonConvert.DeserializeObject<APIAddress>(xmlStr);
            foreach (var d in api.results)
            {
                foreach (var p in d.address_components)
                {
                    foreach (var t in p.types)
                    {
                        if (t == "country")
                        {
                            country = p.long_name;
                        }

                    }

                }
            }
        }
        catch { }
        return country;
    }


    protected void btnAddMiddleRoute_Click(object sender,EventArgs e)
    {
        ViewState["Middle"] = "1";
        if ((Request.QueryString["RouteId"]) != null)
        {           
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable 
            isreadonly.SetValue(this.Request.QueryString, false, null);
            // remove 
            this.Request.QueryString.Remove("RouteId");
        }
        ddlDay.Enabled = false;
        txtAddress.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtNewsLink.Text = "";
        txtNorthBoundText.Text = "";
        txtPointTitle.Text = "";
        txtSouthBoundText.Text = "";
       // ddlHighway.SelectedIndex = 0;
        txtPromotext.Text = "";
        hdnregion.Value = "";

        hdnuserid.Value = "0";
        txtAddress2.Text = "";
        txtLatitude2.Text = "";
        txtLongitude2.Text = "";
        txtNewsLink2.Text = "";
        txtPointTitle2.Text = "";
        txtPromotext2.Text = "";


       
       
        ddlAngle.SelectedIndex = 0;
        ddlAngle2.SelectedIndex = 0;

        lnkflIntroductory.Text = "";
        lnkflIntroductory.Visible = false;

        lnkaudioUpload.Text = "";
        lnkaudioUpload.Visible = false;

        lnkvideoUpload.Text = "";
        lnkvideoUpload.Visible = false;

        lnkimageUpload.Text = "";
        lnkimageUpload.Visible = false;


        lnkflIntroductory2.Text = "";
        lnkflIntroductory2.Visible = false;

        lnkaudioUpload2.Text = "";
        lnkaudioUpload2.Visible = false;

        lnkvideoUpload2.Text = "";
        lnkvideoUpload2.Visible = false;

        lnkimageUpload2.Text = "";
        lnkimageUpload2.Visible = false;

        imgBtn1.Visible = false;
        imgBtn2.Visible = false;
        imgBtn3.Visible = false;
        imgBtn4.Visible = false;
        imgBtn5.Visible = false;
        imgBtn6.Visible = false;
        imgBtn7.Visible = false;
        imgBtn8.Visible = false;

        hdn2.Value = "";
    }


    //protected void Map_Click(object sender, EventArgs e)
    //{
    //    dvWayPoint.Attributes.Add("Style", "display:block;");
    //    FillWayPoints(Convert.ToInt32(ddlMainhighway.SelectedValue));
    //}


    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {

        var dresult=SaveRoute();
        if (dresult)
        {
            hdnDay.Value = ddlDay.SelectedValue;
            MasterUpdate objMasterUpdate = new MasterUpdate();
            var pointObj = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(txtSorting.Text), Convert.ToInt32(ddlMainhighway.SelectedValue));
            var pointObj2 = objMasterUpdate.GetRouteIdBySortingValue(Convert.ToInt32(txtSorting2.Text), Convert.ToInt32(ddlMainhighway.SelectedValue));

            if (pointObj != null)
            {
                if (ddlDay.SelectedValue == "Monday")
                {
                    FillForm(pointObj, pointObj2);
                   // dvMapOuter1.Visible = true;
                }
                else
                {
                    var point1 = objMasterUpdate.GetIntermediatePointChildById(pointObj.Id, 0, ddlDay.SelectedValue);
                    var point2 = objMasterUpdate.GetIntermediatePointChildById(0, pointObj2.Id, ddlDay.SelectedValue);
                    FillFormChild(point1, point2, Convert.ToInt32(txtSorting.Text), pointObj.Id, pointObj2.Id);
                    //dvMapOuter1.Visible = false;
                }
            }
        }
    }

    private void FillFormChild(IntermediatePointsChild pointObj, IntermediatePointsChild pointObj2,int sorting,int id1,int id2)
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();

        //if (pointObj.Sorting == 1)
        //{
        //    // leftLinkBtn.Enabled = false;
        //}
        rightImg.Visible = true;
        dvMiddle.Visible = true;
        // int RoutePointCount = objMasterUpdate.GetAllIntermediatePoint().Count();
        int RoutePointCount = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlMainhighway.SelectedValue)).Count();
        if (Convert.ToInt32(pointObj.Sorting) == RoutePointCount)
        {
            // rightLinkBtn.Enabled = false;
        }
        ddlHighwayName.SelectedValue = pointObj.Highway != null ? pointObj.Highway.ToString() : "-1";
        ddlRegion.SelectedValue = pointObj.RegionId != null ? pointObj.RegionId.ToString() : "-1";
        hdnSortingValue2.Value = sorting.ToString();
        hdnSortingValue.Value = sorting.ToString();
        txtSorting.Text = sorting.ToString();
        txtAddress.Text = pointObj.PointAddress;
        txtLatitude.Text = pointObj.PointLatitude;
        txtLongitude.Text = pointObj.PointLongitude;
        txtPromotext.Text = pointObj.PromoText;
        chkFrequency.Checked = false;
        if (pointObj.Frequency != null && pointObj.Frequency != "")
        {
            if (pointObj.Frequency == chkFrequency.Text)
            {
                chkFrequency.Checked = true;
            }
         //   ddlFrequency.SelectedValue = pointObj.Frequency;
        }

        if (pointObj.DayFullName != null && pointObj.DayFullName != "")
        {
            ddlDay.SelectedValue = pointObj.DayFullName;
        }
        javascrpt = "markersLat.push(" + pointObj.PointLatitude + ");markersLong.push(" + pointObj.PointLongitude + ");";

        txtNewsLink.Text = pointObj.NewsLink;

        if (pointObj.IntroductoryMusicFile != null && pointObj.IntroductoryMusicFile != "")
        {
            lnkflIntroductory.Text = pointObj.IntroductoryMusicFile;
            lnkflIntroductory.Visible = true;
            imgBtn1.Visible = true;
        }
        if (pointObj.NewsAudio != null && pointObj.NewsAudio != "")
        {
            lnkaudioUpload.Text = pointObj.NewsAudio;
            lnkaudioUpload.Visible = true;
            imgBtn2.Visible = true;
        }
        if (pointObj.NewsVideo != null && pointObj.NewsVideo != "")
        {
            lnkvideoUpload.Text = pointObj.NewsVideo;
            lnkvideoUpload.Visible = true;
            imgBtn3.Visible = true;
        }
        if (pointObj.NewsImage != null && pointObj.NewsImage != "")
        {
            lnkimageUpload.Text = pointObj.NewsImage;
            lnkimageUpload.Visible = true;
            imgBtn4.Visible = true;
        }
        if (pointObj.ConclusionAudio != null && pointObj.ConclusionAudio != "")
        {
            lnkflConclusion.Text = pointObj.ConclusionAudio;
            lnkflConclusion.Visible = true;
            imgflConclusion.Visible = true;
        }


        if (pointObj.HighwayRuns != null)
        {
            if (ddlHighway.Items.FindByValue(Convert.ToString(pointObj.HighwayRuns)) != null)
            {
                ddlHighway.SelectedValue = pointObj.HighwayRuns;
                if (pointObj.HighwayRuns == "Northbound/Southbound")
                {
                    lblText1.Text = "Text for TTS NorthBound (▲)";
                    lblText2.Text = "Text for TTS SouthBound (▼)";


                }
                else
                {
                    lblText1.Text = "Text for TTS EastBound (►)";
                    lblText2.Text = "Text for TTS WestBound (◄)";

                    lblDirection1.Text = "Eastbound Point ►";
                    lbldirection2.Text = "Westbound Point ◄";

                }
            }
        }

        FillAngle();
        if (pointObj.Angle != null)
        {
            if (ddlAngle.Items.FindByValue(Convert.ToString(pointObj.Angle)) != null)
            {
                ddlAngle.SelectedValue = pointObj.Angle;
                imgAngle1.ImageUrl = "Images\\uptemp.png" + "?time2=" + DateTime.Now.ToString();
            }
        }

        txtPointTitle.Text = pointObj.PointTitle;

        txtNorthBoundText.Text = pointObj.NewsText;

        hdnuserid.Value = id1.ToString();
        txtSorting.Text = sorting.ToString();


        if (pointObj2 != null)
        {
            txtSouthBoundText.Text = pointObj2.SouthBoundText;
            txtAddress2.Text = pointObj2.PointAddress2;
            txtLatitude2.Text = pointObj2.PointLatitude2;
            txtLongitude2.Text = pointObj2.PointLongitude2;
            javascrpt2 = "markersLat2.push(" + pointObj2.PointLatitude2 + ");markersLong2.push(" + pointObj2.PointLongitude2 + ");";
            txtSorting2.Text = Convert.ToString(sorting + 1);
            txtPointTitle2.Text = pointObj2.PointTitle2;
            txtPromotext2.Text = pointObj2.PromoText2;
            txtNewsLink2.Text = pointObj2.NewsLink2;
            if (pointObj.Angle2 != null)
            {
                if (ddlAngle2.Items.FindByValue(Convert.ToString(pointObj.Angle2)) != null)
                {
                    ddlAngle2.SelectedValue = pointObj.Angle2;
                    imgAngle2.ImageUrl = "Images\\uptemp2.png" + "?time2=" + DateTime.Now.ToString();
                }
            }
            if (pointObj2.IntroductoryMusicFile2 != null && pointObj2.IntroductoryMusicFile2 != "")
            {
                lnkflIntroductory2.Text = pointObj2.IntroductoryMusicFile2;
                lnkflIntroductory2.Visible = true;
                imgBtn5.Visible = true;
            }
            if (pointObj2.NewsAudio2 != null && pointObj2.NewsAudio2 != "")
            {
                lnkaudioUpload2.Text = pointObj2.NewsAudio2;
                lnkaudioUpload2.Visible = true;
                imgBtn6.Visible = true;
            }
            if (pointObj2.NewsVideo2 != null && pointObj2.NewsVideo2 != "")
            {
                lnkvideoUpload2.Text = pointObj2.NewsVideo2;
                lnkvideoUpload2.Visible = true;
                imgBtn7.Visible = true;
            }
            if (pointObj2.NewsImage2 != null && pointObj2.NewsImage2 != "")
            {
                lnkimageUpload2.Text = pointObj2.NewsImage2;
                lnkimageUpload2.Visible = true;
                imgBtn8.Visible = true;
            }
            if (pointObj.ConclusionAudio2 != null && pointObj.ConclusionAudio2 != "")
            {
                lnkflConclusion2.Text = pointObj.ConclusionAudio2;
                lnkflConclusion2.Visible = true;
                imgflConclusion2.Visible = true;
            }
        }
        else
        {
            txtSorting2.Text = Convert.ToString(sorting + 1);
        }
        dvAddEdit.Visible = true;
        dvIntermediateList.Visible = false;
    }

    private void Savenewchild(int sort, int highway)
    {
     MasterUpdate objMasterUpdate = new MasterUpdate();
     objMasterUpdate.UpdateChild(sort, highway);
     }

    protected void rdbRoutePointType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillIntermediatePoint();
    }

}