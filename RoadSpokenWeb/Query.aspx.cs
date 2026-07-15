using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BAL;
public partial class Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnquery_Click(object sender, EventArgs e)
    {

        SqlHelper obj = new SqlHelper();
        grd.DataSource = obj.GetDatasetByCommand(txtQuery.Text);
        grd.DataBind();
    }
    protected void btnnonquery_Click(object sender, EventArgs e)
    {
        SqlHelper obj = new SqlHelper();
        obj.GetExecuteNonQueryByCommand(txtQuery.Text);
    }
    protected void btnnonquery2_Click(object sender, EventArgs e)
    {
        MasterUpdate objMasterUpdate = new MasterUpdate();

        var dd = objMasterUpdate.GetAllIntermediatePoint().ToList().Where(x => x.Highway == 49).ToList();
        foreach (var t in dd)
        {
            if (objMasterUpdate.IsOdd(Convert.ToInt32( t.Sorting)))
            {
                objMasterUpdate.SaveIntermediatePointChild(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.PromoText2, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                     t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Tuesday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.PromoText2, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Wednesday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.PromoText2, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Thursday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.PromoText2, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Friday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.PromoText2, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Saturday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.PromoText2, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Sunday", t.Frequency);


                //objMasterUpdate.SaveIntermediatePointChildShort(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude,
                //     t.PointAddress2, t.PointLongitude2, t.PointLatitude2,  "Tuesday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2,  "Wednesday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2,  "Thursday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2,   "Friday" );

                //objMasterUpdate.SaveIntermediatePointChildShort(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2,  "Saturday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, t.Id, 0, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2,  "Sunday");


            }
            else
            {

                objMasterUpdate.SaveIntermediatePointChild(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.SouthBoundText, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                      t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText2, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Tuesday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.SouthBoundText, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText2, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Wednesday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.SouthBoundText, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText2, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Thursday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.SouthBoundText, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText2, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Friday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.SouthBoundText, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText2, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Saturday", t.Frequency);

                objMasterUpdate.SaveIntermediatePointChild(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude, t.NewsLink, t.NewsText, t.NewsImage, t.NewsVideo, t.NewsAudio, t.PointTitle, (int)t.Sorting, t.HighwayRuns, t.SouthBoundText, t.IntroductoryMusicFile, "", "", t.PromoText, t.Region,
                 t.PointAddress2, t.PointLongitude2, t.PointLatitude2, t.NewsLink2, t.NewsImage2, t.NewsVideo2, t.NewsAudio2, t.PointTitle2, t.IntroductoryMusicFile, t.PromoText2, t.Region2, t.Highway.ToString(), "", t.ConclusionAudio, t.ConclusionAudio2, (int)t.RegionId, 0, "Sunday", t.Frequency);


                //objMasterUpdate.SaveIntermediatePointChildShort(0,0, t.Id,  t.PointAddress, t.PointLongitude, t.PointLatitude,
                //    t.PointAddress2, t.PointLongitude2, t.PointLatitude2, "Tuesday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2, "Wednesday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2, "Thursday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2, "Friday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2, "Saturday");

                //objMasterUpdate.SaveIntermediatePointChildShort(0, 0, t.Id, t.PointAddress, t.PointLongitude, t.PointLatitude,
                // t.PointAddress2, t.PointLongitude2, t.PointLatitude2, "Sunday");
            }
        }
    }
   
}