
namespace Models.Character.Requests
{
    public class GetCharacterRequest
    {
        public Guid UserId;
        public Guid CharacterId;

        public GetCharacterRequest(Guid userId, Guid characterId)
        {
            UserId = userId;
            CharacterId = characterId;
        }
    }
}
