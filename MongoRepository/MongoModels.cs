using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository
{
    public class UserDB
    {
        [BsonId]
        public Guid ID { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class CharacterDB
    {
        [BsonId]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int ArmorClass { get; set; }

        public List<CharacterAttributeDB> Attributes { get; set; }
        public List<CharacterSkillDB> Skills { get; set; }

        public Guid UserID { get; set; }
    }

    public class CharacterAttributeDB
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public char Proficiency { get; set; }
    }

    public class CharacterSkillDB
    {
        public string Name { get; set; }
        public char Proficiency { get; set; }
    }

    public class CharacterViewDB
    {
        public string Name { get; set; }
        public List<WidgetViewDB> Widgets { get; set; }

        public Guid CharacterID { get; set; }
    }

    public class WidgetViewDB
    {
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}
