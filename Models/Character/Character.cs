
namespace Models.Character
{
    public class Character(Guid id,
                           string name,
                           int maxHealth,
                           int health,
                           int level,
                           int armorClass,
                           Dictionary<string, CharacterAttribute> attributes,
                           Dictionary<string, CharacterSkill> skills)
    {
        public Guid ID { get; set; } = id;
        public string Name { get; set; } = name;
        public int MaxHealth { get; set; } = maxHealth;
        public int Health { get; set; } = health;
        public int Level { get; set; } = level;
        public int ArmorClass { get; set; } = armorClass;
        public Dictionary<string, CharacterAttribute> Attributes { get; set; } = attributes;
        public Dictionary<string, CharacterSkill> Skills { get; set; } = skills;
    }

    public class CharacterAttribute(int value, char proficiency)
    {
        public int Value { get; set; } = value;
        public char Proficiency { get; set; } = proficiency;
    }

    public class CharacterSkill(char proficiency)
    {
        public char Proficiency { get; set; } = proficiency;
    }
}
