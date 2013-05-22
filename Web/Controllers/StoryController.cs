using System;
using System.Collections.Generic;
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
            return View();
        }

    }
}
