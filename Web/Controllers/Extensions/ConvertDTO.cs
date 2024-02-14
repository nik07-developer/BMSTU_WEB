using Web.DTO.Character;
using Web.DTO.User;
using Web.DTO.ViewConfig;

namespace Web.Controllers.Extensions
{
    public static class ConvertDTO
    {
        public static List<CharacterDTO> Convert(List<Models.Character.Character> list)
        {
            var result = new List<CharacterDTO>();

            foreach (var ch in list)
            {
                result.Add(Convert(ch));
            }

            return result;
        }

        public static CharacterDTO Convert(Models.Character.Character ch)
        {
            return new CharacterDTO(ch.ID, ch.Name, ch.Data);
        }

        public static UserDTO Convert(Models.User.User user)
        {
            return new UserDTO(user.ID, user.Login, user.Password, user.Name);
        }

        public static Models.View.WidgetView Convert(WidgetDTO widget)
        {
            return new Models.View.WidgetView(widget.Name, widget.PosX, widget.PosY);
        }

        public static List<Models.View.WidgetView> Convert(List<WidgetDTO> widgets)
        {
            var result = new List<Models.View.WidgetView>();

            foreach (var w in widgets)
            {
                result.Add(Convert(w));
            }

            return result;
        }

        public static WidgetDTO Convert(Models.View.WidgetView view)
        {
            return new(view.Name, view.PosX, view.PosY);
        }

        public static List<WidgetDTO> Convert(List<Models.View.WidgetView> views)
        {
            var result = new List<WidgetDTO>();
            foreach (var v in views)
            {
                result.Add(Convert(v));
            }

            return result;
        }

        public static ViewConfigDTO Convert(Models.View.CharacterView view)
        {
            return new ViewConfigDTO(view.Name, Convert(view.WidgetViews));
        } 
    }
}
