using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace RoadSpokenService
{
    public partial class ServiceTest : System.Web.UI.Page
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalMilliseconds;
        }



        protected void Button2_Click(object sender, EventArgs e)
        {

            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {    //---Live---//
                string serurl = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];

                //string url = "http://localhost:44374/DarknessWCF.svc/" + ddlMethodName.SelectedValue.ToString();
                string url = serurl + ddlMethodName.SelectedValue.ToString();
                //string url = "http://localhost:25657/WeareafricaWebService.svc/" + ddlMethodName.SelectedValue.ToString();

                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "text/" + rbtlist.SelectedValue.ToString() + "; charset=utf-8";
                req.Timeout = 300000;

                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                writer.WriteLine(txtpostdata.Text);
                writer.Close();

                res = (HttpWebResponse)req.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(res.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }

                Repeater r = new Repeater();
                foreach (RepeaterItem ri in r.Items)
                {
                    if (((CheckBox)ri.FindControl("chkSelect")).Checked)
                    { }
                }

                //return only the xml representing the response details (inner request)
                Response.Write(result);
                //Response.Write(soapResonseXMLDocument.InnerXml);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}