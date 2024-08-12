
using MongoRepository;
using MongoDB.Driver;
using MongoDB.Bson;
using Models.Character;
using Models.User;

public class Migrations
{
    public Migrations()
    {
        Init();
    }

    public void Init()
    {
        var client = new MongoClient(MongoConfig.DB_ADDRESS);
        var db = client.GetDatabase(MongoConfig.DB_NAME);

        db.CreateCollection(MongoConfig.USERS_COLLECTION);
        db.CreateCollection(MongoConfig.CHARACTERS_COLLECTION);
        db.CreateCollection(MongoConfig.VIEWS_COLLECTION);

        var users = db.GetCollection<UserDB>(MongoConfig.USERS_COLLECTION);
        var doc = new UserDB()
        {
            Login = "admin",
            Password = "admin",
            Name = "aboba",
        };

        users.InsertOne(doc);
    }
}