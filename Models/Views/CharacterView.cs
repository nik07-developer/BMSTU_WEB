namespace Models.Views
{
    public class CharacterView
    {
        public string Name { get; private set; }
        public List<WidgetView> WidgetViews { get; private set; }

        public CharacterView(string name, List<WidgetView> widgetViews)
        {
            Name = name;
            WidgetViews = widgetViews;
        }
    }
}
