using Models.Character;

namespace DataAccess.DTO
{
    public class CharacterDTO : Character
    {
        public CharacterDTO(Guid id, 
                            string name, 
                            int maxHealth, 
                            int health, 
                            int level, 
                            int armorClass, 
                            Dictionary<string, CharacterAttribute> attributes, 
                            Dictionary<string, CharacterSkill> skills) 
            : base(id, name, maxHealth, health, level, armorClass, attributes, skills)
        {
        }
    }
}
