namespace Models.User.Requests
{
    public class CreateUserRequest
    {
        public string Login;
        public string Password;
        public string Name;

        public CreateUserRequest(string login, string password, string name)
        {
            Login = login;
            Password = password;
            Name = name;
        }
    }
}
