
namespace Models.Character.Responses
{
    public class GetCharacterResponse
    {
        public const int OK = 0;
        public const int NOT_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;
        public Character? Character;

        public GetCharacterResponse()
        {
            Code = -1;
            Character = null;
        }
    }
}
