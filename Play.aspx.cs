//------------------------------------------------------------------------------
// This is the backend code for the page Play.aspx
// This file is used to start the game
//
// File name     : Play.aspx.cs
// Project name  : Card Memory Game 
// Written by    : Goutham
// Language      : C#
// Date modified : 23-09-2020
// Dependencies  : .NET framework 4.7.2
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class Play : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Start_Click(object sender, EventArgs e)
        {
            //var selector = 3;
            var selector = RbSeclector.SelectedItem.Value;
            var UserName = "Guest";
            Logging.WriteLog(UserName, "Game started with " + Convert.ToInt32(selector) * 4 + " cards");
            Response.Redirect(String.Format("GamePage.aspx?cards={0}", selector));
        }
    }
}