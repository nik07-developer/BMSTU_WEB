namespace Models.User.Responses
{
    public class DeleteUserResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;

        public DeleteUserResponse()
        {
            Code = OK;
        }

        public DeleteUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
