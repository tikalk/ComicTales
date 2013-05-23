using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.Builders;

namespace ComicTales.Controllers
{
    public class StoryController : Controller
    {
        //
        // GET: /Story/<id>

        public ActionResult Index(string id)
        {
            return View();
        }


        //
        // GET: /Story/<id>

        public ActionResult Edit(string id)
        {
            dynamic model = new ExpandoObject();

            model.StoryId = id;
            
            return View(model);
        }

        public ActionResult GetTiles(string id)
        {
            var storyToken = id;

            var repository = new MongoDBRepository(MongoDBConnector.Database);
            var story = repository.GetStoryByToken(storyToken);

            var data = new
                           {
                               tiles = (story.Tiles ?? new List<Tile>())
                                   .Select(tile =>
                                           new
                                               {
                                                   id = tile.TileId,
                                                   imageUrl = "/Upload/" + tile.Image,
                                               }
                                   ).ToArray(),
                           };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
