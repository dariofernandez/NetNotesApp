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
    public class PatientProcessor
    {

        //public int ID { get; set; }
        //public bool Active { get; set; }
        //public string First_Name { get; set; }
        //public string Middle_Name { get; set; }
        //public string Last_Name { get; set; }
        //public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        //public string Home_Phone { get; set; }
        //public string Work_Phone { get; set; }
        //public string Alternate_Phone { get; set; }
        //public string Sex { get; set; }
        //public string Directions { get; set; }
        //public string SSN { get; set; }
        //public string Address1 { get; set; }
        //public string Address2 { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Zip { get; set; }
        //public int LanguageID { get; set; }
        //public string Email { get; set; }

        public static int CreateNew(string firstName, string middleName, string lastName, System.DateTime dob,
            string homePhone, string workPhone, string altPhone, string sex, string directions, string ssn, string address1, string address2,
            string city, string state, string zip, int language, string email)
        {
            PatientModel data = new PatientModel
            {
                First_Name = firstName,
                Middle_Name = middleName,
                Last_Name = lastName,
                Date_Of_Birth = dob,
                Home_Phone = homePhone,
                Work_Phone = workPhone,
                Alternate_Phone = altPhone,
                Sex = sex,
                Directions = directions,
                SSN = ssn,
                Address1 = address1,
                Address2 = address2,
                City = city,
                State = state,
                Zip = zip,
                LanguageID = language,
                Email = email
            };

            string sql = @"insert into dbo.Patients (active, First_Name, Middle_Name, Last_Name, Date_Of_Birth, Home_Phone, Work_Phone,
                           Alternate_Phone, Sex, Directions, SSN, Address1, Address2, City, State, Zip, LanguageID, Email) 
                            values (1, @First_Name, @Middle_Name, @Last_Name,  @Date_Of_Birth, @Home_Phone, @Work_Phone,
                           @Alternate_Phone, @Sex, @Directions, @SSN, @Address1, @Address2, @City, @State, @Zip, @LanguageID, @Email);";

            return SqlDataAccessProd.SaveData(sql, data);
        }


        public static List<PatientModel> LoadAllRecords()
        {
            string sql = @"select * from dbo.Patients;";

            return SqlDataAccessProd.LoadData<PatientModel>(sql);
        }


        public static List<PatientModel> LoadPatientsSearch(int PageNo, int PageSize, string SearchFor)
        {
            string sql = @"select * from dbo.Patients;";

            if (SearchFor == "")
            {
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,* " +
                    "FROM dbo.Patients WHERE 1 = 1 )  a WHERE a.RowNum " +
                    "BETWEEN(CAST(" + PageNo + " AS int) -1)* CAST(" + PageSize + " AS int) " +
                    "+1 AND (CAST(" + PageNo + " AS int)*     CAST(" + PageSize + " AS int)); ";
            }
            else
            {
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,* " +
                    "FROM dbo.Patients WHERE " + SearchFor + " )  a WHERE a.RowNum " +
                    "BETWEEN(CAST(" + PageNo + " AS int) -1)* CAST(" + PageSize + " AS int) " +
                    "+1 AND (CAST(" + PageNo + " AS int)*     CAST(" + PageSize + " AS int)); ";
            }

            return SqlDataAccessProd.LoadData<PatientModel>(sql);
        }


        public static int CountRecords()
        {
            string sqlU = @"select count(*) as count from dbo.Patients;";

            return SqlDataAccessProd.CountData(sqlU);
        }


        public static List<PatientVisitsModel> LoadAllVisitsForPatient(int iId)
        {
            string sql = @"select VisitDate, ID from dbo.Patient_Visits WHERE Problem_ID=" + iId +" ORDER BY VisitDate DESC;";

            return SqlDataAccessProd.LoadData<PatientVisitsModel>(sql);
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


        public static List<PatientModel> LoadOneRecord(int iId)
        {
            string sql = @"select * from dbo.Patients where ID=" + iId + ";";

            return SqlDataAccessProd.LoadData<PatientModel>(sql);
        }




        public static int UpdateRecord(int iId, string firstName, string middleName, string lastName, DateTime dob,
            string homePhone, string workPhone, string altPhone, string sex, string directions, string ssn, string address1, string address2,
            string city, string state, string zip, int language, string email)
        {
            // remove the time part
            var dob_date = dob.Date;

            PatientModel data = new PatientModel
            {
                ID = iId,
                First_Name = firstName,
                Middle_Name = middleName,
                Last_Name = lastName,
                Date_Of_Birth = dob_date,
                Home_Phone = homePhone,
                Work_Phone = workPhone,
                Alternate_Phone = altPhone,
                Sex = sex,
                Directions = directions,
                SSN = ssn,
                Address1 = address1,
                Address2 = address2,
                City = city,
                State = state,
                Zip = zip,
                LanguageID = language,
                Email = email
            };

            string sqlU = @"update dbo.Patients set First_Name=@First_Name, Middle_Name=@Middle_Name, Last_Name=@Last_Name, 
                            Date_Of_Birth=@Date_Of_Birth, Home_Phone=@Home_Phone, Work_Phone=@Work_Phone, Alternate_Phone=@Alternate_Phone, Sex=@Sex, 
                            Directions=@Directions, SSN=@SSN, Address1=@Address1, Address2=@Address2, City=@City, State=@State, 
                            Zip=@Zip, LanguageID=@LanguageID, Email=@Email 
                        where ID=@ID; ";

            return SqlDataAccessProd.SaveData(sqlU, data);
        }


        public static int DeleteRecord(int iId)
        {
            string sqlU = @"delete from dbo.Patients where ID=" + iId + ";";

            return SqlDataAccessProd.DeleteData(sqlU);
        }


        public static List<LanguageModel> LoadLanguages()
        {
            string sql = @"select * from dbo.Language;";

            return SqlDataAccessProd.LoadData<LanguageModel>(sql);
        }

    }
}
