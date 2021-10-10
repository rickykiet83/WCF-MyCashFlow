using System;
using System.Web.UI.WebControls;

namespace MyCashFlowClient
{
    using CashFlowServiceRef;

    public partial class MyCash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyRecords();
        }

        private CashRecordServiceClient InitServiceClient()
        {
            var client = new CashRecordServiceClient();
            client.ClientCredentials.UserName.UserName = "test";
            client.ClientCredentials.UserName.Password = "password";
            return client;
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            var client = InitServiceClient();
            client.AddRecord(desc.Value, Convert.ToInt32(amount.Value), Session["UserEmail"].ToString());
            client.Close();
            Response.Redirect("MyCash.aspx");
        }

        private void MyRecords()
        {
            var client = InitServiceClient();
            rpt.DataSource = client.GetAllRecords(Session["UserEmail"].ToString());
            rpt.DataBind();
            client.Close();
        }
        protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            if (e.CommandName == "Delete")
            {
                var client = InitServiceClient();
                int id = Convert.ToInt32(e.CommandArgument);
                client.DeleteRecord(id);
                client.Close();
                Response.Redirect("MyCash.aspx");
            }
        }

    }
}