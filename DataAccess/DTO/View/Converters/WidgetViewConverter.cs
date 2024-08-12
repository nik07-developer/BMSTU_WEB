using Models.View;

namespace DataAccess.DTO.Converters
{
    public static class WidgetViewConverter
    {
        public static WidgetViewDTO Model2DataAccess(this WidgetView widgetView)
        {
            return new(widgetView.Name, widgetView.PosX, widgetView.PosY);
        }

        public static List<WidgetViewDTO> Model2DataAccess(this List<WidgetView> widgetViews)
        {
            var result = new List<WidgetViewDTO>();
            foreach (var wv in widgetViews)
            {
                result.Add(wv.Model2DataAccess());
            }

            return result;
        }

        public static WidgetView DataAccess2Model(this WidgetViewDTO widgetView)
        {
            return new(widgetView.Name, widgetView.PosX, widgetView.PosY);
        }

        public static List<WidgetView> DataAccess2Model(this List<WidgetViewDTO> widgetViews)
        {
            var result = new List<WidgetView>();
            foreach (var wv in widgetViews)
            {
                result.Add(wv.DataAccess2Model());
            }

            return result;
        }
    }
}
