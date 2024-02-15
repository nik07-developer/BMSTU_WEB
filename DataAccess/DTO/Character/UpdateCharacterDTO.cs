using Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Character
{
    public class UpdateCharacterDTO(string? name,
                            int? maxHealth,
                            int? health,
                            int? level,
                            int? armorClass,
                            Dictionary<string, CharacterAttribute>? attributes,
                            Dictionary<string, CharacterSkill>? skills)
    {
        public string? Name { get; set; } = name;
        public int? MaxHealth { get; set; } = maxHealth;
        public int? Health { get; set; } = health;
        public int? Level { get; set; } = level;
        public int? ArmorClass { get; set; } = armorClass;
        public Dictionary<string, CharacterAttribute>? Attributes { get; set; } = attributes;
        public Dictionary<string, CharacterSkill>? Skills { get; set; } = skills;
    }
}
