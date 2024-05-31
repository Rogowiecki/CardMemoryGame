using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardMemoryGame
{
    /// <summary>
    /// Этот класс используется для генерации пароля
    /// </summary>
    public class GeneratePassword
    {
        /// <summary>
        /// Эта функция используется для генерации пароля
        /// </summary>
        /// <param name="length">(int) length of the password to be generated, default = 8</param>
        /// <returns>password</returns>
        public static string Generate(int length=8)
        {
            // Создаем строку символов, цифр и специальных символов, разрешенных в пароле
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            // Выбираем по одному случайному символу из строки  
            // и создаем массив символов
            char[] chars = new char[length];

            int i = 0;
            while (i < length)
            {
                chars[i] = validChars[random.Next(0, 25)];
                chars[i + 1] = validChars[random.Next(26, 51)];
                chars[i + 2] = validChars[random.Next(52, 61)];
                chars[i + 3] = validChars[random.Next(62, validChars.Length)];
                i += 4;
            }
            return new string(chars);
        }
    }
}