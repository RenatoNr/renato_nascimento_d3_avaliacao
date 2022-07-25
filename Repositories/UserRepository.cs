using renato_nascimento_d3_avaliacao.Models;
using System.Data.SqlClient;
using static BCrypt.Net.BCrypt;


namespace renato_nascimento_d3_avaliacao.Repositories
{
    public class UserRepository
    {
        private readonly string stringConnection = "Data source=DESKTOP-H81L0KE\\SQLEXPRESS; initial catalog=ProvaD3; integrated security=true;";
        
        public UserRepository()
        {
           
        }

        public string CreateUser(User user)
        {
            try
            {
                using (SqlConnection con = new(stringConnection))
                {
                    string query = "INSERT INTO Users (IdUser, Name, Email, Password) VALUES (@IdUser, @Name, @Email, @Password)";
                    using SqlCommand cmd = con.CreateCommand();
                    string strongPass = HashPassword(user.Password);
                    cmd.Parameters.AddWithValue("@IdUser", user.Id);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", strongPass);
                    cmd.CommandText = query;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }

                LoggerRepository.WriteInLogFile(user, "new");

                return $"Usuário criado com sucesso";
            }
            catch (Exception e)
            {

                return $"Erro ao criar o Usuário - {e}";
            }
            
        }

        public User Login(string email, string password)
        {
            
            User user = new User();

            using SqlConnection con = new(stringConnection);

            string query = "SELECT TOP 1 * FROM Users WHERE Email=@Email";

            using SqlCommand cmd = con.CreateCommand();
            
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.CommandText = query;
            con.Open();

            var result = cmd.ExecuteReader();

            if (!result.Read())
            {
                return user;
            }

            string hashed_pass = result.GetString(3);
            var isPasswordValid = Verify(password, hashed_pass);

            if (!isPasswordValid)
            {
                return user;
            }

          
            user.Id = Guid.Parse( result.GetString(0));
            user.Name = result.GetString(1);
            user.Email = result.GetString(2);

            return user;
          

        }

    }
}
