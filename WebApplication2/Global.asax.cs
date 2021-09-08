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
        public static Controller Controller { get => _Controller; }

        protected static PersonenTypListe _personenTypListe;
        public static PersonenTypListe personenTypListe { get => _personenTypListe; }

        protected void Application_Start(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Properties.Settings.Default.CWD);
            _Controller = new Controller();
            _personenTypListe = new PersonenTypListe();
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