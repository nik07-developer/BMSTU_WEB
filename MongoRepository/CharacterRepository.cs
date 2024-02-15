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
using DataAccess.DTO.Character;

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
            _client = new MongoClient(MongoConfig.DB_ADDRESS);
            _db = _client.GetDatabase(MongoConfig.DB_NAME);
            _users = _db.GetCollection<UserDB>(MongoConfig.USERS_COLLECTION);
            _characters = _db.GetCollection<CharacterDB>(MongoConfig.CHARACTERS_COLLECTION);
            _views = _db.GetCollection<CharacterViewDB>(MongoConfig.VIEWS_COLLECTION);
        }

        public Guid Create(Guid userId, CreateCharacterDTO character)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            if (_users.Find(filter: x => x.ID == userId) == null)
            {
                session.AbortTransaction();
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
            session.CommitTransaction();

            return doc.ID;
        }

        public void Delete(Guid userId, Guid characterId)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            _views.DeleteMany(x => x.CharacterID == characterId);

            var res = _characters.DeleteOne(x => x.UserID == userId && x.ID == characterId);
            if (res.DeletedCount == 0)
            {
                session.AbortTransaction();
                throw new ArgumentOutOfRangeException();
            }

            session.CommitTransaction();
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
            var characters = _characters.Find(filter: x => x.UserID == userId).ToList();
            var result = new List<CharacterDTO>();

            foreach (var ch in characters)
            {
                result.Add(Convert(ch));
            }

            return result;
        }

        public void Update(Guid userId, Guid characterId, UpdateCharacterDTO updateDTO)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            var update = Builders<CharacterDB>.Update.Set(ch => ch.ID, characterId);

            if (updateDTO.Name != null)
            {
                update.Set(ch => ch.Name, updateDTO.Name);
            }

            if (updateDTO.MaxHealth != null)
            {
                update.Set(ch => ch.MaxHealth, updateDTO.MaxHealth);
            }

            if (updateDTO.Health != null) 
            {
                update.Set(ch => ch.Health, updateDTO.Health);
            }

            if (updateDTO.Level != null)
            {
                update.Set(ch => ch.Level, updateDTO.Level);
            }

            if (updateDTO.ArmorClass != null)
            {
                update.Set(ch => ch.ArmorClass, updateDTO.ArmorClass);
            }

            if (updateDTO.Attributes != null)
            {
                var attributes = new List<CharacterAttributeDB>();
                foreach (var pair in updateDTO.Attributes)
                {
                    attributes.Add(new() { Name = pair.Key, Value = pair.Value.Value, Proficiency = pair.Value.Proficiency });
                }

                update.Set(ch => ch.Attributes, attributes);
            }

            if (updateDTO.Skills != null)
            {
                var skills = new List<CharacterSkillDB>();
                foreach (var pair in updateDTO.Skills)
                {
                    skills.Add(new() { Name = pair.Key, Proficiency = pair.Value.Proficiency });
                }

                update.Set(ch => ch.Skills, skills);
            }

            var res = _characters.FindOneAndUpdate(filter: ch => ch.UserID == userId && ch.ID == characterId, update: update);

            if (res == null)
            {
                session.AbortTransaction();
                throw new ArgumentOutOfRangeException();
            }

            session.CommitTransaction();
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
