namespace Web.DTO.Character
{
    public class CharacterDTO(Guid id,
                           string name,
                           int maxHealth,
                           int health,
                           int level,
                           int armorClass,
                           Dictionary<string, AttributeDTO> attributes,
                           Dictionary<string, SkillDTO> skills)
    {
        public Guid ID { get; set; } = id;
        public string Name { get; set; } = name;
        public int MaxHealth { get; set; } = maxHealth;
        public int Health { get; set; } = health;
        public int Level { get; set; } = level;
        public int ArmorClass { get; set; } = armorClass;
        public Dictionary<string, AttributeDTO> Attributes { get; set; } = attributes;
        public Dictionary<string, SkillDTO> Skills { get; set; } = skills;

    }

    public class AttributeDTO(int value, string proficiency)
    {
        public int Value { get; set; } = value;
        public string Proficiency { get; set; } = proficiency;
    }

    public class SkillDTO(string proficiency)
    {
        public string Proficiency { get; set; } = proficiency;
    }
}