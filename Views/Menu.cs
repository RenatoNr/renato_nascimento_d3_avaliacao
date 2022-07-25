using renato_nascimento_d3_avaliacao.Models;
using renato_nascimento_d3_avaliacao.Repositories;


namespace renato_nascimento_d3_avaliacao.Views
{
    public class Menu
    {
        
        public static void MainMenu()
        {
            Console.WriteLine("\n***  Avaliação D3 - Renato Nascimento Reis Santos *** \n");
            Console.WriteLine("\nEscolha uma das opções abaixo:\n");
            Console.WriteLine("1 - Cadastrar usuário");
            Console.WriteLine("2 - Logar no sistema");
            Console.WriteLine("3 - Exibir logs do sistema");
            Console.WriteLine("4 - Sair");
        }

        public static void CreateUserMenu()
        {
            Console.Clear();

            User user = new User();
            Console.WriteLine("**** Cadastrar usuário *****\n");

            Console.WriteLine("\nDigite o nome do usuário:\n");
            var name = Console.ReadLine();

            Console.WriteLine("\nDigite o email do usuário:\n");
            var email = Console.ReadLine();

            Console.WriteLine("\nDigite uma senha:\n");
            var password = PasswordMask();

            user.Name = name;
            user.Email = email;
            user.Password = password;

            Console.WriteLine($"Confirmar a criação do usuário {name}? (s/n) \n");
            var option = Console.ReadLine();

            if (option !="s")
            {
                Console.Clear();
                return;
            }

            UserRepository newUser = new();
            
            var response = newUser.CreateUser(user);
            Console.Clear();
            LoggerRepository.WriteInLogFile(user, "new");
            Console.WriteLine(response);
        }

        public static void LoginMenu()
        {
            UserRepository user = new();
            Console.Clear();
            Console.WriteLine("****** Efetuar Login no sistema ******/n");

            Console.WriteLine("\nDigite o email do usuário:\n");
            var email = Console.ReadLine();

            Console.WriteLine("\nDigite a senha:\n");
            var password = PasswordMask();

            var response = user.Login(email, password);

            if (response.Name is not null)
            {
                LoggedMenu(response);
            }
            else
            {
                Console.WriteLine("Não foi possível efetuar o login. Verifique seu email ou senha.");
            }
           

        }

        public static void LoggedMenu(User user)
        {
            Console.Clear();
            Console.WriteLine($"****  Bem vindo {user.Name.ToUpper()} *****\n");
            Console.WriteLine($"Você está logado no sistema.");
            LoggerRepository.WriteInLogFile(user, "login");
            string option;

            Console.WriteLine("Digite 1 para deslogar");
            Console.WriteLine("Digite 2 para encerrar");
            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    LoggerRepository.WriteInLogFile(user, "logout");
                    Console.Clear();
                    break;
                    case "2":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

        }

        public static void MenuReadLogs()
        {
            Console.Clear();
            Console.WriteLine("******** Logs do sistema ******** \n");
            
            LoggerRepository.ReadLogsFile();
        }

        private static string PasswordMask()
        {
            ConsoleKeyInfo key;
            string pass = "";
            do
            {
                key = Console.ReadKey(true);

                if (Char.IsLetterOrDigit(key.KeyChar))
                {
                    Console.Write("*");
                }
                
                pass += key.KeyChar;
            } while (key.Key != ConsoleKey.Enter);

            return pass;

        }
    }

}
