namespace Web.DTO.ViewConfig
{
    public class ViewConfigDTO(string name, List<WidgetDTO> widgets)
    {
        public string Name { get; set; } = name;
        public List<WidgetDTO> Widgets { get; set; } = widgets;
    }
}
