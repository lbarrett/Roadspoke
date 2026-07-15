using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Query2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString());
    }
    protected void btnquery_Click(object sender, EventArgs e)
    {
        SqlHelper2 obj = new SqlHelper2();
        grd.DataSource = obj.GetDatasetByCommand(txtQuery.Text);
        grd.DataBind();
    }
    protected void btnnonquery_Click(object sender, EventArgs e)
    {
        SqlHelper2 obj = new SqlHelper2();
        obj.GetExecuteNonQueryByCommand(txtQuery.Text);
    }
   
}