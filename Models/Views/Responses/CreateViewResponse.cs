
namespace Models.Views.Responses
{
    public class CreateViewResponse
    {
        public const int OK = 0;
        public const int ALREADY_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;

        public CreateViewResponse()
        {
            Code = -1;
        }
    }
}
