
namespace Models.Views.Requests
{
    public class UpdateViewRequest
    {
        public Guid UserId;
        public Guid CharacterId;
        public List<WidgetView> NewWidgets;

        public UpdateViewRequest(Guid userId, Guid characterId, List<WidgetView> newWidgets)
        {
            UserId = userId;
            CharacterId = characterId;
            NewWidgets = newWidgets;
        }
    }
}
