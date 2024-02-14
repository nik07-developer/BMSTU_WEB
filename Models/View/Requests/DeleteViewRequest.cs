
namespace Models.View.Requests
{
    public class DeleteViewRequest
    {
        public Guid UserId;
        public Guid CharacterId;
        public string Name;

        public DeleteViewRequest(Guid userId, Guid characterId, string name)
        {
            UserId = userId;
            CharacterId = characterId;
            Name = name;
        }
    }
}
