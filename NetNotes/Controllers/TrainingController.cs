using NetNotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetNotes.Controllers
{
    public class TrainingController : Controller
    {
        TrainingRepository _trainingRepository = new TrainingRepository();

        public ActionResult Index()
        {
            List<Training> allTrainings = _trainingRepository.GetTrainings().ToList();
            return View(allTrainings);
        }

        [HttpGet]
        public ActionResult CreatePartialView()
        {
            return PartialView("CreatePartialView");
        }

        [HttpPost]
        public void Create(Training training)
        {
            _trainingRepository.InsertTraining(training);
        }

        [HttpGet]
        public JsonResult GetInstructorList()
        {
            var allInstructors = _trainingRepository.GetInstructor().ToList();
            return Json(allInstructors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditPartialView(string id)
        {
            int selectedTrainingId = Convert.ToInt32(id);
            Training selectedTraining = _trainingRepository.GetTrainingByID(selectedTrainingId);

            return PartialView("EditPartialView", selectedTraining);
        }

        [HttpPost]
        public void Edit(Training training)
        {
            _trainingRepository.EditTraining(training);
        }

        public void Delete(string id)
        {
            int selectedTrainingId = Convert.ToInt32(id);
            _trainingRepository.DeleteTraining(selectedTrainingId);
        }
    }

}

