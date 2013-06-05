using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicTales.Entities
{
    public class ComicTile
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("order")]
        public int Order { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("overlays")]
        public List<TileOverlay> Overlays { get; set; }

        #region Ctor

        public ComicTile()
        {
            Overlays = new List<TileOverlay>();
        }

        #endregion
    }
}