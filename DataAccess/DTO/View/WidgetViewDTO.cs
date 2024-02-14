using Models.View;

namespace DataAccess.DTO.View
{
    public class WidgetViewDTO : WidgetView
    {
        public WidgetViewDTO(string name, int posx, int posy) 
            : base(name, posx, posy)
        {
        }
    }
}
