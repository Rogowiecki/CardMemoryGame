using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Threading;

namespace CardMemoryGame
{
    public class Logging
    {
        /// <param name="UserName">(String) User name of current user</param>
        /// <param name="Message">(String) The message to be logged</param>
        public static void WriteLog(string UserName, string Message)
        {
            var Retries = Convert.ToInt32(ConfigurationManager.AppSettings["LogRetryCount"]);
            while (Retries > 0)
            {
                try
                {
                    // Получаем путь к файлу журнала
                    var Path = ConfigurationManager.AppSettings["LogPath"];

                    // Создаем файл, если он не существует
                    var FileName = "Log_" + System.DateTime.Now.ToString("dd_MMM_yyyy") + ".txt";
                    if (!Directory.Exists(Path))
                        Directory.CreateDirectory(Path);

                    // Записываем лог в файл
                    StreamWriter Writer = new StreamWriter(Path + FileName, true);
                    Writer.WriteLine(DateTime.Now + " [" + UserName + "] : " + Message);
                    
                    Writer.Close();
                    Writer.Dispose();
                    Retries = 0;
                }
                catch
                {
                    Thread.Sleep(500);
                    Retries--;
                }
            }
        }
    }
}