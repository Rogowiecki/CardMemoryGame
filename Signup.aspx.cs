using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CardMemoryGame
{
    public partial class Signup : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateOTP(object sender, EventArgs e)
        {
            // Проверяем, пусты ли имя пользователя и адрес электронной почты
            bool empty = false;
            if (string.IsNullOrEmpty(UserName.Text))
            {
                UsernameValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailValidate.Text = "required";
                empty = true;
            }
            else
                UsernameValidate.Text = "";

            if (empty == true)
                return;

            SignUpValidate.Text = "";
            OtpInvalidLabel.Text = "";

            // Генерация кода подтверждения
            var Otp = GenerateOtp.Generate();
            Logging.WriteLog("New user", "OTP generated");
            Session["Otp"] = Otp;

            // Отправляем код подтверждения на электронную почту пользователя
            if (ConfigurationManager.AppSettings["DisableMail"] == "true")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('This feature is disabled. Use " + Otp + " as OTP')", true);
                return;
            }
            bool otp_sent = Mail.SendVerificationMail(Otp, Email.Text);
            if (otp_sent == true)
            {
                OtpInvalidLabel.Text = "OTP is sent to your email";
                Logging.WriteLog("New user", "OTP sent to mail");
            }
            else
                OtpInvalidLabel.Text = "Unable to send OTP. Enter valid email ID or try after some time";
        }

        protected void SignupButton(object sender, EventArgs e)
        {
            // Проверяем, сгенерирован ли код подтверждения
            if (Session["Otp"] == null)
            {
                OtpInvalidLabel.Text = "Код не сгенерирован";
                return;
            }

            // Проверяем правильность кода подтверждения
            if (OTPbox.Text == Session["Otp"].ToString())
            {
                // Проверяем, пуст ли пароль
                if (string.IsNullOrEmpty(Password.Text))
                {
                    PasswordValidate.Text = "Required";
                    RePasswordValidate.Text = "Required";
                    OtpInvalidLabel.Text = "Click again to Generate OTP";
                    Session["Otp"] = null;
                    return;
                }

                // Проверяем, существует ли имя пользователя
                if (DataAccess.Signup(UserName.Text, Password.Text, Email.Text))
                {
                    // Журнал и перенаправление на страницу входа
                    Logging.WriteLog(UserName.Text, "New user signed up");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", "alert ('Sign up successful'); window.location = 'Login.aspx';", true);

                }
                else 
                {
                    SignUpValidate.Text = "Username already exists";
                    OtpInvalidLabel.Text = "";
                    PasswordValidate.Text = "";
                    RePasswordValidate.Text = "";
                }
                Session["Otp"] = null;
            }
            else
            {
                SignUpValidate.Text = "Wrong OTP ";
                PasswordValidate.Text = "";
                RePasswordValidate.Text = "";
                PasswordValidate.Text = "";
                RePasswordValidate.Text = "";
            }
        }
    }
}