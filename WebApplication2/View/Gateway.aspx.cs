using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebApplication2.View
{
    public partial class View2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void Spielerverwaltung_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Spielerverwaltung.aspx");
        }

        protected void Mannschaftsverwaltung_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Mannschaftsverwaltung.aspx");
        }

        protected void Turnierverwaltung_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Turnierverwaltung.aspx");
        }

        protected void Ranking_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Ranking.aspx");
        }
    }
}