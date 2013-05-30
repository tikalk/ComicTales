using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicTales.Entities
{
    public class ComicStory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tiles")]
        public List<ComicTile> Tiles { get; set; }

        #region Ctor

        public ComicStory()
        {
            
            Tiles = new List<ComicTile>();
        }

        #endregion
    }
}