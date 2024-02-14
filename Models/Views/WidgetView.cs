
namespace Models.Views
{
    public class WidgetView
    {
        public string Name { get; private set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public WidgetView(string name, int posx, int posy)
        {
            Name = name;
            PosX = posx;
            PosY = posy;
        }
    }
}
