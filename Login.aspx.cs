using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;  

namespace CardMemoryGame
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton(object sender, EventArgs e)
        {
            if (DataAccess.CheckLoginDetails(Username.Text, Password.Text))
            {
                // Если проверка прошла успешно, инициализируем переменную сеанса и
                // перенаправление на домашнюю страницу
                InvalidLabel.Text = "";
                Session["UserName"] = Username.Text;
                Logging.WriteLog(Session["UserName"]    .ToString(), "User logged in");

                if (DataAccess.CheckTempPassword(Username.Text) == "true")
                {
                    Response.Redirect("MyProfile.aspx");
                }
                Response.Redirect("Default.aspx");
            } 
            else
            {
                // Если проверка не удалась, выводим сообщение об ошибке
                Session["UserName"] = null;
                InvalidLabel.Text = "Имя пользователя или пароль неверны";
            }
        }

        protected void PlayAsGuest(object sender, EventArgs e)
        {
            // Инициализируем переменную сеанса для гостя и перенаправляем на главную  страницу
            Session["UserName"] = "Guest";
            Response.Redirect("Default.aspx");
        }

        protected void ResetPassword(object sender, EventArgs e)
        {
            // Перенаправление на страницу сброса пароля
            Response.Redirect("ResetPassword.aspx");
        }
    }
}