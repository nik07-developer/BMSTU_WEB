namespace Web.DTO.Character
{
    public class CharacterDTO(Guid id,
                           string name,
                           string data)
    {
        public Guid ID { get; set; } = id;
        public string Name { get; set; } = name;
        public string Data { get; set; } = data;
    }
}
