using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NetNotes.Models;
using NetNotes.Class;
using System.Dynamic;
using System.Globalization;
using System.Web.Helpers;

namespace NetNotes.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.VisitWorkModel model)
        {
            //May be you want to pass the posted model to the parial view?
            return View(model);
        }
    }
}