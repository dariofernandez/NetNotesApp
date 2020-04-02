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
    public class BillingCart
    {

        public delegate void RetrieveTotal(decimal total);

        public List<PatientVisitsBillingModel> BillingItems { get; set; } = new List<PatientVisitsBillingModel>();


 
        public List<T> RetrieveBillingItemsTotal<T>(int iVisitId, RetrieveTotal remoteGrandTotal,
            Func<List<PatientVisitsBillingModel>, decimal, List<T>> retrieveBillingTotal)
        {
            BillingItems = GetPatientVisitBilling(iVisitId);

            decimal total = BillingItems.Sum(x => x.Units * x.Unit_Amount);

            remoteGrandTotal(total);

            return retrieveBillingTotal(BillingItems, total);
        }



        public static List<PatientVisitsBillingModel> GetPatientVisitBilling(int iVisitId)
        {
            var p = new DynamicParameters();
            p.Add("@lngVisitID", iVisitId);
            //p.Add("@b", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //p.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            //Guid guVisitID = ToGuid(iVisitId);
            return SqlDataAccessProd.LoadSP2<PatientVisitsBillingModel>("spGetPatientVisitBilling70_NET", p);
        }


        public static PatientVisitsBillingModel GetOneItem(int iId)
        {
            string sql = @"select ID, Billing_ID, Billing_Code as Code, Units, Unit_Amount from dbo.Patient_Visit_Billings where ID=" + iId + ";";
            return SqlDataAccessProd.LoadOneRecord<PatientVisitsBillingModel>(sql);
        }

        public static string GetOneBillingCode(int iId)
        {
            string sql = @"select Code from dbo.Billing_Codes where ID=" + iId + ";";
            return SqlDataAccessProd.LoadOneRecord<string>(sql);
        }


        ////public class PatientVisitsBillingModel
        ////{
        ////    public short Units { get; set; }
        ////    public decimal Unit_Amount { get; set; }

        ////}
        public static int UpdateRecord(int iId, int Billing_ID, string Code, short Units, Decimal Unit_Amount)
        {
            PatientVisitsBillingModel data = new PatientVisitsBillingModel
            {
                ID = iId,
                Billing_ID = Billing_ID,
                Code = Code,
                Units = Units,
                Unit_Amount = Unit_Amount
            };

            string sqlU = @"update dbo.Patient_Visit_Billings set Billing_ID=@Billing_ID, Billing_Code=@Code, Units=@Units, 
                            Unit_Amount=@Unit_Amount  where ID=@ID; ";

            return SqlDataAccessProd.SaveData(sqlU, data);
        }

    }
}
