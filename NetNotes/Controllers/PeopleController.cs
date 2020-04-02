using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NetNotes.Models;

namespace NetNotes.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListPeople()
        {
            List<PeopleModel> people = new List<PeopleModel>();
            people.Add(new PeopleModel { name = "Dar Fer", biography = "My bio", status = true });

            // return model
            return View(people);
        }
    }
}