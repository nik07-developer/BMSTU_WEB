
namespace Models.Views.Responses
{
    public class GetViewResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;
        public CharacterView? CharacterView;

        public GetViewResponse()
        {
            Code = -1;
            CharacterView = null;
        }
    }
}
