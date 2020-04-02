using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLibrary.DataAccess;
using DataLibrary.Models;



namespace DataLibrary.BusinessLogic
{
    public class StudentProcessor
    {

        public static int CreateStudent(int iId, string sName, string sAddress, int iAge, string sStandard, decimal dPercent,
            DateTime dAddedOn, bool bStatus)
        {
            DataLibrary.Models.StudentModel data = new DataLibrary.Models.StudentModel
            {
                id = iId,
                name = sName,
                address = sAddress,
                age = iAge,
                standard = sStandard,
                percent = dPercent,
                addedOn = dAddedOn,
                status = bStatus
            };

            string sql = @"insert into dbo.Student ([name], [address], [age], [standard], [percent], [status]) 
                            values (@name,  @address,  @age,  @standard, @percent, @status);";

            return SqlDataAccess.SaveData(sql, data);
        }


        public static List<StudentModel> LoadStudents()
        {
            string sql = @"select  [id], [name], [address], [age], [standard], [percent], [addedOn], [status] from dbo.Student;";

            return SqlDataAccess.LoadData<StudentModel>(sql);
        }


        public static List<StudentModel> LoadOneStudent(int iId)
        {
            //string sql = @"select id, name,  address,  age,  standard, percent, addedOn, status from dbo.Student where id=" + iId + ";";
            string sql = @"select  [id], [name], [address], [age], [standard], [percent], [addedOn], [status] from dbo.Student where id=" + iId + ";";

            return SqlDataAccess.LoadData<StudentModel>(sql);
        }




        public static int UpdateStudent(int iId, string sName, string sAddress, int iAge, string sStandard, decimal dPercent,
            DateTime dAddedOn, bool bStatus)
        {
            DataLibrary.Models.StudentModel data = new DataLibrary.Models.StudentModel
            {
                name = sName,
                address = sAddress,
                age = iAge,
                standard = sStandard,
                percent = dPercent,
                status = bStatus
            };

            string sqlU = @"update dbo.Student set [name]=@name, [address]=@address, [age]=@age, [standard]=@standard, [percent]=@percent, [status]=@status  
                            where Id=" + iId + ";";

            return SqlDataAccess.SaveData(sqlU, data);
        }


        public static int DeleteStudent(int iId)
        {
            string sqlU = @"delete from dbo.student where Id=" + iId + ";";

            return SqlDataAccess.DeleteData(sqlU);
        }

        public static int CountStudents()
        {
            string sqlU = @"select count(*) as count from dbo.student;";

            return SqlDataAccess.CountData(sqlU);
        }


        public static List<StudentModel> LoadStudentsSearch(int PageNo, int PageSize, string SearchFor)
        {
            //string sql = @"select id, name,  address,  age,  standard, percent, addedOn, status from dbo.Student;";
            string sql = @"select [id], [name], [address], [age], [standard], [percent], [addedOn], [status] from dbo.Student;";

            if (SearchFor == "")
            {
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,* " +
                    "FROM dbo.Student WHERE 1 = 1 )  a WHERE a.RowNum " +
                    "BETWEEN(CAST(" + PageNo + " AS int) -1)* CAST(" + PageSize + " AS int) " +
                    "+1 AND (CAST(" + PageNo + " AS int)*     CAST(" + PageSize + " AS int)); ";
            }
            else
            {
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,* " +
                    "FROM dbo.Student WHERE " + SearchFor + " )  a WHERE a.RowNum " +
                    "BETWEEN(CAST(" + PageNo + " AS int) -1)* CAST(" + PageSize + " AS int) " +
                    "+1 AND (CAST(" + PageNo + " AS int)*     CAST(" + PageSize + " AS int)); ";
            }

            return SqlDataAccess.LoadData<StudentModel>(sql);
        }


    }
}
