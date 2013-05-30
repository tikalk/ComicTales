using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComicTales.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace ComicTales
{
    public class MongoDBRepository
    {
        private readonly MongoDatabase _database;
        private readonly MongoCollection<ComicStory> _comicStoryCollection;
        private readonly IQueryable<ComicStory> _comicStoryQuery;

        public MongoDBRepository(MongoDatabase database)
        {
            _database = database;
            _comicStoryCollection = _database.GetCollection<ComicStory>("ComicStories");
            _comicStoryQuery = _comicStoryCollection.AsQueryable();
        }

        public ComicStory GetStoryById(string id)
        {
            var res = _comicStoryQuery.FirstOrDefault(e => e.Id == id);
            
            //or use the equivalant
            //var query = Query<ComicStory>.EQ(x => x.Token, storyToken);

            return res;
        }

        public void SaveComicStory(ComicStory comicStory)
        {
            #region Testing
            //if (comicStory == null) //just for testing 
            //{
            //    comicStory = new ComicStory()
            //    {
            //        Name = "Menny Story 1",
            //    };

            //    var tile = new ComicTile()
            //    {
            //        Name = "first tile",
            //        Image = "encoded image",
            //    };
            //    comicStory.Tiles.Add(tile);

            //    tile.Overlays.Add(new TileOverlay()
            //    {
            //        Height = 20,
            //        Text = "hello",
            //        Width = 2,
            //        X = 1,
            //        Y = 1,
            //        Type = TileOverlayTypes.Baloon1,
            //    });
            //}
            #endregion

            //Insert or update according to ComicStory.Id property
            _comicStoryCollection.Save(comicStory);
        }

        //public void SaveTile(string storyId, ComicTile tile)
        //{
        //    var query = Query.And(Query.EQ("Id", storyId), Query.EQ("Tiles.Id", tileId));
        //    var update = Update.Set("Tiles.$.Creator", "Jack");
        //    _comicStoryCollection.Update(query, update);
        //}
    }
}