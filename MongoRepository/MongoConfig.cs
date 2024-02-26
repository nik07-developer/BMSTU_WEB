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
        //public static string DB_ADDRESS => "mongodb://localhost:30002/?ReadPreference=secondary";
        public static string DB_ADDRESS => "mongodb://localhost:30001";
        //public static string DB_ADDRESS => "mongodb://localhost:30002/?ReplicaSetName=my-replica-set";
        //public static string DB_ADDRESS => "mongodb://localhost:30001,localhost:30002?connect=replicaSet";
        public static string DB_NAME => "test";
        public static string USERS_COLLECTION => "users";
        public static string CHARACTERS_COLLECTION =>  "characters";
        public static string VIEWS_COLLECTION =>  "views";
    }
}
