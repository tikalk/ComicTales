using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicTales.Web.Controllers
{
    public class MockupsController : Controller
    {
        //
        // GET: /Mockups/

        public ActionResult Index()
        {
            return ViewComics();
        }

        public ActionResult ViewComics()
        {
            return View("ViewComics");
        }
    
        public ActionResult EditComics()
        {
            return View("EditComics");
        }

        public ActionResult CreateTile()
        {
            return View("CreateTile");
        }

        public ActionResult CreateTileCam()
        {
            return View("CreateTileCam");
        }

        public ActionResult CreateTileUpload()
        {
            return View("CreateTileUpload");
        }

        public ActionResult EditTile()
        {
            return View("EditTile");
        }
    }
}
