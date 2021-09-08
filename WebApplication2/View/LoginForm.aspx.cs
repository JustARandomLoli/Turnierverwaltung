using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;

namespace WebApplication2.View
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if(Nutzer.GetNutzer(Login1.UserName) != null) FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
        }
    }
}