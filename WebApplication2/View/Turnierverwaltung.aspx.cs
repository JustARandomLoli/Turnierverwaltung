using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication2.View
{
    public partial class Turnierverwaltung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labb.Text = Directory.GetCurrentDirectory();
        }
    }
}