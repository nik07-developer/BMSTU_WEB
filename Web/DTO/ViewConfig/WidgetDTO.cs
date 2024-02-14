namespace Web.DTO.ViewConfig
{
    public class WidgetDTO(string name, int posx, int posy)
    {
        public string Name { get; set; } = name;
        public int PosX { get; set; } = posx;
        public int PosY { get; set; } = posy;
    }
}
