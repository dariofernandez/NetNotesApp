using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NetNotes.Models
{
    public class ViewNotesModel
    {
        public bool PatientWasSelected { get; set; } = false;
        public bool VisitWasSelected { get; set; } = false;
        public string ControllerInvoking { get; set; }

        public int PatientID { get; set; }
        public int ProblemID { get; set; }
        public int VisitID { get; set; }

        [Display(Name = "Patient")]
        public string Name { get; set; }

        // This property will hold a state, selected by user
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }

        public string Patient_Number { get; set; }
        public string MasterPatientNumber { get; set; }

        public IEnumerable<SelectListItem> VisitsSelectList { get; set; }

        [Display(Name = "Visit Date")]
        [DataType(DataType.Date)]
        public DateTime? VisitDate { get; set; }


        //public DateTime? visitDate { get; set; }

        [Display(Name = "Resource(s)")]
        public IEnumerable<SelectListItem> ResourceList { get; set; }

        public string SubjectiveText { get; set; }
        public string ObjectiveText { get; set; }
        public string AssessmentText { get; set; }
        public string PlanText { get; set; }
        public int Duration { get; set; }
        public int Resource_ID { get; set; }
        public bool Cancellation { get; set; }
        public bool NoShow { get; set; }

    }


    // this class is based on the query to retrieve work records for a visit
    //    the last 2 classes were retrieved from SQL using [sp__ModelFromTable] in SunshineNew
    public class VisitWorkModel
    {
        // Exercises_ModalitiesModel  (see below)
        public DateTime VisitDate { get; set; }
        public string Comment { get; set; }
        public short? Exercise_Modality_Code { get; set; }
        public string Exercise_Modality_Description { get; set; }
        public bool Sets_Reps_Weight { get; set; }
        public bool Label { get; set; }
        // from Daily_Work_RecordsModel
        public short? Sets { get; set; }
        public short? Reps { get; set; }
        public float? Weight { get; set; }
        public short? Duration { get; set; }
        public bool On_Hold { get; set; }

    }



    public class Daily_Work_RecordsModel
    {
        public int Visit_ID { get; set; }
        public short? Exercise_Modality_Code { get; set; }
        public string Comment { get; set; }
        public short? Sets { get; set; }
        public short? Reps { get; set; }
        public float? Weight { get; set; }
        public short? Duration { get; set; }
        public bool On_Hold { get; set; }
    }


    public class Exercises_ModalitiesModel
    {
        public short Exercise_Modality_Code { get; set; }

        public bool Exercise { get; set; }

        public string Exercise_Modality_Description { get; set; }

        public bool Sets_Reps_Weight { get; set; }

        public string Site_Code { get; set; }

        public bool Inactive { get; set; }

        public int Discipline_ID { get; set; }

        public short? SortOrder { get; set; }

        public string ExerciseNumber { get; set; }

        public bool Label { get; set; }

        public int Assigned_BillingCodeID { get; set; }

    }


}
