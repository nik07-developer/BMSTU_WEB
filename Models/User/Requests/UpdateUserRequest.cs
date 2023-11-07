namespace Models.User.Requests
{
    public class DeleteUserRequest
    {
        public string Login;
        public string Password;
        public string Name;

        public DeleteUserRequest(string login, string password, string name)
        {
            Login = login;
            Password = password;
            Name = name;
        }
    }
}
