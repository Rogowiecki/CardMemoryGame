using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class GamePage : System.Web.UI.Page
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
        protected void Victory_RedirectToHome(object sender, EventArgs e)
        {
            // Обновляем оценки только для вошедшего в систему пользователя
            if (Session["UserName"].ToString() != "Guest")
            {
                UpdateScoreDetails(UserScoreHidden.Value);
            }

            // Регистрируемся и перенаправляем на главную страницу
            Logging.WriteLog(Session["UserName"].ToString(), "Game won, Score: "+ UserScoreHidden.Value);
            Response.Redirect(String.Format("Default.aspx"));
        }

        
        protected void GameOver_RedirectToHome(object sender, EventArgs e)
        {
            // Журнал и перенаправление после проигрыша игры
            Logging.WriteLog(Session["UserName"].ToString(), "Game lost");
            Response.Redirect(String.Format("Default.aspx"));
        }


        public void UpdateScoreDetails(string UserScore)
        {
            // Получаем лучшие баллы текущего пользователя
            List<int> UserTopScore = DataAccess.GetUserTopScores(Session["UserName"].ToString());

            // Добавляем текущий балл к существующим баллам
            int HighScore = 0;
            string UserScores = null;
            UserTopScore.Add(Convert.ToInt32(UserScore));
            UserTopScore.Sort();
            UserTopScore.Reverse();
            UserTopScore = UserTopScore.Take(10).ToList();
            
            UserScores = string.Join(",", UserTopScore);
            HighScore = Convert.ToInt32(UserTopScore[0]);

            // Обновляем счет в базу данных
            DataAccess.UpdateUserScores(Session["UserName"].ToString(), UserScores, HighScore);
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