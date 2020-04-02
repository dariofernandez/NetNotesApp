using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NetNotes.Models;
using NetNotes.Class;
using System.Dynamic;


namespace NetNotes.Controllers
{
    public class PatientSelectionController : Controller
    {
        // GET: PatientSelection
        public ActionResult Index(int? patientId, int? visitId, int? page, string parentController = "", string searchString = "")
        {
            var model = new PatientSelectionModel();
            model.ControllerInvoking = "PatientSelection";

            int patientID = 0;

            int idFound = 0;
            if (searchString != "")
            {
                idFound = BindPatient(Convert.ToInt32(page), searchString, model.PageSize);
                if (idFound > 0) patientID = idFound;
            } else
            {
                BindPatient(Convert.ToInt32(page), "", model.PageSize);
            }

            model.PatientSelectList = ViewBag.PatientList;

            return View(model);
        }


        public int BindPatient(int page, string searchString = "", int pageSizeInModel=20)
        {
            int idFound = 0;
            if (true)
            {
                string SearchFor = searchString;
                SearchFor = " Last_Name LIKE '%" + SearchFor + "%' ";
                int pageSize = pageSizeInModel;
                int pageNo = page == 0 ? 1 : page;
                int iRecordCount = DataLibrary.BusinessLogic.PatientProcessor.CountRecords();

                var data = DataLibrary.BusinessLogic.PatientProcessor.LoadPatientsSearch(pageNo, pageSize, SearchFor);

                List<SelectListItem> listPatients = new List<SelectListItem>();

                foreach (var row in data)
                {
                    listPatients.Add(new SelectListItem
                    {
                        Text = row.Last_Name + ", " + row.First_Name,
                        Value = row.ID.ToString()
                    });
                }

                if (listPatients.Count > 0) idFound = Convert.ToInt32(listPatients[0].Value);

                ViewBag.PatientList = listPatients;
                ViewData["currentFilter"] = searchString;

                PagingInfo pagingInfo = new PagingInfo();
                pagingInfo.CurrentPage = pageNo;
                pagingInfo.TotalItems = iRecordCount;   /*context.Students.Count();*/
                pagingInfo.ItemsPerPage = pageSize;
                ViewBag.Paging = pagingInfo;

            }
            return idFound;
        }

    }
}