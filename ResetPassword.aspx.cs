using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CardMemoryGame
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateNewPassword(object sender, EventArgs e)
        {
            // Проверяем, действительны ли имя пользователя и данные
            if (DataAccess.UserValidation(Username.Text, Email.Text))
            {

                // Создаем случайный пароль
                string NewPassword = GeneratePassword.Generate(8);

                if (ConfigurationManager.AppSettings["DisableMail"] == "false")
                {
                    
                    // Send the password to user e-mail id and store the password
                    bool sent = Mail.SendNewPasswordMail(NewPassword, Email.Text);
                    if (sent == true)
                    {
                        DataAccess.StoreTempPassword(Username.Text, NewPassword);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                        "alert ('Password is sent to your e-mail. Use the new password to login'); window.location = 'Login.aspx';", true);
                        Logging.WriteLog(Username.Text, "Temporary password created");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                       "alert ('Cannot send e-mail. Check your mail ID or try after sometime'); window.location = 'Login.aspx';", true);
                    }
                }

                else
                {
                    DataAccess.StoreTempPassword(Username.Text, NewPassword);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                        "alert ('This feature is disabled. Use " + NewPassword + " as new password'); window.location = 'Login.aspx';", true);
                    Logging.WriteLog(Username.Text, "Temporary password created");
                }

            }
            else
            {
                InvalidLabel.Text = "Неверное имя пользователя или идентификатор электронной почты.";
            }
        }
    }
}