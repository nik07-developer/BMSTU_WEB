namespace Models.User.Responses
{
    public class GetUserResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;
        public User? User;

        public GetUserResponse()
        {
            Code = -1;
            User = null;
        }
    }
}
