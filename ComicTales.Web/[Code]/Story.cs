using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicTales
{
    public class Story
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("token")]
        public string Token { get; set; }

        [BsonElement("tiles")]
        public List<Tile> Tiles { get; set; } 
        
    }

    public class Tile
    {
        [BsonElement("id")]
        public string TileId { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("overlays")]
        public List<Overlay> Overlays { get; set; } 

    }

    public class Overlay
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("x")]
        public int X { get; set; }

        [BsonElement("y")]
        public int Y { get; set; }
        
        [BsonElement("width")]
        public int Width { get; set; }
        
        [BsonElement("height")]
        public int Height { get; set; }
    }
}