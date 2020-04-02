using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NetNotes.Models;
using NetNotes.Class;


namespace NetNotes.Models
{
    public class PatientSelectionModel
    {
        public bool PatientWasSelected { get; set; } = false;
        public string ControllerInvoking { get; set; }
        public int PageSize { get; set; } = 20;

        // This property will hold all available patients for selection (used in partial view _SelectPatient)
        public List<SelectListItem> PatientSelectList { get; set; }

    }
}