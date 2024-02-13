using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.DTO;
using Models;

namespace DataAccess.Interfaces
{
    public interface ICharacterRepository
    {
        public Guid Create(Guid userId, CreateCharacterDTO character);
        public List<CharacterDTO> GetAll(Guid userId);
        public CharacterDTO Get(Guid userId, Guid characterId);
        public void Delete(Guid userId, Guid characterId);
        public void UpdateName(Guid userId, Guid characterId, string newName);
        public void UpdateData(Guid userId, Guid characterId, string newData);
    }
}
