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
using Breadcrumb.Model.FilesModels;
using Breadcrumb.Model.vMoviesModels;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class FilesDataAccess : IFilesDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public FilesDataAccess(MSSqlDatabase msSqlDatabase, CommonFunctions commonFunctions)
        {
            MSSqlDatabase = msSqlDatabase;
            CommonFunctions = commonFunctions;
        }

        public List<tbFilesDataModel> SPInsertMultipleFiles(List<tbFilesDataViewModel> tbFilesDataViewModel)
        {
            var FilesDataMultipleInsertSP = UtilityCustom.ToDataTable<tbFilesDataViewModel>(tbFilesDataViewModel);

            var ret = new List<tbFilesDataModel>();

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPInsertMultipleFiles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = FilesDataMultipleInsertSP;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbFilesDataModel>(reader);
                    ret.Add(t);
                }
            }

            return ret;
        }

        public List<tbFileDataChunksModel> SPInsertMultipleFilesChunks(List<tbFileDataChunksViewModel> tbFileDataChunksViewList)
        {
            var FilesDataChunksMultipleInsertSP = UtilityCustom.ToDataTable<tbFileDataChunksViewModel>(tbFileDataChunksViewList);

            var ret = new List<tbFileDataChunksModel>();

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPInsertMultipleFilesChunks";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = FilesDataChunksMultipleInsertSP;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbFileDataChunksModel>(reader);
                    ret.Add(t);
                }
            }

            return ret;
        }

        public List<vNotAssignedFilesModel> GetNotAssignedFiles()
        {
            var ret = new List<vNotAssignedFilesModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  v.* FROM vNotAssignedFiles v";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vNotAssignedFilesModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }

        public List<tbFilesDataModel> GetFiles()
        {
            var ret = new List<tbFilesDataModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM tbFilesData t";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbFilesDataModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }

        public List<vFullShowFilesModel> GetFilesByEpisodeIds(String EpisodeIds)
        {
            var ret = new List<vFullShowFilesModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM vFullShowFiles t where t.EpisodeId IN (" + EpisodeIds + ")";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vFullShowFilesModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }

        public List<tbFileDataChunksModel> GetChunksFromFileIds(String FileIds)
        {
            var ret = new List<tbFileDataChunksModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM tbFIleDataChunks t where t.FileDataID IN (" + FileIds + ")";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbFileDataChunksModel>(reader);
                    ret.Add(t);
                }
            }
            return ret;
        }

    }
}

