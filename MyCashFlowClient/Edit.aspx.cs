using System;

namespace MyCashFlowClient
{
    using CashFlowServiceRef;
    public partial class Edit : System.Web.UI.Page
    {
        private CashRecordServiceClient client;
        public Edit()
        {
            InitServiceClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void InitServiceClient()
        {
            if (client != null) return;

            client = new CashRecordServiceClient();
            client.ClientCredentials.UserName.UserName = "test";
            client.ClientCredentials.UserName.Password = "password";
        }

        protected void UpdateRecord_Click(object sender, EventArgs e)
        {
            client.Open();
            client.UpdateRecord(Convert.ToInt32(Request.QueryString["id"].ToString()), desc.Value, Convert.ToInt32(amount.Value));
            client.Close();
            Response.Redirect("~/MyCash.aspx");
        }
    }
}