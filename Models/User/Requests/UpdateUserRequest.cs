namespace Models.User.Requests
{
    public class UpdateUserRequest
    {
        public string Login;
        public string Password;
        public string Name;

        public UpdateUserRequest(string login, string password, string name)
        {
            Login = login;
            Password = password;
            Name = name;
        }
    }
}
