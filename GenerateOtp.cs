using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardMemoryGame
{
    /// <summary>
    /// Этот класс используется для генерации кода подтверждения для проверки
    /// </summary>
    public class GenerateOtp
    {
        /// <summary>
        /// Эта функция используется для генерации кода подтверждения
        /// </summary>
        /// <returns>OTP</returns>
        public static string Generate()
        {
            // Генерируем случайный код подтверждения
            Random r = new Random();
            var num = r.Next(0, 1000000);
            string otp = num.ToString("000000");

            return otp;
        }
    }
}