
namespace Models.Character.Requests
{
    public class UpdateCharacterRequest
    {
        public Guid UserId;
        public Guid CharacterId;
        public Dictionary<string, string> Changes;

        public UpdateCharacterRequest(Guid userId, Guid characterId, Dictionary<string, string> changes)
        {
            UserId = userId;
            CharacterId = characterId;
            Changes = changes;
        }
    }
}
