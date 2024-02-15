
/*
 * using MongoRepository;
using MongoDB.Driver;
using MongoDB.Bson;
using Models.Character;
using Models.User;

var client = new MongoClient("mongodb://localhost:8081");
var db = client.GetDatabase("web");

db.CreateCollection("users");
db.CreateCollection("characters");
db.CreateCollection("views");
*/
/*
var users = db.GetCollection<UserDB>("users");
var doc = new UserDB()
{
    Login = "admin",
    Password = "admin",
    Name = "aboba",
};

users.InsertOne(doc);*/