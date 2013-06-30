using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MongoDB.Driver.Builders;
using ComicTales.Entities;
using MongoDB.Bson;
using Newtonsoft.Json.Bson;
using MongoDB.Bson.Serialization;
using ComicTales.MVC;
using Microsoft.AspNet.SignalR.Hubs;
using System.IO;

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
        public JsonNetResult Create(ComicStory comicStory)
        {
            //reset the Id
            comicStory.Id = null;

            try
            {
                _mongoRepositiry.SaveComicStory(comicStory);
            }
            catch (FormatException)
            {
                throw new ArgumentException("The story could not be created");
            }

            //ActionResult
            JsonNetResult jsonNetResult = new JsonNetResult();
            jsonNetResult.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonNetResult.Data = comicStory;
            return jsonNetResult;
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

        //static string path = @"C:\work\ComicTales\ComicTales.Web\Upload\";
        //
        // GET: /Story/<id>
        [HttpPost]
        public ActionResult SaveSnapshot(string id, string dataURL)
        {
            string fileNameWitPath = string.Empty;
            string pathSuffix = string.Empty;
            string tick = DateTime.Now.Ticks.ToString();
            if (dataURL.Contains("jpeg"))
            {
                dataURL = dataURL.Remove(0, 23);
                pathSuffix = ".jpeg";
                
            }
            else if (dataURL.Contains("png"))
            {
                dataURL = dataURL.Remove(0, 22);
                pathSuffix = ".png";
            }
            var path = Request.MapPath("~/Upload");
            fileNameWitPath = Path.Combine( path, tick + pathSuffix);
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] snapshotData = Convert.FromBase64String(dataURL);
                    bw.Write(snapshotData);
                    bw.Close();

                }
            }

            return Json(tick + pathSuffix, JsonRequestBehavior.AllowGet);
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
            //var context = GlobalHost.ConnectionManager.GetHubContext<StoryNotificationsHub>();
            //context.Clients.Group(comicStory.Id).notifyHasUpdates();

            //return the Id in case the story is new
            return Json(new { status = "OK", comicStoryId = comicStory.Id });
        }

        [HttpPost]
        public ActionResult AddTile(string id, string imageUrl)
        {
            var tile = new ComicTile { Image = imageUrl };
            var story = _mongoRepositiry.GetStoryById(id);
            story.Tiles.Add(tile);
            _mongoRepositiry.SaveComicStory(story);

            return Json(new { });
        }

        [HttpPost]
        public ActionResult UploadFile(string id, int resumableChunkNumber, int resumableChunkSize, int resumableTotalSize, string resumableIdentifier, string resumableFilename, string resumableRelativePath)
        {
            string fileName = resumableIdentifier + "_" + resumableFilename;
            string filePath = Path.Combine(this.Server.MapPath("/Upload"), fileName);

            var stream = Request.Files[0].InputStream;

            var file = new FileInfo(filePath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            int offset = (resumableChunkNumber - 1) * resumableChunkSize;

            using (var fileStream = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fileStream.Seek(offset, SeekOrigin.Begin);

                var buffer = new byte[1024];

                for (; ; )
                {
                    var read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        break;

                    fileStream.Write(buffer, 0, read);
                    fileStream.Flush();
                }
            }

            string ex = string.Format("Igor is hearting me!!!\r\n" +
                "id: {0}\r\n" +
                "resumableChunkNumber: {1}\r\n" +
                "resumableChunkSize: {2}\r\n" +
                "resumableFilename: {3}\r\n" +
                "resumableIdentifier: {4}\r\n" +
                "resumableRelativePath: {5}\r\n" +
                "resumableTotalSize: {6}\r\n" +
                "filePath: {7}\r\n"
                , id, resumableChunkNumber, resumableChunkSize, resumableFilename,
                resumableIdentifier, resumableRelativePath, resumableTotalSize, filePath);
            //throw new Exception(ex);

            return Json(new { imageUrl = fileName, });
        }
    }
}
