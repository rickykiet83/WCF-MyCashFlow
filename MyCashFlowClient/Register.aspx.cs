using System;

namespace MyCashFlowClient
{
    using CashFlowServiceRef;
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            var result = client.Register(name.Value, email.Value, pwd.Value);
            if (result == "Success")
            {
                resLbl.Text = "Registration succeed.";
            }
            else
            {
                resLbl.Text = result;
            }
        }
    }
}