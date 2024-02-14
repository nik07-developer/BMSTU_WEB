
namespace Models.Character.Responses
{
    public class GetCharactersResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;
        public List<Character> Characters;

        public GetCharactersResponse()
        {
            Code = -1;
            Characters = [];
        }
    }
}
