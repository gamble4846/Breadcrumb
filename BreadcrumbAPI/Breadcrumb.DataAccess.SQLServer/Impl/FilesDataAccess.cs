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
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class FilesDataAccess : IFilesDataAccess
    {
        private String ConnectionString { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public FilesDataAccess(String connectionString, CommonFunctions commonFunctions)
        {
            CommonFunctions = commonFunctions;
            ConnectionString = connectionString;
        }

        public List<tbFilesDataModel> SPInsertMultipleFiles(List<tbFilesDataViewModel> tbFilesDataViewModel)
        {
            var FilesDataMultipleInsertSP = UtilityCustom.ToDataTable<tbFilesDataViewModel>(tbFilesDataViewModel);
            var ret = new List<tbFilesDataModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertMultipleFiles";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = FilesDataMultipleInsertSP;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbFilesDataModel>();
                return ret;
            }
        }

        public List<tbFileDataChunksModel> SPInsertMultipleFilesChunks(List<tbFileDataChunksViewModel> tbFileDataChunksViewList)
        {
            var FilesDataChunksMultipleInsertSP = UtilityCustom.ToDataTable<tbFileDataChunksViewModel>(tbFileDataChunksViewList);
            var ret = new List<tbFileDataChunksModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertMultipleFilesChunks";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = FilesDataChunksMultipleInsertSP;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbFileDataChunksModel>();
                return ret;
            }
        }

        public List<vNotAssignedFilesModel> GetNotAssignedFiles()
        {
            var ret = new List<vNotAssignedFilesModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  v.* FROM vNotAssignedFiles v";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<vNotAssignedFilesModel>();
                return ret;
            }
        }

        public List<tbFilesDataModel> GetFiles()
        {
            var ret = new List<tbFilesDataModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"XISELECT  t.* FROM tbFilesData tIX";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbFilesDataModel>();
                return ret;
            }
        }

        public List<vFullShowFilesModel> GetFilesByEpisodeIds(String EpisodeIds)
        {
            var ret = new List<vFullShowFilesModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM vFullShowFiles t where t.EpisodeId IN (" + EpisodeIds + ")";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<vFullShowFilesModel>();
                return ret;
            }
        }

        public List<tbFileDataChunksModel> GetChunksFromFileIds(String FileIds)
        {
            var ret = new List<tbFileDataChunksModel>(); 
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM tbFIleDataChunks t where t.FileDataID IN (" + FileIds + ")";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbFileDataChunksModel>();
                return ret;
            }
        }
    }
}

