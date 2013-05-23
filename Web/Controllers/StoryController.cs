using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MongoDB.Driver.Builders;

namespace ComicTales.Controllers
{
    public class StoryController : Controller
    {
        //
        // GET: /Story/<id>
        [HttpGet]
        public ActionResult Index(string id)
        {
            return View();
        }


        //
        // GET: /Story/<id>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            dynamic model = new ExpandoObject();

            model.StoryId = id;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveTile(string id, string tileId)
        {
            //do save 

            var context = GlobalHost.ConnectionManager.GetHubContext<StoryNotifications>();
            context.Clients.Group(id).notifyHasUpdates();

            return Json(new {status = "OK"});
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
