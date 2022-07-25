
using renato_nascimento_d3_avaliacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace renato_nascimento_d3_avaliacao.Repositories
{
    public class LoggerRepository
    {
        private const string logfile = "logs/log.txt";
        public LoggerRepository()
        {
            CreateFolderAndLogFile(logfile);
        }

        static void CreateFolderAndLogFile(string path)
        {
            string folder = path.Split("/")[0];
            if (!Directory.Exists(folder))
            {
            Console.WriteLine(folder);
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        private static string FormatLogLine(User user, string type)
        {
            string logMessage = String.Empty;
            switch (type)
            {                
                case "login":
                    logMessage = $"O usuário {user.Name} ({user.Id}) acessou o sistema às " +
                $"{DateTime.Now.ToString("HH:mm:ss")} do dia {DateTime.Now.ToString("dd/mm/yy")} \n";
                    break;
                case "new":
                    logMessage = $"Usuário {user.Name} criado com sucesso às " +
                $"{DateTime.Now.ToString("HH:mm:ss")} do dia {DateTime.Now.ToString("dd/mm/yy")} \n";
                    break;
                case "logout":
                    logMessage = $"O usuário {user.Name} ({user.Id}) deslogou do sistema às " +
                $"{DateTime.Now.ToString("HH:mm:ss")} do dia {DateTime.Now.ToString("dd/mm/yy")} \n";
                    break;
                default:
                    break;
            }
            return logMessage;
        } 

        public static void WriteInLogFile(User user, string type)
        {
            string LogLine = FormatLogLine(user, type);
            File.AppendAllText(logfile, LogLine);

        }

        public static void ReadLogsFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(logfile);
                foreach (var line in lines)
                {
                    Console.WriteLine($"{line} \n");
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
               
    }
}
