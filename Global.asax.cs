using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;


namespace CardMemoryGame
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Logging.WriteLog("Application Log", "Application started");
            Logging.WriteLog("Application Log", "E-mail disabled : " + ConfigurationManager.AppSettings["DisableMail"]);
            if(ConfigurationManager.AppSettings["DisableMail"] == "false")
            {
                Logging.WriteLog("Application Log", "E-mails will be sent from '" + ConfigurationManager.AppSettings["FromMailId"] + "'");
            }
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