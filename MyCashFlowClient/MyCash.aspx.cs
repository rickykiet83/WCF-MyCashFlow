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

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            client.AddRecord(desc.Value, Convert.ToInt32(amount.Value), Session["UserEmail"].ToString());
            Response.Redirect("MyCash.aspx");
        }

        private void MyRecords()
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            rpt.DataSource = client.GetAllRecords(Session["UserEmail"].ToString());
            rpt.DataBind();
        }
        protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                client.DeleteRecord(id);
                Response.Redirect("MyCash.aspx");
            }
        }

    }
}