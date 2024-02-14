using Models.View;

namespace DataAccess.DTO.Converters
{
    public static class WidgetViewConverter
    {
        public static WidgetViewDTO Model2DataAccess(WidgetView widgetView)
        {
            return new(widgetView.Name, widgetView.PosX, widgetView.PosY);
        }

        public static List<WidgetViewDTO> Model2DataAccess(List<WidgetView> widgetViews)
        {
            var result = new List<WidgetViewDTO>();
            foreach (var wv in widgetViews)
            {
                result.Add(new(wv.Name, wv.PosX, wv.PosY));
            }

            return result;
        }

        public static WidgetView DataAccess2Model(WidgetViewDTO widgetView)
        {
            return new(widgetView.Name, widgetView.PosX, widgetView.PosY);
        }

        public static List<WidgetView> DataAccess2Model(List<WidgetViewDTO> widgetViews)
        {
            var result = new List<WidgetView>();
            foreach (var wv in widgetViews)
            {
                result.Add(new(wv.Name, wv.PosX, wv.PosY));
            }

            return result;
        }
    }
}
