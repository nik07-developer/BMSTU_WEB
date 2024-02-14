using Models.View;

namespace DataAccess.DTO.View
{
    public class CharacterViewDTO : CharacterView
    {
        public CharacterViewDTO(string name, List<WidgetViewDTO> widgetViews)
            : base(name,  new List<WidgetView>())
        {
            foreach (var wv in widgetViews)
            {
                WidgetViews.Add(new(wv.Name, wv.PosX, wv.PosY));
            }
        }
    }
}
