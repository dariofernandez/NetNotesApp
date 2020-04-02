using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLibrary.DataAccess;
using DataLibrary.Models;

using Dapper;
using System.Data;


namespace DataLibrary.BusinessLogic
{
    public class ViewNotesProcessor
    {

        public static ViewNotesModel GetVisitDetails(int iVisitID)
        {
            string sql = @"select ID as VisitID, VisitDate, Problem_ID as ProblemID from dbo.Patient_Visits WHERE ID=" + iVisitID + ";";
            ViewNotesModel rec = SqlDataAccessProd.LoadRecord(sql);
            return rec;
        }


        public static List<PatientVisitsModel> LoadAllVisitsForPatient(int iId)
        {
            string sql = @"select VisitDate, ID from dbo.Patient_Visits WHERE Problem_ID=" + iId + " ORDER BY VisitDate DESC;";
            return SqlDataAccessProd.LoadData<PatientVisitsModel>(sql);
        }


        public static List<AnySelectListModel> LoadSelectList(string sTableName)
        {
            string sql = "";
            // Resources
            if (sTableName=="Resources")
            {
                sql = @"select ID, Active, Last_Name + ', ' + First_Name as Description from dbo." + sTableName + " WHERE NonResource=0 ORDER BY Last_Name, First_Name;";
            }
            else
            {
                if (sTableName == "Billing_Codes")
                {
                    sql = @"select ID, Description + ' (' + Code + ')' as Description from dbo." + sTableName + " WHERE Active=1 ORDER BY Description;";
                }
            }

            return SqlDataAccessProd.LoadData<AnySelectListModel>(sql);
        }


        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static List<ViewNotesModel> spGetPatientVisit70(int iVisitId)
        {
            var p = new DynamicParameters();
            p.Add("@lngVisitID", iVisitId);
            //p.Add("@b", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //p.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            //Guid guVisitID = ToGuid(iVisitId);
            return SqlDataAccessProd.LoadSP2<ViewNotesModel>("spGetPatientVisit70", p);
        }


        //public string SubjectiveText { get; set; }
        //public string ObjectiveText { get; set; }
        //public string AssessmentText { get; set; }
        //public string PlanText { get; set; }
        //public int Duration { get; set; }
        //public int Resource_ID { get; set; }
        //public bool Cancellation { get; set; }
        //public bool NoShow { get; set; }

        public static int UpdateNote(int iVisitId, string sSubjectiveText, string sObjectiveText, string sAssessmentText, string sPlanText, 
            int iDuration, int iResource_ID, bool bCancellation, bool bNoShow)
        {
            DataLibrary.Models.ViewNotesModel data = new DataLibrary.Models.ViewNotesModel
            {
                SubjectiveText = sSubjectiveText,
                ObjectiveText = sObjectiveText,
                AssessmentText = sAssessmentText,
                PlanText = sPlanText,
                Duration = iDuration,
                Resource_ID= iResource_ID,
                Cancellation = bCancellation,
                NoShow = bNoShow
            };

            string sqlU = @"update dbo.Patient_Visit_Notes set [SubjectiveText]=@SubjectiveText, [ObjectiveText]=@ObjectiveText 
                            where Visit_ID=" + iVisitId + ";";
            SqlDataAccessProd.SaveData(sqlU, data);

            sqlU = @"update dbo.Patient_Visits set [AssessmentText]=@AssessmentText, [PlanText]=@PlanText, [Cancellation]=@Cancellation, [No_Show]=@NoShow, [Duration]=@Duration, [Resource_ID]=@Resource_ID   
                            where ID=" + iVisitId + ";";

            return SqlDataAccessProd.SaveData(sqlU, data);
        }



        public static List<VisitWorkModel> LoadWorkForVisit(int iVisitId)
        {
            string sql = @"SELECT Patient_Visits.VisitDate, Daily_Work_Records.Comment, Daily_Work_Records.Exercise_Modality_Code, 
                Exercises_Modalities.Exercise_Modality_Description, Daily_Work_Records.Sets, Daily_Work_Records.Reps, 
                Daily_Work_Records.Weight, Daily_Work_Records.Duration, Daily_Work_Records.On_Hold, Exercises_Modalities.Sets_Reps_Weight, 
                Exercises_Modalities.Label 
                FROM Exercises_Modalities 
                INNER JOIN Patient_Visits INNER JOIN Daily_Work_Records ON Patient_Visits.ID = Daily_Work_Records.Visit_ID ON Exercises_Modalities.Exercise_Modality_Code = Daily_Work_Records.Exercise_Modality_Code LEFT OUTER JOIN Patient_Treatment_Plan ON Patient_Visits.Problem_ID = Patient_Treatment_Plan.Problem_ID AND Exercises_Modalities.Exercise_Modality_Code = Patient_Treatment_Plan.Exercise_Modality_Code 
                WHERE (Patient_Visits.ID=" + iVisitId + ") ORDER BY Patient_Treatment_Plan.Sort_Order;";

            return SqlDataAccessProd.LoadData<VisitWorkModel>(sql);
        }

    }
}
