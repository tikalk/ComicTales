using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MongoDB.Driver.Builders;
using ComicTales.Entities;
using MongoDB.Bson;
using Newtonsoft.Json.Bson;
using MongoDB.Bson.Serialization;
using Microsoft.AspNet.SignalR.Hubs;

namespace ComicTales.Controllers
{
    public class StoryController : Controller
    {
        #region Private Fields

        private readonly MongoDBRepository _mongoRepositiry;

        #endregion

        #region Ctor

        public StoryController(MongoDBRepository mongoRepositiry)
        {
            _mongoRepositiry = mongoRepositiry;
        }

        #endregion

        //
        // GET: /Story/<id>
        [HttpGet]
        public ActionResult Index(string id)
        {
            //see if this is a new story
            if (string.IsNullOrEmpty(id))
            {
                return View(new ComicStory());
            }

            //fetch the data
            ComicStory comicStory;
            try
            {
                comicStory = _mongoRepositiry.GetStoryById(id);
            }
            catch (FormatException)
            {
                throw new ArgumentException("The story Id you have provided is invalid, or the story is corrupted");
            }
           
            //ensure data is valid
            if (comicStory == null)
            {
                throw new ArgumentException("The story you requested could not be found");
            }

            //return view
            return View(comicStory);
        }

        [HttpPost]
        public JsonResult Create()
        {
            ComicStory comicStory = new ComicStory();
            try
            {
                _mongoRepositiry.SaveComicStory(comicStory);
            }
            catch (FormatException)
            {
                throw new ArgumentException("The story could not be created");
            }

            return Json(new { id = comicStory.Id });
        }

        //
        // GET: /Story/<id>/Edit
        [HttpGet]
        public ActionResult Edit(string id)
        {
            dynamic model = new ExpandoObject();
            model.StoryId = id;

            return View(model);
        }

        //
        // GET: /Story/<id>/GetTiles
        [HttpGet]
        public ActionResult GetTiles(string id)
        {
            var story = _mongoRepositiry.GetStoryById(id);

            var data = new
                           {
                               tiles = (story.Tiles ?? new List<ComicTile>())
                                   .OrderBy(tile => tile.Order)
                                   .Select((tile) =>
                                           new
                                               {
                                                   idx = tile.Order,
                                                   imageUrl = "/Upload/" + tile.Image,
                                                   name = tile.Name,
                                               }
                                   ).ToArray(),
                           };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Story/<id>
        [HttpPost]
        public ActionResult Save(ComicStory comicStory)
        {
            //_mongoRepositiry.SaveComicStory(comicStory);

            //SignalR
            var context = GlobalHost.ConnectionManager.GetHubContext<StoryNotificationsHub>();
            context.Clients.Group(comicStory.Id).notifyHasUpdates();

            //return the Id in case the story is new
            return Json(new { status = "OK", comicStoryId = comicStory.Id });
        }
    }
}
