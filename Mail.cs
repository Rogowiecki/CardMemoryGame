using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace CardMemoryGame
{
    /// <summary>
    /// Этот класс используется для отправки OTP и временного пароля на электронную почту пользователя
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Эта функция используется для отправки OTP на электронную почту пользователя
        /// </summary>
        /// <param name="VerificationCode">(String) OTP</param>
        /// <param name="MailId">(String) E-mail id of current user</param>
        /// <returns>True if mail is sent, else false</returns>
        public static bool SendVerificationMail(string VerificationCode, string MailId)
        {
            String Message = "Verification code: " + VerificationCode;
            String Subject = "E-Mail Verification";
            return SendMail(MailId, Subject, Message);
        }

        /// <summary>
        /// Эта функция используется для отправки временного пароля для сброса пароля.
        /// </summary>
        /// <param name="NewPassword">(String) temporary password</param>
        /// <param name="MailId">(String) E-mail id of current user</param>
        /// <returns>True if mail is sent, else false</returns>
        public static bool SendNewPasswordMail(string NewPassword, string MailId)
        {
            String Message = "One time password: " + NewPassword;
            String Subject = "Reset Password";
            return SendMail(MailId, Subject, Message);
        }

        /// <summary>
        /// Эта функция используется для отправки электронного письма на почтовый идентификатор пользователя.
        /// </summary>
        /// <param name="ToMailId">(String) E-mail id of current user</param>
        /// <param name="subject">(String) Subject of the E-mail</param>
        /// <param name="Message">(String) Message of the E-mail</param>
        /// <returns>True if mail is sent, else false</returns>
        private static bool SendMail(string ToMailId, string subject, string Message)
        {
            try
            {

                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromMailId"], ConfigurationManager.AppSettings["FromName"]);
                var toAddress = new MailAddress(ToMailId);
                string fromPassword = ConfigurationManager.AppSettings["FromPassword"];
                string body = "This is system generated mail. Do not reply to this mail\n\n" +
                    Message + "\n\nNote: This mail contains sensitive information. " +
                    "If you are not the intended receiver of this mail, delete this mail."; 

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch(Exception ex)
            {
                Logging.WriteLog("Mail Error", "Error in sending mail : " + ex.Message);
                return false;
            }
        }
    }
}