
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CardMemoryGame
{

    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Перенаправление, если переменная сеанса не создана
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Session["forget"] = DataAccess.CheckTempPassword(Session["UserName"].ToString());
                    if (Session["forget"].ToString() == "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                    }

                    // Отображение данных пользователя
                    MenuItem mi = mnu.Items[0];
                    mi.Value = Session["UserName"].ToString();
                    mi.Text = mi.Value;
                    Dictionary<string, string> UserProfile = DataAccess.UserProfiledetails(Session["UserName"].ToString());
                    UserName.Text = UserProfile["UserName"];
                    Email.Text = UserProfile["EmailId"];

                }
            }
            
        }

        protected void Navclick(object sender, MenuEventArgs e)
        {
            // Отключить, если перенаправлено после сброса пароля
            if (Session["forget"].ToString() == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                return;
            }

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

        private bool CheckProfileEmpty()
        {
            bool empty = false;

            // Проверяем, пусто ли имя пользователя
            if (string.IsNullOrEmpty(UserName.Text))
            {
                UsernameValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            // Проверяем, пуст ли адрес электронной почты
            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            // Проверяем, пуст ли пароль
            if (string.IsNullOrEmpty(Password.Text))
            {
                PasswordValidate.Text = "required";
                empty = true;
            }
            else
                PasswordValidate.Text = "";

            return empty;
        }

        private void ClearText()
        {
            // Очищаем все метки на странице

            OldPasswordValidate.Text = "";
            NewPasswordValidate.Text = "";
            RePasswordValidate.Text = "";
            OtpValidate.Text = "";
            UpdateLabel.Text = "";
            PasswordUpdateLabel.Text = "";
            PasswordValidate.Text = "";
            UsernameValidate.Text = "";
            EmailValidate.Text = "";

        }
        protected void GenerateOTP(object sender, EventArgs e)
        {
            // Отключить, если перенаправлено после сброса пароля
            if (Session["forget"].ToString() == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                return;
            }

            // Очистить все метки
            ClearText();

            bool empty = CheckProfileEmpty();
            if (empty == true)
                return;

            // Генерация одноразового пароля
            Session["Otp"] = GenerateOtp.Generate();
            Logging.WriteLog(Session["UserName"].ToString(), "OTP generated");

            // Отправляем код подтверждения на электронную почту пользователя
            if (ConfigurationManager.AppSettings["DisableMail"] == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('This feature is disabled. Use " + Session["Otp"].ToString() + " as OTP')", true);
                return;
            }
            bool otp_sent = Mail.SendVerificationMail(Session["Otp"].ToString(), Email.Text);
            if (otp_sent == true)
            {
                OtpValidate.Text = "OTP is sent to your email";
                Logging.WriteLog(Session["UserName"].ToString(), "OTP sent to mail");
            }
            else
                OtpValidate.Text = "Unable to send OTP. Enter valid email ID or try after some time";
        }

        protected void UpdateProfileClick(object sender, EventArgs e)
        {
            // Отключить, если перенаправлено после сброса пароля
            if (Session["forget"].ToString() == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Change your password')", true);
                return;
            }

            // Очистить все метки
            ClearText();

            // Проверка пустых входных данных
            bool empty = CheckProfileEmpty();
            if (empty == true)
                return;

            // Проверяем, сгенерирован ли код подтверждения
            if (Session["Otp"] == null)
            {
                OtpValidate.Text = "OTP not generated";
                return;
            }

            // Проверяем правильность кода подтверждения
            if (OtpBox.Text == Session["Otp"].ToString())   
            {

                // Проверяем, существует ли имя пользователя
                if (DataAccess.UsernameValidation(UserName.Text,Session["UserName"].ToString()))
                {
                    UpdateLabel.Text = "Username already exists";
                }
                else
                {
                    // Обновить профиль
                    Dictionary<string, string> UpdatedDetails = DataAccess.UpdateProfileDetails(Session["UserName"].ToString(), Password.Text, UserName.Text, Email.Text);
                    if (UpdatedDetails.Count() == 2)
                    {
                        // Обновляем переменную сеанса и отображаем обновленную информацию
                        Session["UserName"] = UpdatedDetails["UserName"];
                        MenuItem mi = mnu.Items[0];
                        mi.Value = Session["UserName"].ToString();
                        mi.Text = mi.Value;
                        UserName.Text = UpdatedDetails["UserName"];
                        Email.Text = UpdatedDetails["EmailId"];
                        UpdateLabel.Text = "Profile updated successfully";
                        Logging.WriteLog(Session["UserName"].ToString(), "Updated user profile");
                    }
                    else
                        UpdateLabel.Text = "Password is wrong";
                }
                Session["Otp"] = null;
            }
            else
            {
                OtpValidate.Text = "OTP is wrong";
            }
            
        }

        private bool CheckPasswordEmpty()
        {
            //Проверяем, пуст ли пароль
            bool empty = false;
            if (string.IsNullOrEmpty(OldPassword.Text))
            {
                OldPasswordValidate.Text = "required";
                empty = true;
            }
            else
                OldPasswordValidate.Text = "";

            // Проверяем, пуст ли новый пароль
            if (string.IsNullOrEmpty(NewPassword.Text))
            {
                NewPasswordValidate.Text = "required";
                empty = true;
            }
            else
                NewPasswordValidate.Text = "";

            // Проверяем, пуст ли пароль для повторного ввода
            if (string.IsNullOrEmpty(ReNewPassword.Text))
            {
                RePasswordValidate.Text = "required";
                empty = true;
            }
            else
                RePasswordValidate.Text = "";

            return empty;
        }
        protected void UpdatePassword_Click(object sender, EventArgs e)
        {

            ClearText();

            bool empty = CheckPasswordEmpty();
            if (empty == true)
                return;

            // Проверка входных данных
            if (OldPassword.Text == NewPassword.Text)
                PasswordUpdateLabel.Text = "Current password and new password should not be same";
            else if (NewPassword.Text != ReNewPassword.Text)
                PasswordUpdateLabel.Text = "New password and retry new password should be same";
            else
            {
                // Обновляем новый пароль
                if (DataAccess.UpdateProfilePassword(Session["UserName"].ToString(), OldPassword.Text, NewPassword.Text))
                {
                    PasswordUpdateLabel.Text = "password updated successfully";
                    Logging.WriteLog(Session["UserName"].ToString(), "Updated user password");
                    Session["forget"] = "false";
                }
                else
                    PasswordUpdateLabel.Text = "Current password is wrong";
            }
        }
    }
}