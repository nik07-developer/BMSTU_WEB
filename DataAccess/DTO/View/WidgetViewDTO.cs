using Models.View;

namespace DataAccess.DTO
{
    public class WidgetViewDTO : WidgetView
    {
        public WidgetViewDTO(string name, int posx, int posy) 
            : base(name, posx, posy)
        {
        }
    }
}
