using DataAccess.DTO;
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
            guids.Append(Guid.NewGuid());
            return guids.Last();
        }

        public void Delete(Guid userId, Guid characterId)
        {
            guids.Remove(characterId);
        }

        public CharacterDTO Get(Guid userId, Guid characterId)
        {
            return new CharacterDTO(characterId, "Frodo Goblings", "Rogue 4 lvl");
        }

        public List<CharacterDTO> GetAll(Guid userId)
        {
            return new() { new CharacterDTO(guids.Last(), "Frodo Goblings", "Rogue 4 lvl") };
        }

        public void UpdateData(Guid userId, Guid characterId, string newData)
        {
            
        }

        public void UpdateName(Guid userId, Guid characterId, string newName)
        {
            
        }
    }
}
