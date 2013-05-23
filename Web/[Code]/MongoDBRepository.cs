using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ComicTales
{
    public class MongoDBRepository
    {
        private readonly MongoDatabase _database;

        static MongoDBRepository()
        {
            BsonClassMap.RegisterClassMap<Story>();
            BsonClassMap.RegisterClassMap<Tile>();
        }


        public MongoDBRepository(MongoDatabase database)
        {
            _database = database;
        }

        public Story GetStoryByToken(string storyToken)
        {
            var collection = MongoDBConnector.Database.GetCollection<Story>("stories");
            var query = Query<Story>.EQ(x => x.Token, storyToken);

            return collection.FindOne(query);

        }
    }
}