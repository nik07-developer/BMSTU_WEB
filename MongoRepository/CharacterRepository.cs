using DataAccess.DTO;
using DataAccess.Interfaces;
using Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<UserDB> _users;
        private readonly IMongoCollection<CharacterDB> _characters;
        private readonly IMongoCollection<CharacterViewDB> _views;

        public CharacterRepository()
        {
            _client = new MongoClient("mongodb://localhost:8081");
            _db = _client.GetDatabase("web");
            _users = _db.GetCollection<UserDB>("users");
            _characters = _db.GetCollection<CharacterDB>("characters");
            _views = _db.GetCollection<CharacterViewDB>("views");
        }

        public Guid Create(Guid userId, CreateCharacterDTO character)
        {
            if (_users.Find(filter: x => x.ID == userId) == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var doc = new CharacterDB()
            {
                UserID = userId,
                Name = character.Name,
                MaxHealth = character.MaxHealth,
                Health = character.Health,
                Level = character.Level,
                ArmorClass = character.ArmorClass,
                Attributes = [],
                Skills = [],
            };

            foreach (var a in character.Attributes)
            {
                doc.Attributes.Add(new CharacterAttributeDB()
                {
                    Name = a.Key,
                    Proficiency = a.Value.Proficiency,
                    Value = a.Value.Value
                });
            }

            foreach (var s in character.Skills)
            {
                doc.Skills.Add(new CharacterSkillDB()
                {
                    Name = s.Key,
                    Proficiency = s.Value.Proficiency,
                });
            }

            _characters.InsertOne(doc);
            return doc.ID;
        }

        public void Delete(Guid userId, Guid characterId)
        {
            _views.DeleteMany(x => x.CharacterID == characterId);

            var res = _characters.DeleteOne(x => x.UserID == userId && x.ID == characterId);
            if (res.DeletedCount == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public CharacterDTO Get(Guid userId, Guid characterId)
        {
            var ch = _characters.Find(filter: x => x.ID == userId).First();
            if (ch == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Convert(ch);
        }

        public List<CharacterDTO> GetAll(Guid userId)
        {
            var characters = _characters.Find(filter: x => x.ID == userId).ToList();
            var result = new List<CharacterDTO>();

            foreach (var ch in characters)
            {
                result.Add(Convert(ch));
            }

            return result;
        }

        public void UpdateArmorClass(Guid userId, Guid characterId, int newArmorClass)
        {
            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.ArmorClass, newArmorClass);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateAttributes(Guid userId, Guid characterId, Dictionary<string, CharacterAttribute> newAttributes)
        {
            var attributes = new List<CharacterAttributeDB>();
            foreach (var pair in newAttributes)
            {
                attributes.Add(new() { Name = pair.Key, Value = pair.Value.Value, Proficiency = pair.Value.Proficiency });
            }

            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.Attributes, attributes);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateHealth(Guid userId, Guid characterId, int newHealth)
        {
            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.Health, newHealth);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateLevel(Guid userId, Guid characterId, int newLevel)
        {
            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.Level, newLevel);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateMaxHealth(Guid userId, Guid characterId, int newMaxHealth)
        {
            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.MaxHealth, newMaxHealth);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateName(Guid userId, Guid characterId, string newName)
        {
            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.Name, newName);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateSkills(Guid userId, Guid characterId, Dictionary<string, CharacterSkill> newSkills)
        {
            var skills = new List<CharacterSkillDB>();
            foreach (var pair in newSkills)
            {
                skills.Add(new() { Name = pair.Key, Proficiency = pair.Value.Proficiency });
            }

            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);
            update.Set(ch => ch.Skills, skills);

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private static CharacterDTO Convert(CharacterDB ch)
        {
            var dto = new CharacterDTO(ch.UserID,
                                       ch.Name,
                                       ch.MaxHealth,
                                       ch.Health,
                                       ch.Level,
                                       ch.ArmorClass,
                                       [],
                                       []);

            foreach (var a in ch.Attributes)
            {
                dto.Attributes.Add(a.Name, new(a.Value, a.Proficiency));
            }

            foreach (var s in ch.Skills)
            {
                dto.Skills.Add(s.Name, new(s.Proficiency));
            }

            return dto;
        }
    }
}
