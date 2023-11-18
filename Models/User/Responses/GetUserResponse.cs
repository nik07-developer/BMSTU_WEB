namespace Models.User.Responses
{
    public class GetUserResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;

        public Guid ID;
        public string Login;
        public string Password;
        public string Name;

        public GetUserResponse(Guid id, string login, string password, string name)
        {
            Code = OK;

            ID = id;
            Login = login;
            Password = password;
            Name = name;
        }

        public GetUserResponse(int returnCode)
        {
            Code = returnCode;

            ID = Guid.Empty;
            Login = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }
    }
}
