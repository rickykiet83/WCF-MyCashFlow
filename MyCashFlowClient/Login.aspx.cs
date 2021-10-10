using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCashFlowClient
{
    using CashFlowServiceRef;
    public partial class Login : System.Web.UI.Page
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

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            var client = InitServiceClient();
            var result = client.Login(email.Value, pwd.Value);
            client.Close();
            if (result == "Success")
            {
                Session["UserEmail"] = email.Value;
                Response.Redirect("~/MyCash.aspx");
            }
            else
            {
                errorLbl.Text = "Wrong Login Creditials";
            }
        }
    }
}