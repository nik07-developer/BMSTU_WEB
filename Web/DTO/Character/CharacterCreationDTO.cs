using System.Reflection.Emit;
using System.Xml.Linq;

namespace Web.DTO.Character
{
    public class CharacterCreationDTO
    {
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int ArmorClass { get; set; }
        public Dictionary<string, AttributeDTO> Attributes { get; set; }
        public Dictionary<string, SkillDTO> Skills { get; set; }
    }
}
