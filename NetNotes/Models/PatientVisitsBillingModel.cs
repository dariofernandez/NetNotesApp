using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NetNotes.Models;
using NetNotes.Class;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NetNotes.Models
{
    public class PatientVisitsBillingModel
    {
        public int ID { get; set; }
        public PagingInfo PagingInfo { get; set; }

        [Display(Name = "Billing Code(s)")]
        public IEnumerable<SelectListItem> BillingCodesList { get; set; }

        public string Description { get; set; }

        [Display(Name = "Bill. Description")]
        public int Billing_ID { get; set; }

        //[Required(ErrorMessage = "Billing Code is required")]
        public string Code { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public short Units { get; set; }

        [DisplayName("Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public decimal Unit_Amount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public decimal Total
        {
            get
            {
                return (Units * Unit_Amount);
            }
        }

    }


    public class Billing_Codes
    {
        public int ID { get; set; }

        public bool Active { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public decimal? Amount { get; set; }

        public string Type { get; set; }

        public string Quick_Code { get; set; }

        public short Sort_Order { get; set; }

        public string Insurance_IDs { get; set; }

        public int Discipline_ID { get; set; }

        public string Alternate_Description { get; set; }

        public bool ExcludeFromExport { get; set; }

        public short Charge_Units { get; set; }

        public bool MedBill { get; set; }

        public short Minutes { get; set; }

        public decimal Amount2 { get; set; }

        public decimal Amount3 { get; set; }

        public decimal Amount4 { get; set; }

        public bool Taxable { get; set; }

        public string Modifier1 { get; set; }

        public string Modifier2 { get; set; }

        public string Modifier3 { get; set; }

        public string Modifier4 { get; set; }

        public int Timed { get; set; }

        public int Limit { get; set; }

        public string Combined { get; set; }

        public bool Patient_Charge { get; set; }

    }


    public class Patient_Visit_Billings
    {
        public int ID { get; set; }

        public int Visit_ID { get; set; }

        public int Billing_ID { get; set; }

        public int Resource_ID { get; set; }

        public int Diagnosis_ID { get; set; }

        public int Classification_ID { get; set; }

        public short Units { get; set; }

        public short? SortOrder { get; set; }

        public DateTime? ExportTimeStamp { get; set; }

        public string Billing_Code { get; set; }

        public short? Minutes { get; set; }

        public decimal Unit_Amount { get; set; }

        public int ProblemID { get; set; }

        public int Insurance_ID { get; set; }

        public int Insurance_Order { get; set; }

        public bool Deleted { get; set; }

        public string Deleted_By { get; set; }

        public DateTime? Deleted_Date { get; set; }

        public bool Invoiced { get; set; }

        public int POS_ID { get; set; }

        public int TOS_ID { get; set; }

        public int Secondary_Diagnosis_ID { get; set; }

        public int Tertiary_Diagnosis_ID { get; set; }

        public int Quad_Diagnosis_ID { get; set; }

        public string Modifier1 { get; set; }

        public string Modifier2 { get; set; }

        public string Modifier3 { get; set; }

        public string Modifier4 { get; set; }

        public int Timed { get; set; }

        public int Limit { get; set; }

        public string Combined { get; set; }

        public int Diagnosis5_ID { get; set; }

        public int Diagnosis6_ID { get; set; }

        public int Diagnosis7_ID { get; set; }

        public int Diagnosis8_ID { get; set; }

        public int Diagnosis9_ID { get; set; }

        public int Diagnosis10_ID { get; set; }

        public int Diagnosis11_ID { get; set; }

        public int Diagnosis12_ID { get; set; }

    }

}

