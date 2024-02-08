
namespace Models.Character.Requests
{
    public class CreateCharacterRequest
    {
        public Guid UserId;

        public string Name;
        public string Data;

        public CreateCharacterRequest(Guid userId, string name, string data)
        {
            UserId = userId;

            Name = name;
            Data = data;
        }
    }
}
