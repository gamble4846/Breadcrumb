using Breadcrumb.Utility;
using Breadcrumb.DataAccess.SQLServer.Interface;
using Breadcrumb.Model;
using Breadcrumb.DataAccess.SqlClient;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class ShowsDataAccess : IShowsDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public ShowsDataAccess(MSSqlDatabase msSqlDatabase, CommonFunctions commonFunctions)
        {
            MSSqlDatabase = msSqlDatabase;
            CommonFunctions = commonFunctions;
        }

        public List<ShowsModel> GetShows(int page, int itemsPerPage, List<OrderByModel> orderBy, string FilterQuery, string Type)
        {
            var ret = new List<ShowsModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT t.* FROM Shows t WHERE Type = @Type Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
            if (orderBy != null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            }
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            cmd.Parameters.AddWithValue("@Type", Type);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<ShowsModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }

        public int GetShowsTotal(string FilterQuery, string Type)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Shows t WHERE Type = @Type";
            cmd.Parameters.AddWithValue("@Type", Type);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return reader.GetInt32("TotalRecord");
                }
            }
            return 0;
        }
    }
}

