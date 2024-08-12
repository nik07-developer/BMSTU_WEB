
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.Character.Requests
{
    public class CreateCharacterRequest(Guid userId,
                           string name,
                           int maxHealth,
                           int health,
                           int level,
                           int armorClass,
                           Dictionary<string, CharacterAttribute> attributes,
                           Dictionary<string, CharacterSkill> skills)
    {
        public Guid UserId { get; set; } = userId;

        public string Name { get; set; } = name;
        public int MaxHealth { get; set; } = maxHealth;
        public int Health { get; set; } = health;
        public int Level { get; set; } = level;
        public int ArmorClass { get; set; } = armorClass;
        public Dictionary<string, CharacterAttribute> Attributes { get; set; } = attributes;
        public Dictionary<string, CharacterSkill> Skills { get; set; } = skills;
    }
}
