using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ComicTales.Entities
{
    public class ComicStory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [BsonElement("name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [BsonElement("tiles")]
        [JsonProperty(PropertyName = "tiles")]
        public List<ComicTile> Tiles { get; set; }

        #region Ctor

        public ComicStory()
        {
            Tiles = new List<ComicTile>();
        }

        #endregion
    }
}