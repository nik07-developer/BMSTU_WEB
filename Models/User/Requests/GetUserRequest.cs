namespace Models.User.Requests
{
    public class GetUserRequest
    {
        public string Login;
        public string Password;
        public string Name;

        public GetUserRequest(string login, string password, string name)
        {
            Login = login;
            Password = password;
            Name = name;
        }
    }
}
