using DataAccess.DTO;

namespace DataAccess.Interfaces
{
    public interface ICharacterViewRepository
    {
        public void Create(Guid userId, Guid characterId, CreateCharacterViewDTO view);
        public CharacterViewDTO Get(Guid userId, Guid characterId, string name);
        public void Update(Guid userId, Guid characterId, string name, List<WidgetViewDTO> newWidgetViews);
        public void Delete(Guid userId, Guid characterId, string name);
    }
}
