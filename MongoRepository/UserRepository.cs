using DataAccess.Interfaces;
using DataAccess.DTO;

using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Models.User;
using Models.Character;
using DataAccess.DTO.User;

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
            _client = new MongoClient(MongoConfig.DB_ADDRESS);
            _db = _client.GetDatabase(MongoConfig.DB_NAME);
            _users = _db.GetCollection<UserDB>(MongoConfig.USERS_COLLECTION);
            _characters = _db.GetCollection<CharacterDB>(MongoConfig.CHARACTERS_COLLECTION);
            _views = _db.GetCollection<CharacterViewDB>(MongoConfig.VIEWS_COLLECTION);
        }

        public Guid Create(CreateUserDTO user)
        {
            var doc = new UserDB()
            {
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
            };

            var session = _client.StartSession();
            session.StartTransaction();

            var uu = _users.Find(filter: x => x.Login == user.Login);

            if (uu == null)
            {
                session.AbortTransaction();
                throw new ArgumentOutOfRangeException();
            }

            _users.InsertOne(doc);
            session.CommitTransaction();

            return doc.ID;
        }

        public void Delete(Guid userId)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            var chars = _characters.Find(filter: x => x.UserID == userId).ToList();
            foreach (var ch in chars)
            {
                _views.DeleteMany(x => x.CharacterID == ch.ID);
            }

            _characters.DeleteMany(x => x.UserID == userId);

            var res = _users.DeleteOne(x => x.ID == userId);
            if (res.DeletedCount == 0)
            {
                session.AbortTransaction();
                throw new ArgumentOutOfRangeException();
            }

            session.CommitTransaction();
        }

        public UserDTO Find(string login, string password)
        {
            var user = _users.Find(filter: x => x.Login == login && x.Password == password).First();
            if (user == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new UserDTO(user.ID, user.Login, user.Password, user.Name);
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

        public void Update(Guid userId, UpdateUserDTO updateDTO)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            var update = Builders<UserDB>.Update.Set(user => user.ID, userId);

            if (updateDTO.Name != null)
            {
                update.Set(user => user.Name, updateDTO.Name);
            }

            if (updateDTO.Password != null)
            {
                update.Set(user => user.Password, updateDTO.Password);
            }

            var res = _users.FindOneAndUpdate(filter: user => user.ID == userId, update: update);

            if (res == null)
            {
                session.AbortTransaction();
                throw new ArgumentOutOfRangeException();
            }

            session.CommitTransaction();
        }
    }
}