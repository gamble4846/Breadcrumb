using Breadcrumb.Utility;
using Breadcrumb.DataAccess.SQLServer.Interface;
using Breadcrumb.DataAccess.SqlClient;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model.tbCoversModels;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class CoversDataAccess : ICoversDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public CoversDataAccess(MSSqlDatabase msSqlDatabase, CommonFunctions commonFunctions)
        {
            MSSqlDatabase = msSqlDatabase;
            CommonFunctions = commonFunctions;
        }

        public List<tbCoversModel> GetCoverByBreadId(Guid BreadId)
        {
            var ret = new List<tbCoversModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM tbCovers t WHERE BreadId = @BreadId";
            cmd.Parameters.AddWithValue("@BreadId", BreadId);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbCoversModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }

        public List<tbCoversModel> GetCoverByBreadIds(string BreadIds)
        {
            var ret = new List<tbCoversModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM tbCovers t WHERE BreadId IN (" + BreadIds + ")";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbCoversModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }
    }
}

