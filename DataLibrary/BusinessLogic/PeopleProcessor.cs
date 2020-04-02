using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLibrary.DataAccess;
using DataLibrary.Models;


namespace DataLibrary.BusinessLogic
{
    public class PeopleProcessor
    {
        public static int CreateRecord(int iId, string sName, string sBiography, DateTime dAddedOn, bool bStatus)
        {
            DataLibrary.Models.PeopleModel data = new DataLibrary.Models.PeopleModel
            {
                id = iId,
                name = sName,
                biography = sBiography,
                addedOn = dAddedOn,
                status = bStatus
            };

            string sql = @"insert into dbo.People ([name], [biography], [status]) 
                            values (@name,  @biography, @status);";

            return SqlDataAccess.SaveData(sql, data);
        }


        public static List<PeopleModel> LoadRecords()
        {
            string sql = @"select  [id], [name], [biography], [addedOn], [status] from dbo.People;";

            return SqlDataAccess.LoadData<PeopleModel>(sql);
        }


        public static List<PeopleModel> LoadOneRecord(int iId)
        {
            string sql = @"select  [id], [name], [biography], [addedOn], [status] from dbo.People where id=" + iId + ";";

            return SqlDataAccess.LoadData<PeopleModel>(sql);
        }




        public static int UpdateRecord(int iId, string sName, string sBiography, DateTime dAddedOn, bool bStatus)
        {
            DataLibrary.Models.PeopleModel data = new DataLibrary.Models.PeopleModel
            {
                name = sName,
                biography = sBiography,
                status = bStatus
            };

            string sqlU = @"update dbo.People set [name]=@name, [biography]=@biography, [status]=@status  
                            where Id=" + iId + ";";

            return SqlDataAccess.SaveData(sqlU, data);
        }


        public static int DeleteRecord(int iId)
        {
            string sqlU = @"delete from dbo.People where Id=" + iId + ";";

            return SqlDataAccess.DeleteData(sqlU);
        }

        public static int CountRecords()
        {
            string sqlU = @"select count(*) as count from dbo.People;";

            return SqlDataAccess.CountData(sqlU);
        }


        public static List<PeopleModel> LoadPeopleSearch(int PageNo, int PageSize, string SearchFor)
        {
            string sql = @"select [id], [name], [biography], [addedOn], [status] from dbo.People;";

            if (SearchFor == "")
            {
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,* " +
                    "FROM dbo.People WHERE 1 = 1 )  a WHERE a.RowNum " +
                    "BETWEEN(CAST(" + PageNo + " AS int) -1)* CAST(" + PageSize + " AS int) " +
                    "+1 AND (CAST(" + PageNo + " AS int)*     CAST(" + PageSize + " AS int)); ";
            }
            else
            {
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,* " +
                    "FROM dbo.People WHERE " + SearchFor + " )  a WHERE a.RowNum " +
                    "BETWEEN(CAST(" + PageNo + " AS int) -1)* CAST(" + PageSize + " AS int) " +
                    "+1 AND (CAST(" + PageNo + " AS int)*     CAST(" + PageSize + " AS int)); ";
            }

            return SqlDataAccess.LoadData<PeopleModel>(sql);
        }


    }
}
