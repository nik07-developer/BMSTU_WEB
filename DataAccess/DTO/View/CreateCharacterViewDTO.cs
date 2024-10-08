﻿using Models.View;

namespace DataAccess.DTO
{
    public class CreateCharacterViewDTO
    {
        public string Name;
        public List<WidgetViewDTO> WidgetViews;

        public CreateCharacterViewDTO(string name, List<WidgetViewDTO> widgetViews)
        {
            Name = name;
            WidgetViews = widgetViews;
        } 
    }
}
