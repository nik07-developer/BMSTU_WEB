namespace Models.User.Responses
{
    public class UpdateUserResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;

        public UpdateUserResponse()
        {
            Code = OK;
        }

        public UpdateUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
