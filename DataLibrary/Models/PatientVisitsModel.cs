using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PatientVisitsModel
    {
        public int ID { get; set; }
        public int Problem_ID { get; set; }
        public int Resource_ID { get; set; }
        public System.DateTime VisitDate { get; set; }
        public System.DateTime Time_In { get; set; }
        public Nullable<System.DateTime> Time_Out { get; set; }
        public bool Flash_Next_Physician_Appointment_Date { get; set; }
        public bool Flash_Prescription_Expiration_Date { get; set; }
        public bool Flash_Next_Test_Date { get; set; }
        public string Message { get; set; }
        public bool Cancellation { get; set; }
        public bool No_Show { get; set; }
        public bool NoteStation_Notified { get; set; }
        public string AssessmentText { get; set; }
        public string PlanText { get; set; }
        public Nullable<float> Duration { get; set; }
        public int SiteID { get; set; }
        public bool Flash_Next_Phy_Appt_Date { get; set; }
        public bool Flash_Presc_Exp_Date { get; set; }
        public bool EvalVisit { get; set; }
        public Nullable<int> PTPlan { get; set; }
        public Nullable<int> PTActual { get; set; }
        public Nullable<int> OTPlan { get; set; }
        public Nullable<int> OTActual { get; set; }
        public Nullable<int> STPlan { get; set; }
        public Nullable<int> STActual { get; set; }
        public string StatusComment { get; set; }
        public int StatusID { get; set; }
        public Nullable<bool> Copay_Paid { get; set; }
        public Nullable<System.DateTime> Start_Time { get; set; }
        public Nullable<System.DateTime> End_time { get; set; }
        public Nullable<System.DateTime> Visit_End_Date { get; set; }
        public Nullable<System.DateTime> Last_Status_Update_Date { get; set; }
        public bool Medicare { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
        public bool Group1 { get; set; }
        public bool Group2 { get; set; }
        public bool Group3 { get; set; }
        public bool Invoiced { get; set; }
        public bool Batched { get; set; }
        public Nullable<System.DateTime> LastPhysVisit { get; set; }
        public Nullable<System.DateTime> ExportTimeStamp { get; set; }
        public string MedicaidResubCode { get; set; }
        public string MedicaidOrigRefNo { get; set; }
        public string PriorAuthNo { get; set; }
        public bool OutsideLab { get; set; }
        public decimal OutsideLabCharges { get; set; }
        public decimal Visit_Total_Amount { get; set; }
        public int Supervisor_ID { get; set; }
        public short Supervised { get; set; }
        public Nullable<System.DateTime> Supervised_DateTime { get; set; }

    }
}
