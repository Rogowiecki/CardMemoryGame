using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string PageName = Page.AppRelativeVirtualPath;
            if (PageName == "~/GamePage.aspx" || PageName == "~/MyProfile.aspx")
            {
                footer.Style["position"] = "relative";
                footer.Style["bottom"] = "0";
            }
            else
            {
                footer.Style["position"] = "absolute";
            }
        }
    }
}