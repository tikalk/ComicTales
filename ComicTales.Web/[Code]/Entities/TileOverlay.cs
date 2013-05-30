using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComicTales.Entities
{
    public class TileOverlay
    {
        [BsonElement("type")]
        public TileOverlayTypes Type { get; set; }

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