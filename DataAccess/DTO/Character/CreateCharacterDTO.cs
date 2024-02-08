namespace DataAccess.DTO
{
    public class CreateCharacterDTO
    {
        public string Name;
        public string Data;

        public CreateCharacterDTO(string name, string data)
        {
            Name = name;
            Data = data;
        }
    }
}
