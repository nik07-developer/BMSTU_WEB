
namespace Models.Character.Requests
{
    public class DeleteCharacterRequest
    {
        public Guid UserId;
        public Guid CharacterId;

        public DeleteCharacterRequest(Guid userId, Guid charaterId)
        {
            UserId = userId;
            CharacterId = charaterId;
        }
    }
}
