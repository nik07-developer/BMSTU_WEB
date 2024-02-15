using Amazon.Runtime.Internal.Transform;
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

        public static CharacterDTO Convert(Models.Character.Character c)
        {
            return new CharacterDTO(c.ID, c.Name, c.MaxHealth, c.Health, c.Level, c.ArmorClass, 
                Convert(c.Attributes), Convert(c.Skills));
        }
        
        public static AttributeDTO Convert(Models.Character.CharacterAttribute a)
        {
            return new AttributeDTO(a.Value, a.Proficiency);
        }

        public static Models.Character.CharacterAttribute Convert(AttributeDTO a)
        {
            return new Models.Character.CharacterAttribute(a.Value, a.Proficiency);
        }

        public static SkillDTO Convert(Models.Character.CharacterSkill s)
        {
            return new SkillDTO(s.Proficiency);
        }

        public static Models.Character.CharacterSkill Convert(SkillDTO s)
        {
            return new Models.Character.CharacterSkill(s.Proficiency);
        }

        public static Dictionary<string, AttributeDTO> Convert(Dictionary<string, Models.Character.CharacterAttribute> dict)
        {
            var result = new Dictionary<string, AttributeDTO>();

            foreach (var pair in dict)
            {
                result.Add(pair.Key, Convert(pair.Value));
            }

            return result;
        }

        public static Dictionary<string, Models.Character.CharacterAttribute> Convert(Dictionary<string, AttributeDTO> dict)
        {
            var result = new Dictionary<string, Models.Character.CharacterAttribute>();

            foreach (var pair in dict)
            {
                result.Add(pair.Key, Convert(pair.Value));
            }

            return result;
        }

        public static Dictionary<string, Models.Character.CharacterSkill> Convert(Dictionary<string, SkillDTO> dict)
        {
            var result = new Dictionary<string, Models.Character.CharacterSkill>();

            foreach (var pair in dict)
            {
                result.Add(pair.Key, Convert(pair.Value));
            }

            return result;
        }

        public static Dictionary<string, SkillDTO> Convert(Dictionary<string, Models.Character.CharacterSkill> dict)
        {
            var result = new Dictionary<string, SkillDTO>();

            foreach (var pair in dict)
            {
                result.Add(pair.Key, Convert(pair.Value));
            }

            return result;
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
