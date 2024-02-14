
namespace Models.Views.Requests
{
    public class CreateViewRequest
    {
        public Guid UserId;
        public Guid CharacterId;

        public string Name;
        public List<WidgetView> WidgetViews;

        public CreateViewRequest(Guid userId, Guid characterId, string name, List<WidgetView> widgetViews)
        {
            UserId = userId;
            CharacterId = characterId;
            Name = name;
            WidgetViews = widgetViews;
        }
    }
}
