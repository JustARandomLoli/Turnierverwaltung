using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;

namespace WebApplication2
{
    public class Global : System.Web.HttpApplication
    {

        protected static Controller _Controller;
        public static Controller Controller { get => _Controller; set => _Controller = value; }

        protected void Application_Start(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Properties.Settings.Default.CWD);
            Controller = new Controller();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}