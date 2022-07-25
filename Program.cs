using renato_nascimento_d3_avaliacao.Models;
using renato_nascimento_d3_avaliacao.Repositories;
using renato_nascimento_d3_avaliacao.Views;

namespace renato_nascimento_d3_avaliacao
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            string option;

            do
            {
                Menu.MainMenu();
                
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Menu.CreateUserMenu();
                        break;
                      
                    case "2":
                        Menu.LoginMenu();
                        break;
                    case "3":
                        Menu.MenuReadLogs();
                        break;
                    default:
                        break;
                }

            } while (option !="4");
        }
    }
}