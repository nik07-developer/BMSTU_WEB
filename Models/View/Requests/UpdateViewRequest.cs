
namespace Models.View.Requests
{
    public class UpdateViewRequest
    {
        public Guid UserId;
        public Guid CharacterId;
        public string Name;
        public List<WidgetView> NewWidgets;

        public UpdateViewRequest(Guid userId, Guid characterId, string name, List<WidgetView> newWidgets)
        {
            UserId = userId;
            CharacterId = characterId;
            Name = name;
            NewWidgets = newWidgets;
        }
    }
}
