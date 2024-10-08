﻿using Models.View;
using DataAccess.DTO.Converters;

namespace DataAccess.DTO
{
    public class CharacterViewDTO : CharacterView
    {
        public CharacterViewDTO(string name, List<WidgetViewDTO> widgetViews)
            : base(name, widgetViews.DataAccess2Model())
        {
        }
    }
}
