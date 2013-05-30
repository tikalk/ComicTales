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
        public string Name { get; set; }

        public int Order { get; set; }

        public string Image { get; set; }

        public List<TileOverlay> Overlays { get; set; }

        #region Ctor

        public ComicTile()
        {
            Overlays = new List<TileOverlay>();
        }

        #endregion
    }
}