using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Перенаправление, если переменная сеанса не создана
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Проверяем, является ли пароль временным
            else if (DataAccess.CheckTempPassword(Session["UserName"].ToString()) == "true")
            {
                Response.Redirect("MyProfile.aspx");
            }
            
            else
            {
                MenuItem mi = mnu.Items[0];
                mi.Value = Session["UserName"].ToString();
                mi.Text = mi.Value;

                // Параметры отображения для гостя
                if (mi.Text == "Guest")
                {
                    Guest.Visible = true;
                    mnu.Visible = false;
                }

                // Параметры отображения для вошедшего в систему пользователя
                else
                {
                    Guest.Visible = false;
                    mnu.Visible = true;
                }
                    
            }
        }

        protected void Btn_Start_Click(object sender, EventArgs e)
        {
            // Перенаправляем страницу игры с выбранной опцией в URL
            var selector = RbSeclector.SelectedItem.Value;
            Logging.WriteLog(Session["UserName"].ToString(), "Game started with " + Convert.ToInt32(selector) * 4 + " cards");
            Response.Redirect(String.Format("GamePage.aspx?cards={0}", selector));
        }

        protected void Navclick(object sender, MenuEventArgs e)
        {
            if (e.Item.Text == "logout" || e.Item.Text == "Login")
            {
                Logging.WriteLog(Session["UserName"].ToString(), "User logged out");

                // Очистить переменные сеанса и выйти из системы
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }

            else if (e.Item.Text == "My Profile")
            {
                // Перенаправление на страницу профиля
                Response.Redirect("MyProfile.aspx");
            }

            else if (e.Item.Text == "View scores")
            {
                // Перенаправление на страницу результатов
                Response.Redirect("ViewScores.aspx");
            }

            else if (e.Item.Text == "Game Page")
            {
                // Перенаправление на страницу игры с возможностью выбора карт
                Response.Redirect("Default.aspx");
            }
        }

    }
}