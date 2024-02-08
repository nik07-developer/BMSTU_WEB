namespace Web.DTO.Character
{
    public class Character(Guid id,
                           string name,
                           string data)
    {
        public Guid ID = id;
        public string Name = name;
        public string Data = data;
    }
}
