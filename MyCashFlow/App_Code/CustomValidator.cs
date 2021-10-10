using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace MyCashFlow.App_Code
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException("Username and Password required!");
            }
            if (userName != "test" && password != "password")
            {
                throw new FaultException("Username and Password incorrect!");
            }
        }
    }
}