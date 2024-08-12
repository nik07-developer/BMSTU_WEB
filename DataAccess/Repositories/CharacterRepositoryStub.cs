using DataAccess.DTO;
using DataAccess.DTO.Character;
using DataAccess.Interfaces;
using Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CharacterRepositoryStub : ICharacterRepository
    {
        private List<Guid> guids = new();

        public Guid Create(Guid userId, CreateCharacterDTO character)
        {
            guids.Add(Guid.NewGuid());
            return guids.Last();
        }

        public void Delete(Guid userId, Guid characterId)
        {
            guids.Remove(characterId);
        }

        public CharacterDTO Get(Guid userId, Guid characterId)
        {
            throw new NotImplementedException();
        }

        public List<CharacterDTO> GetAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid userId, Guid characterId, UpdateCharacterDTO updateDTO)
        {
            throw new NotImplementedException();
        }

        public void UpdateArmorClass(Guid userId, Guid characterId, int newArmorClass)
        {
            throw new NotImplementedException();
        }

        public void UpdateAttributes(Guid userId, Guid characterId, Dictionary<string, CharacterAttribute> attributes)
        {
            throw new NotImplementedException();
        }

        public void UpdateHealth(Guid userId, Guid characterId, int newHealth)
        {
            throw new NotImplementedException();
        }

        public void UpdateLevel(Guid userId, Guid characterId, int newLevel)
        {
            throw new NotImplementedException();
        }

        public void UpdateMaxHealth(Guid userId, Guid characterId, int newMaxHealth)
        {
            throw new NotImplementedException();
        }

        public void UpdateName(Guid userId, Guid characterId, string newName)
        {
            throw new NotImplementedException();
        }

        public void UpdateSkills(Guid userId, Guid characterId, Dictionary<string, CharacterSkill> skills)
        {
            throw new NotImplementedException();
        }
    }
}
