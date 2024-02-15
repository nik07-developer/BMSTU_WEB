using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.DTO;
using DataAccess.DTO.Character;
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
        public void Update(Guid userId, Guid characterId, UpdateCharacterDTO updateDTO);
    }
}
