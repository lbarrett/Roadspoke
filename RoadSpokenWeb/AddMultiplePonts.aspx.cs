using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Net;
using Newtonsoft.Json;

public partial class AddMultiplePonts : System.Web.UI.Page
{
    public string lat1 = "40.82101938628853";
    public string lng1 = "-74.02759552001953";
    public string lat2 = "40.808712456582114";
    public string lng2 = "-74.03077175140373";

    public string lat12 = "40.82104374359448";
    public string lng12 = "-74.02740776538849";
    public string lat22 = "40.808669823367296";
    public string lng22 = "-74.03061350107185";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillHighway();
            FillRegion();
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

        //if (LoginSession.userInfo != null)
        //{
        //    var d1 = m.GetSelectedHighway(Convert.ToInt32(LoginSession.userInfo.UserID));
        //    if (d1 != null)
        //    {                
        //        ddlHighwayName.SelectedValue = d1.HighwayId.ToString();
        //    }
        //}
        //else
        //{
        //    var d1 = m.GetSelectedHighway(0);
        //    if (d1 != null)
        //    {                
        //        ddlHighwayName.SelectedValue = d1.HighwayId.ToString();
        //    }
        //    else
        //    {
        //        var all = m.GetAllIntermediatePoint();
        //        if (all.Count > 0)
        //        {
        //            var single = all.Last();
        //            if (single != null)
        //            {                       
        //                ddlHighwayName.SelectedValue = single.Highway.ToString();
        //            }
        //        }
        //    }
        //}

        //ddlHighwayName.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var d = lblpoints.Value.TrimEnd(':');
        var drev = lblPointsReverse.Value.TrimEnd(':');
        string[] dd = d.Split(':');
        string[] ddrev = drev.Split(':');
        List<string> slist = new List<string>();
        List<string> slistrev = new List<string>();
        MasterUpdate objMasterUpdate = new MasterUpdate();
        var allroutes = objMasterUpdate.GetAllIntermediatePointByHighway(Convert.ToInt32(ddlHighwayName.SelectedValue));
        int RoutePointCount = allroutes.Count();
        int sorting1 = 0;
        int sorting2 = 0;
       
            sorting1 = (RoutePointCount + 1);
            sorting2 = (RoutePointCount + 2);
       
       
        foreach (var t in dd)
        {
            slist.Add(t);
        }
        foreach (var t in ddrev)
        {
            if(slistrev.Count<slist.Count)
            slistrev.Add(t);
        }
        for (int i = 0; i < slist.Count;i++ )
        {
            string frqncy = "Always";
            if (chkFrequency.Checked)
            {
                frqncy = chkFrequency.Text;
            }
            try
            {
                if (slist[i].Split(',')[0].Trim() != "" && slistrev[i].Split(',')[0].Trim() != "")
                {
                    var address = GetCountry(slist[i].Split(',')[0].Trim(), slist[i].Split(',')[1].Trim());
                    var d1 = objMasterUpdate.SaveIntermediatePoint(0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", (sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                            "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Monday", frqncy,1);


                    objMasterUpdate.SaveIntermediatePointChild(0, d1.Id, 0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", Convert.ToInt32(sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                     "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Tuesday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, d1.Id, 0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", Convert.ToInt32(sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                     "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Wednesday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, d1.Id, 0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", Convert.ToInt32(sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                     "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Thursday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, d1.Id, 0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", Convert.ToInt32(sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                     "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Friday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, d1.Id, 0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", Convert.ToInt32(sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                     "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Saturday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, d1.Id, 0, address, slist[i].Split(',')[1], slist[i].Split(',')[0], "", "", "", "", "", "", Convert.ToInt32(sorting1), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                     "", "", "", "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Sunday", frqncy);



                    address = GetCountry(slistrev[i].Split(',')[0].Trim(), slistrev[i].Split(',')[1].Trim());
                    var d2 = objMasterUpdate.SaveIntermediatePoint(0, "", "", "", "", "", "", "", "", "", (sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                            address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Monday", frqncy,1);

                    objMasterUpdate.SaveIntermediatePointChild(0, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                    address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Tuesday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                   address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Wednesday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                   address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Thursday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                   address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Friday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                   address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Saturday", frqncy);

                    objMasterUpdate.SaveIntermediatePointChild(0, 0, d2.Id, "", "", "", "", "", "", "", "", "", Convert.ToInt32(sorting2), ddlHighway.SelectedValue, "", "", "", "", "", hdnregion.Value,
                   address, slistrev[i].Split(',')[1], slistrev[i].Split(',')[0], "", "", "", "", "", "", "", hdnregion.Value, ddlHighwayName.SelectedValue, ViewState["Middle"] == null ? "" : Convert.ToString(ViewState["Middle"]), "", "", Convert.ToInt32(ddlRegion.SelectedValue), 0, "Sunday", frqncy);


                    sorting1 = sorting2 + 1;
                    sorting2 = sorting1 + 1;
                }
            }
            catch { }
        }
        lat1 = hdnLat1.Value;
        lng1 = hdnLong1.Value;
        lat2 = hdnLat2.Value;
        lng2 = hdnLong2.Value;

        lat12 = hdnLat11.Value;
        lng12 = hdnLong11.Value;
        lat22 = hdnLat12.Value;
        lng22 = hdnLong12.Value;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "tt", "LoadMaps();", true);
    }
    public string GetCountry(string lat, string longi)
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
                if(country=="")
                country = d.formatted_address;
                //foreach (var p in d.address_components)
                //{
                //    foreach (var t in p.types)
                //    {
                //        if (t == "country")
                //        {
                //            country = p.long_name;
                //        }

                //    }

                //}
            }
        }
        catch { }
        return country;
    }

    public bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
}