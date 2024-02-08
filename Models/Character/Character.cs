
namespace Models.Character
{
    public class Character
    {
        public Guid ID { get; }
        public string Name { get; set; }
        public string Data { get; set; }

        public Character(Guid id, string name, string data)
        {
            ID = id;
            Name = name;
            Data = data;
        }
    }
}
