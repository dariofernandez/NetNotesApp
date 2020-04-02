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
    public class ViewNotesController : Controller
    {

        public ActionResult Index(int? patientId, int? visitId, int? page, int? iTabOption=1, string LeaveModalOpen="false", string searchString = "")
        {
            ViewBag.TabOption = (int)iTabOption;
            ViewBag.LeaveModalOpen = false;
            if (LeaveModalOpen == "true") ViewBag.LeaveModalOpen = true;

            var model = new ViewNotesModel();
            model.ControllerInvoking = "ViewNotes";  // for partial view 

            int patientID = 0;
            int visitID = 0;

            if (patientId != null)
            {
                patientID = (int)patientId;
                ViewBag.patientID = patientID;
                //ViewBag.patientName = patientName;
                model.PatientWasSelected = true;
                if (visitId != null)
                {
                    visitID = (int)visitId;
                    ViewBag.visitID = visitID;
                    model.VisitWasSelected = true;
                } else
                {
                    model.VisitsSelectList = GetVisitsList(patientID);
                }
            }

            return View(model);
        }


        [LogActionFilter]
        [HttpPost]
        public ActionResult Index(Models.ViewNotesModel model)
        {
            string sVisitID = Session["visitID"].ToString();
            int visitID = Convert.ToInt32(sVisitID);
            if (true)    //if (ModelState.IsValid)
            {
                if (visitID==0)
                {
                    //int iRecordsCreated = DataLibrary.BusinessLogic.ViewNotesProcessor.CreateStudent(0, student.name, student.address, student.age, student.standard,
                    //    student.percent, student.addedOn, student.status);
                }
                else
                {
                    DataLibrary.BusinessLogic.ViewNotesProcessor.UpdateNote(visitID, model.SubjectiveText, model.ObjectiveText, model.AssessmentText, model.PlanText,
                        model.Duration, model.Resource_ID, model.Cancellation, model.NoShow);

                }
                //TempData["Result"] = createdId.Value == null ? result.Value : result.Value + " New Student Id is " + createdId.Value;
            }
            return View(model);
        }



        //  This routine is called from _SelectPatient partial view
        [HttpGet]
        public ActionResult SelectedPatient(int? id, string name)
        {
            int patientID = 0;
            ViewBag.patientName = name;
            var model = new ViewNotesModel();
            if (id != null)
            {
                patientID = (int)id;
                Session["patientID"] = patientID;
                Session["patientName"] = name;
                model.PatientWasSelected = true;
                model.VisitWasSelected = false;
                model.Name = name;
            }
            ViewData["currentFilter"] = ""; //reset search filter
            return RedirectToAction("Index", "ViewNotes", new { patientId  = patientID, searchingFor = "" });
        }


        public List<SelectListItem> GetSelectList(string sTableName)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            var data = DataLibrary.BusinessLogic.ViewNotesProcessor.LoadSelectList(sTableName).ToList();

            foreach (var row in data)
            {
                if (row.Description == null)
                {
                    selectList.Add(new SelectListItem { Text = "Description missing for ID", Value = row.ID.ToString() });
                }
                else
                {
                    selectList.Add(new SelectListItem { Text = row.Description.ToString(), Value = row.ID.ToString() });
                }
            }
            return selectList;
        }


        [HttpGet]
        public ActionResult GetNotesPartialView(int? visitId=0)
        {
            int visitID =(int)visitId;
            List<SelectListItem> resources = GetSelectList("Resources");
            ViewNotesModel model = new ViewNotesModel();
            model = GetVisitNotes(visitID);
            model.ResourceList = resources;
            return PartialView("GetNotesPartialView", model);
        }


        [HttpGet]
        public ActionResult GetBillingPartialViewOneItem(int? iId=0)
        {
            int iID = (int)iId;
            bool bModelRetrieved = false;
            List<SelectListItem> billingCodes = GetSelectList("Billing_Codes");

            PatientVisitsBillingModel model = new PatientVisitsBillingModel();

            if (iID > 0)
            {
                var row = DataLibrary.BusinessLogic.BillingCart.GetOneItem(iID);
                if (row != null)
                {
                    model.BillingCodesList = billingCodes;
                    model.ID = row.ID;
                    model.Description = row.Description;
                    model.Billing_ID = row.Billing_ID;
                    model.Code = row.Code;
                    model.Units = row.Units;
                    model.Unit_Amount = row.Unit_Amount;
                    bModelRetrieved = true;
                }
            }

            if (bModelRetrieved)
                return PartialView("GetBillingPartialViewOneItem", model);
            else
                return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult GetBillingPartialViewOneItem(PatientVisitsBillingModel model)
        {

            string sBillingCode = DataLibrary.BusinessLogic.BillingCart.GetOneBillingCode(model.Billing_ID);
            model.Code = sBillingCode;
            DataLibrary.BusinessLogic.BillingCart.UpdateRecord(model.ID, model.Billing_ID, model.Code, model.Units, model.Unit_Amount);

            return RedirectToAction("Index", "ViewNotes", new { visitId = Session["visitID"], iTabOption = 5});
         }



        ///private readonly List<VisitWorkModel> listExercises;

        [HttpGet]
        public ActionResult GetWorkPartialView(int? visitId = 0, int? page=1)
        {
            int visitID = (int)visitId;
            if (visitID == 0)
            {
                return RedirectToAction("Index");
            }
            if (page == 1)
            {
                List<VisitWorkModel> listExercises = new List<VisitWorkModel>();

                var data = DataLibrary.BusinessLogic.ViewNotesProcessor.LoadWorkForVisit(visitID).ToList();
                foreach (var row in data)
                {
                    listExercises.Add(new VisitWorkModel
                    {
                        VisitDate = row.VisitDate,
                        Comment = row.Comment,
                        Exercise_Modality_Code = row.Exercise_Modality_Code,
                        Exercise_Modality_Description = row.Exercise_Modality_Description,
                        Sets_Reps_Weight = row.Sets_Reps_Weight,
                        Label = row.Label,
                        Sets = row.Sets,
                        Reps = row.Reps,
                        Weight = row.Weight,
                        Duration = row.Duration,
                        On_Hold = row.On_Hold
                    });
                }
                return RedirectToAction("Index", "ViewNotes", listExercises);
                //return PartialView("GetWorkPartialView", listExercises.Take(5) );
            } else
            {
                return null;
            }
        }


        private void LocalGrandTotal(decimal total)
        {
            int here = 0;
        }

        private List<PatientVisitsBillingModel> LocalBillingTotal<T>(List<DataLibrary.Models.PatientVisitsBillingModel> BillingItems, decimal total)
        {
            List<PatientVisitsBillingModel> listBillingItems = new List<PatientVisitsBillingModel>();

            foreach (var row in BillingItems)
            {
                listBillingItems.Add(new PatientVisitsBillingModel
                {
                    ID = row.ID,
                    Code = row.Code,
                    Billing_ID = row.Billing_ID,
                    Description = row.Description,
                    Units = row.Units,
                    Unit_Amount = row.Unit_Amount
                });
            }

            return listBillingItems;
        }


        

        [HttpGet]
        public ActionResult GetBillingPartialView(int? visitId = 0, int? page = 1)
        {
            int visitID = (int)visitId;
            if (visitID == 0)
            {
                return RedirectToAction("Index");
            }
            if (page == 1)
            {
                List<PatientVisitsBillingModel> listBillingItems = new List<PatientVisitsBillingModel>();

                DataLibrary.BusinessLogic.BillingCart cart = new DataLibrary.BusinessLogic.BillingCart();

                listBillingItems = cart.RetrieveBillingItemsTotal< PatientVisitsBillingModel>(visitID, LocalGrandTotal, LocalBillingTotal<PatientVisitsBillingModel>);

                ViewBag.listBillingItems = listBillingItems;

                return PartialView("GetBillingPartialView", listBillingItems);
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public JsonResult GetBillingPartialViewJson(IEnumerable<PatientVisitsBillingModel> model)
        {
            List<PatientVisitsBillingModel> listBillingItems = new List<PatientVisitsBillingModel>();
            listBillingItems = model.ToList();

            return Json(listBillingItems, JsonRequestBehavior.AllowGet);
         }


        [HttpPost]
        public JsonResult GetOneBillingItemModel(int iID)
        {
            PatientVisitsBillingModel model = new PatientVisitsBillingModel();
            var row = DataLibrary.BusinessLogic.BillingCart.GetOneItem(iID);
            if (true)
            {
                model.ID = row.ID;
                model.Description = row.Description;
                model.Billing_ID = row.Billing_ID;
                model.Code = row.Code;
                model.Units = row.Units;
                model.Unit_Amount = row.Unit_Amount;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetResourceList()
        {
            var allResources = DataLibrary.BusinessLogic.ViewNotesProcessor.LoadSelectList("Resources").ToList();
            return Json(allResources, JsonRequestBehavior.AllowGet);
        }

 


        [HttpGet]
        public ActionResult SelectedVisit(int? visit_id, string visitdate)
        {
            int visitID = 0;
            int patientID = 0;
            patientID = (int)Session["patientID"];
            ViewBag.visitDate = visitdate;
            var model = new ViewNotesModel();
            model.ControllerInvoking = "ViewNotes";  // for partial view

            if (visit_id != null)
            {
                visitID = (int)visit_id;
                GetVisitDetails(visitID, model);
                Session["visitID"] = model.VisitID;
                Session["visitDate"] = model.VisitDate;
                Session["problemID"] = model.ProblemID;
                model.PatientWasSelected = true;
                model.VisitWasSelected = true;
            }
            return RedirectToAction("Index", "ViewNotes", new { patientId = patientID, visitId = visitID, searchingFor = "" });
        }



        [HttpPost]
        public ActionResult ClearSearch(Models.ViewNotesModel model, int? id)
        {
            Session["patientID"] = 0;
            Session["patientName"] = "";

            Session["visitID"] = 0;
            Session["visitDate"] = "";

            return RedirectToAction("Index", "ViewNotes");
        }



        [HttpPost]
        public JsonResult AjaxMethod(int ID, string name)
        {
            ViewNotesModel person = new ViewNotesModel
            {
                Name = name,
                PatientID = ID
                //DateTime = DateTime.Now.ToString()
            };
            return Json(person);
        }



        public List<SelectListItem> GetVisitsList(int patientID)
        {
            List<SelectListItem> listVisits = new List<SelectListItem>();
            var data = DataLibrary.BusinessLogic.ViewNotesProcessor.LoadAllVisitsForPatient(patientID);

            foreach (var row in data)
            {
                listVisits.Add(new SelectListItem
                {
                    Text = row.VisitDate.ToShortDateString(),
                    Value = row.ID.ToString()
                });
            }
            return listVisits;
        }


        public void GetVisitDetails(int visitID, ViewNotesModel model)
        {
            //ViewNotesModel model = new ViewNotesModel();
            if (visitID > 0)
            {
                var row = DataLibrary.BusinessLogic.ViewNotesProcessor.GetVisitDetails(visitID);
                if (row != null)
                {
                    model.VisitDate = row.visitDate;
                    model.ProblemID = row.ProblemID;
                    model.VisitID = row.VisitID;
                }
            } else
            {
                RedirectToAction("Index", "ViewNotes");
            }
        }


        public ViewNotesModel GetVisitNotes(int visitID)
        {
            ViewNotesModel visitNotes = new ViewNotesModel();
            var data = DataLibrary.BusinessLogic.ViewNotesProcessor.spGetPatientVisit70(visitID);

            foreach (var row in data)
            {
                visitNotes.SubjectiveText = row.SubjectiveText;
                visitNotes.ObjectiveText = row.ObjectiveText;
                visitNotes.AssessmentText = row.AssessmentText;
                visitNotes.PlanText = row.PlanText;
                visitNotes.Duration = row.Duration;
                visitNotes.Resource_ID = row.Resource_ID;
                visitNotes.Cancellation = row.Cancellation;
                visitNotes.NoShow = row.NoShow;
            }
            // Now add the other details
            GetVisitDetails(visitID, visitNotes);

            return visitNotes;
        }

    }
}


//try
//{
//    visitDate = Session["visitDate"].ToString();
//    model.VisitDate = Convert.ToDateTime(visitDate);  //Convert.ToDateTime("2020-03-13");

//    model.VisitDate = DateTime.ParseExact(visitDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
//}
//catch (Exception ex)
//{
//    View("Error", new HandleErrorInfo(ex, "VisitNotes", "GetNotesPartialView"));
//    //return RedirectToAction("Index");
//}
