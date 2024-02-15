using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository
{
    public static class MongoConfig
    {
        public static string DB_ADDRESS => "mongodb://localhost:8081";
        public static string DB_NAME => "web";
        public static string USERS_COLLECTION => "users";
        public static string CHARACTERS_COLLECTION =>  "characters";
        public static string VIEWS_COLLECTION =>  "views";
    }
}
