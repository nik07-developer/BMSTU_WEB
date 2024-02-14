
namespace Models.Views.Requests
{
    public class GetViewRequest
    {
        public Guid UserId;
        public Guid CharacterId;
        public string Name;

        public GetViewRequest(Guid userId, Guid characterId, string name)
        {
            UserId = userId;
            CharacterId = characterId;
            Name = name;
        }
    }
}
