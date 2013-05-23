using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
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
            var data = new
                           {
                               tiles = new []
                                           {
                                               new
                                                   {
                                                       id = "abc",
                                                       imageUrl = "/Content/imgs/tile1.png",
                                                   }
                                           }
                           };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
