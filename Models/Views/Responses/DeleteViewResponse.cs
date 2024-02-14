
namespace Models.Views.Responses
{
    public class DeleteViewResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;

        public DeleteViewResponse()
        {
            Code = -1;
        }
    }
}
