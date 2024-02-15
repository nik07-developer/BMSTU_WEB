namespace Models.User.Requests
{
    public class GetUserRequest
    {
        public Guid ID;
        public string Login;
        public string Password;

        public GetUserRequest(Guid id)
        {
            ID = id;
            Login = "null";
            Password = "null";
        }

        public GetUserRequest(string login, string password)
        {
            ID = Guid.Empty;
            Login = login;
            Password = password;
        }
    }
}
