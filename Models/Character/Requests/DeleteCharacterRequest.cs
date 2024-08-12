
namespace Models.Character.Requests
{
    public class DeleteCharacterRequest(Guid userId, Guid charaterId)
    {
        public Guid UserId = userId;
        public Guid CharacterId = charaterId;
    }
}
