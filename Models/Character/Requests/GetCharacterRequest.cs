
namespace Models.Character.Requests
{
    public class GetCharacterRequest(Guid userId, Guid characterId)
    {
        public Guid UserId = userId;
        public Guid CharacterId = characterId;
    }
}
