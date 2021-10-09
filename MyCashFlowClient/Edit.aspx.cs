using System;

namespace MyCashFlowClient
{
    using CashFlowServiceRef;
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UpdateRecord_Click(object sender, EventArgs e)
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            client.UpdateRecord(Convert.ToInt32(Request.QueryString["id"].ToString()), desc.Value, Convert.ToInt32(amount.Value));
            Response.Redirect("~/MyCash.aspx");
        }
    }
}