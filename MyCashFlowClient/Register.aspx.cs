using System;

namespace MyCashFlowClient
{
    using CashFlowServiceRef;
    public partial class Register : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private CashRecordServiceClient InitServiceClient()
        {
            var client = new CashRecordServiceClient();
            client.ClientCredentials.UserName.UserName = "test";
            client.ClientCredentials.UserName.Password = "password";
            return client;
        }


        protected void registerBtn_Click(object sender, EventArgs e)
        {
            var client = InitServiceClient();
            var result = client.Register(name.Value, email.Value, pwd.Value);
            client.Close();
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