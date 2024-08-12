namespace Models.Character.Responses
{
    public class CreateCharacterResponse
    {
        public const int OK = 0;
        public const int ALREADY_EXISTS = 1;
        public const int DB_ERROR = 2;

        public int Code;
        public Guid ID;

        public CreateCharacterResponse()
        {
            Code = -1;
            ID = Guid.Empty;
        }
    }
}
