using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NetNotes.Models;
using NetNotes.Class;


namespace NetNotes.Controllers
{
    public class StudentController : Controller
    {

        public ActionResult Index(int? id, int? page, string searchingFor = "")
        {
            int idFound = 0;
            if (searchingFor != "")
            {
                idFound=BindStudent(Convert.ToInt32(page), searchingFor);
                if (idFound > 0) id = idFound;
            }
            else
            {
                BindStudent(Convert.ToInt32(page));
            }


            //Models.Student mStudent = new Models.Student();
            StudentModel mStudent = new StudentModel();

            if (id != null)
            {
                int Iid = (int)id;
                var data = DataLibrary.BusinessLogic.StudentProcessor.LoadOneStudent(Iid);

                List<StudentModel> students = new List<StudentModel>();

                foreach (var row in data)
                {
                    students.Add(new StudentModel
                    {
                        id = row.id,
                        name = row.name,
                        address = row.address,
                        age = row.age,
                        standard = row.standard,
                        percent = row.percent,
                        addedOn = row.addedOn,
                        status = row.status
                    });
                }

                if (students != null)
                {
                    if (students.Count > 0)
                    {
                        mStudent = students[0];
                    }
                }


                ViewBag.Operation = "Update Student";
                return View(mStudent);
            }
            else
                ViewBag.Operation = "Insert Student";
            return View();
        }


        [LogActionFilter]
        [HttpPost]
        public ActionResult Index(Models.StudentModel student, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    int iRecordsCreated = DataLibrary.BusinessLogic.StudentProcessor.CreateStudent(0, student.name, student.address, student.age, student.standard,
                        student.percent, student.addedOn, student.status);
                }
                else
                {
                    DataLibrary.BusinessLogic.StudentProcessor.UpdateStudent(student.id, student.name, student.address, student.age, student.standard,
                        student.percent, student.addedOn, student.status);

                }
                //TempData["Result"] = createdId.Value == null ? result.Value : result.Value + " New Student Id is " + createdId.Value;

            }
            ViewBag.Operation = id == null ? "Add Student" : "Update Student";
            BindStudent(0);
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            DataLibrary.BusinessLogic.StudentProcessor.DeleteStudent(id);
            //TempData["Result"] = result.Value;
            BindStudent(0);
            return RedirectToAction("Index", "Student", new { searchingFor = "" });
            //return RedirectToRoute("Index");
        }




        //public ActionResult ViewStudents()
        //{
        //    ViewBag.Operation = "View Students";
        //    var data = DataLibrary.BusinessLogic.StudentProcessor.LoadStudents();
        //    List<StudentModel> students = new List<StudentModel>();
        //    foreach (var row in data)
        //    {
        //        students.Add(new StudentModel
        //        {
        //            id = row.id,
        //            name = row.name,
        //            address = row.address,
        //            age = row.age,
        //            standard = row.standard,
        //            percent = row.percent,
        //            addedOn = row.addedOn,
        //            status = row.status
        //        });
        //    }

        //    return View();
        //}


        [HttpPost]
        public ActionResult Search(string searchString = "")
        {
            ViewData["CurrentFilter"] = searchString;

            ViewBag.Operation = "Search Student";
            string strSearchFor = ViewData["currentFilter"].ToString();
            char[] trimChars = { '*', '@', ' ' };
            searchString = searchString.TrimEnd(trimChars);

            if (searchString != "")
            {
                strSearchFor = " name LIKE '%" + searchString + "%' ";
            }

            BindStudent(0, strSearchFor);
            //return RedirectToRoute("Index", new { searchingFor = strSearchFor });

            return RedirectToAction("Index", "Student", new { searchingFor = strSearchFor });
        }



        public int BindStudent(int page, string strSearchFor = "")
        {
            int idFound = 0;
            if (true)
            {
                string SearchFor = strSearchFor;
                int pageSize = 4;
                int pageNo = page == 0 ? 1 : page;

                int iRecordCount = DataLibrary.BusinessLogic.StudentProcessor.CountStudents();


                var data = DataLibrary.BusinessLogic.StudentProcessor.LoadStudentsSearch(pageNo, pageSize, SearchFor);

                List<StudentModel> students = new List<StudentModel>();
                foreach (var row in data)
                {
                    students.Add(new StudentModel
                    {
                        
                        id = row.id,
                        name = row.name,
                        address = row.address,
                        age = row.age,
                        standard = row.standard,
                        percent = row.percent,
                        addedOn = row.addedOn,
                        status = row.status
                    });
                };

                if (students.Count > 0) idFound = students[0].id;

               PagingInfo pagingInfo = new PagingInfo();
                pagingInfo.CurrentPage = pageNo;
                pagingInfo.TotalItems = iRecordCount;   /*context.Students.Count();*/
                pagingInfo.ItemsPerPage = pageSize;
                ViewBag.Paging = pagingInfo;

                ViewBag.StudentList = students;

            }
            return idFound;
        }

    }

}

