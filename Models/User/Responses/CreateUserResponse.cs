namespace Models.User.Responses
{
    public class CreateUserResponse
    {
        public const int OK = 0;
        public const int ALREADY_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;
        public Guid ID;

        public CreateUserResponse(Guid id)
        {
            ID = id;
            Code = 0;
        }

        public CreateUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
