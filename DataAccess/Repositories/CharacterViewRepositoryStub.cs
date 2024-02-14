using DataAccess.DTO.View;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class CharacterViewRepositoryStub : ICharacterViewRepository
    {
        private List<string> names = new();

        public void Create(Guid userId, Guid characterId, CreateCharacterViewDTO view)
        {
            names.Add("Win10");
        }

        public void Delete(Guid userId, Guid characterId, string name)
        {
            names.RemoveAt(0);
        }

        public CharacterViewDTO Get(Guid userId, Guid characterId, string name)
        {
            return new CharacterViewDTO("Win10", new() { new("attributes-view", 100, 100), new("skills-view", 250, 100) });
        }

        public void Update(Guid userId, Guid characterId, string name, List<WidgetViewDTO> newWidgetViews)
        {
            
        }
    }
}
