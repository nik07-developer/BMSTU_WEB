using Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Character
{
    public class UpdateCharacterDTO(string? name = null,
                            int? maxHealth = null,
                            int? health = null,
                            int? level = null,
                            int? armorClass = null,
                            Dictionary<string, CharacterAttribute>? attributes = null,
                            Dictionary<string, CharacterSkill>? skills = null)
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
