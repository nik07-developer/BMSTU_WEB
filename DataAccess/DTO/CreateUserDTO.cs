namespace DataAccess.DTO
{
    public class CreateUserDTO
    {
        public string Login;
        public string Password;
        public string Name;

        public CreateUserDTO(string login, string password, string name)
        {
            Login = login;
            Password = password;
            Name = name;
        }
    }
}