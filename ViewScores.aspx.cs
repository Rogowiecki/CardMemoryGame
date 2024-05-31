using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardMemoryGame
{
    public partial class ViewScores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Перенаправление, если переменная сеанса не инициализирована
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

                // Получить 10 лучших результатов из базы данных
                Dictionary<string, string> TopScores = DataAccess.GetTopScores();

                tbl.Controls.Clear();
                tbl.HorizontalAlign = HorizontalAlign.Center;
                tbl.CellPadding = 10;
                
                int rows = TopScores.Count;
                int cols = 2;

                // Создаем таблицу и отображаем результаты
                for (int i = 0; i < rows; i++)
                {
                    TableRow rowNew = new TableRow();
                    for (int j = 0; j < cols; j++)
                    {
                        TableCell cellNew = new TableCell();
                        cellNew.BorderWidth = Unit.Pixel(0);
                        cellNew.Width = 100;
                        cellNew.HorizontalAlign = HorizontalAlign.Center;
                        if (TopScores.ElementAt(i).Key.ToString() == Session["UserName"].ToString())
                            cellNew.ForeColor = System.Drawing.Color.DeepSkyBlue;
                        else
                            cellNew.ForeColor = System.Drawing.Color.White;
                        cellNew.BorderColor = System.Drawing.Color.White;
                        Label lblNew = new Label();

                        // Добавляем имя пользователя в таблицу
                        if (j == 0)
                            lblNew.Text = TopScores.ElementAt(i).Key.ToString();

                        // Добавляем оценку в таблицу
                        else if (j == 1)
                            lblNew.Text = TopScores.ElementAt(i).Value.ToString();

                        cellNew.Controls.Add(lblNew);
                        rowNew.Controls.Add(cellNew);
                    }
                    tbl.Controls.Add(rowNew);
                }


                // Получить все оценки текущего пользователя из базы данных
                List<int> UserTopScores = DataAccess.GetUserTopScores(Session["UserName"].ToString());

                tb2.Controls.Clear();
                tb2.HorizontalAlign = HorizontalAlign.Center;
                tb2.CellPadding = 10;

                int rows_CurrUser = UserTopScores.Count;

                // Создаем таблицу и отображаем результаты
                for (int i = 0; i < rows_CurrUser; i++)
                {
                    TableRow rowNew = new TableRow();
                    
                    TableCell cellNew = new TableCell();
                    cellNew.BorderWidth = Unit.Pixel(0);
                    cellNew.Width = 100;
                    cellNew.HorizontalAlign = HorizontalAlign.Center;
                    cellNew.ForeColor = System.Drawing.Color.White;
                    cellNew.BorderColor = System.Drawing.Color.White;
                    Label lblNew = new Label();

                    // Добавляем счет для отображения
                    lblNew.Text = UserTopScores[i].ToString();

                    cellNew.Controls.Add(lblNew);
                    rowNew.Controls.Add(cellNew);
                    
                    tb2.Controls.Add(rowNew);
                }
                if(!IsPostBack)
                    Logging.WriteLog(Session["UserName"].ToString(), "Score page loaded");
            }
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