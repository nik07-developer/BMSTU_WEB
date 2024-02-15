using DataAccess.DTO;
using DataAccess.Interfaces;
using Models.Character;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository
{
    public class CharacterViewRepository : ICharacterViewRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<UserDB> _users;
        private readonly IMongoCollection<CharacterDB> _characters;
        private readonly IMongoCollection<CharacterViewDB> _views;

        public CharacterViewRepository()
        {
            _client = new MongoClient("mongodb://localhost:8081");
            _db = _client.GetDatabase("web");
            _users = _db.GetCollection<UserDB>("users");
            _characters = _db.GetCollection<CharacterDB>("characters");
            _views = _db.GetCollection<CharacterViewDB>("views");
        }

        public void Create(Guid userId, Guid characterId, CreateCharacterViewDTO view)
        {
            if (_characters.Find(filter: x => x.ID == characterId && x.UserID == userId) == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var doc = new CharacterViewDB()
            {
                CharacterID = characterId,
                Name = view.Name,
                Widgets = []
            };

            foreach (var wv in view.WidgetViews)
            {
                doc.Widgets.Add(new WidgetViewDB()
                {
                    Name = wv.Name,
                    PosX = wv.PosX,
                    PosY = wv.PosY
                });
            }

            _views.InsertOne(doc);
        }

        public void Delete(Guid userId, Guid characterId, string name)
        {
            if (_characters.Find(filter: x => x.ID == characterId && x.UserID == userId) == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var res = _views.DeleteOne(x => x.CharacterID == characterId && x.Name == name);

            if (res.DeletedCount == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public CharacterViewDTO Get(Guid userId, Guid characterId, string name)
        {
            if (_characters.Find(filter: x => x.ID == characterId && x.UserID == userId) == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var view = _views.Find(filter: x => x.CharacterID == characterId && x.Name == name).First();

            if (view == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var dto = new CharacterViewDTO(view.Name, []);

            foreach (var wv in view.Widgets)
            {
                dto.WidgetViews.Add(new(wv.Name, wv.PosX, wv.PosY));
            }

            return dto;
        }

        public void Update(Guid userId, Guid characterId, string name, List<WidgetViewDTO> newWidgetViews)
        {
            if (_characters.Find(filter: x => x.ID == characterId && x.UserID == userId) == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var update = Builders<CharacterViewDB>.Update.Set(ch => ch.CharacterID, characterId);
            var widgets = new List<WidgetViewDB>();
            
            foreach( var wv in newWidgetViews)
            {
                widgets.Add(new() { Name = wv.Name, PosX = wv.PosX, PosY = wv.PosY });
            }

            update.Set(view => view.Widgets, widgets);

            var res = _views.FindOneAndUpdate(filter: view => view.CharacterID == characterId, update: update);

            if (res == null)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
