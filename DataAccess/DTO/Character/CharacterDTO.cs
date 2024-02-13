namespace DataAccess.DTO
{
    public class CharacterDTO : Models.Character.Character
    {
        public CharacterDTO(Guid id, string name, string data) 
            : base(id, name, data)
        {
        }
    }
}
