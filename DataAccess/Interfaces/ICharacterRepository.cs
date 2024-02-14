using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.DTO;
using Models;
using Models.Character;

namespace DataAccess.Interfaces
{
    public interface ICharacterRepository
    {
        public Guid Create(Guid userId, CreateCharacterDTO character);
        public List<CharacterDTO> GetAll(Guid userId);
        public CharacterDTO Get(Guid userId, Guid characterId);
        public void Delete(Guid userId, Guid characterId);
        public void UpdateName(Guid userId, Guid characterId, string newName);
        public void UpdateMaxHealth(Guid userId, Guid characterId, int newMaxHealth);
        public void UpdateHealth(Guid userId, Guid characterId, int newHealth);
        public void UpdateLevel(Guid userId, Guid characterId, int newLevel);
        public void UpdateArmorClass(Guid userId, Guid characterId, int newArmorClass);
        public void UpdateAttributes(Guid userId, Guid characterId, Dictionary<string, CharacterAttribute> attributes);
        public void UpdateSkills(Guid userId, Guid characterId, Dictionary<string, CharacterSkill> skills);


    }
}
