using DataAccess.Interfaces;
using DataAccess.DTO;

using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Models.User;

namespace MongoRepository
{
    class UserDB
    {
        [BsonId]
        public Guid ID { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<UserDB> _users;

        public UserRepository()
        {
            _client = new MongoClient("mongodb://localhost:8081");
            _db = _client.GetDatabase("web");
            _users = _db.GetCollection<UserDB>("users");
        }

        public Guid Create(CreateUserDTO user)
        {
            var doc = new UserDB()
            {
                Login = user.Login,
                Password = user.Password,
                Name = user.Name
            };

            _users.InsertOne(doc);
            return doc.ID;
        }

        public void Delete(Guid id)
        {
            var res = _users.DeleteOne(x => x.ID == id);
        }

        public UserDTO Get(Guid id)
        {
            var user = _users.Find(filter: x => x.ID == id).First();
            return new UserDTO(user.ID, user.Login, user.Password, user.Name);
        }

        public void UpdateName(Guid id, string newName)
        {
            var update = Builders<UserDB>.Update.Set(user => user.ID, id);
            update.Set(user => user.Name, newName);
            _users.FindOneAndUpdate(filter: user => user.ID == id, update: update);
        }

        public void UpdatePassword(Guid id, string newPassword)
        {
            var update = Builders<UserDB>.Update.Set(user => user.ID, id);
            update.Set(user => user.Password, newPassword);
            _users.FindOneAndUpdate(filter: user => user.ID == id, update: update);
        }
    }
}