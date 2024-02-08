using Web.DTO.Character;
using Web.DTO.User;

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
    }
}
