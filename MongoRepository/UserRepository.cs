using DataAccess.Interfaces;
using DataAccess.DTO;

using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Models.User;
using Models.Character;

namespace MongoRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<UserDB> _users;
        private readonly IMongoCollection<CharacterDB> _characters;
        private readonly IMongoCollection<CharacterViewDB> _views;

        public UserRepository()
        {
            _client = new MongoClient("mongodb://localhost:8081");
            _db = _client.GetDatabase("web");
            _users = _db.GetCollection<UserDB>("users");
            _characters = _db.GetCollection<CharacterDB>("characters");
            _views = _db.GetCollection<CharacterViewDB>("views");
        }

        public Guid Create(CreateUserDTO user)
        {
            var doc = new UserDB()
            {
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
            };

            _users.InsertOne(doc);
            return doc.ID;
        }

        public void Delete(Guid userId)
        {
            var chars = _characters.Find(filter: x => x.UserID == userId).ToList();
            foreach (var ch in chars)
            {
                _views.DeleteMany(x => x.CharacterID == ch.ID);
            }

            _characters.DeleteMany(x => x.UserID == userId);

            var res = _users.DeleteOne(x => x.ID == userId);
            if (res.DeletedCount == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public UserDTO Get(Guid userId)
        {
            var user = _users.Find(filter: x => x.ID == userId).First();
            if (user == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new UserDTO(user.ID, user.Login, user.Password, user.Name);
        }

        public void UpdateName(Guid userId, string newName)
        {
            var update = Builders<UserDB>.Update.Set(user => user.ID, userId);
            update.Set(user => user.Name, newName);
            var res = _users.FindOneAndUpdate(filter: user => user.ID == userId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdatePassword(Guid userId, string newPassword)
        {
            var update = Builders<UserDB>.Update.Set(user => user.ID, userId);
            update.Set(user => user.Password, newPassword);
            var res = _users.FindOneAndUpdate(filter: user => user.ID == userId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}