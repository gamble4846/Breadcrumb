using Breadcrumb.Utility;
using Breadcrumb.DataAccess.SQLServer.Interface;
using Breadcrumb.DataAccess.SqlClient;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class TvShowsDataAccess : ITvShowsDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public TvShowsDataAccess(MSSqlDatabase msSqlDatabase, CommonFunctions commonFunctions)
        {
            MSSqlDatabase = msSqlDatabase;
            CommonFunctions = commonFunctions;
        }

        public dynamic GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            int totalRecords = 0;
            var ret = new List<vTvShowsModel>();

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPGetTvShows";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@totalRows", SqlDbType.Int).Value = totalRecords;
            cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
            cmd.Parameters.Add("@itemsPerPage", SqlDbType.Int).Value = itemsPerPage;
            cmd.Parameters.Add("@orderByQuery", SqlDbType.VarChar).Value = orderBy;
            cmd.Parameters.Add("@filterQuery", SqlDbType.VarChar).Value = FilterQuery;

            cmd.Parameters["@totalRows"].Direction = ParameterDirection.Output;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vTvShowsModel>(reader);
                    ret.Add(t);
                }
            }

            totalRecords = Convert.ToInt32(cmd.Parameters["@totalRows"].Value);

            dynamic Result = new System.Dynamic.ExpandoObject();

            Result.Data = ret;
            Result.TotalRecords = totalRecords;

            return Result;
        }

        public vTvShowsModel InsertTvShows(vTvShowsViewModel ViewModel)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPInsertTvShow";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PrimaryName", SqlDbType.NVarChar).Value = ViewModel.PrimaryName;
            cmd.Parameters.Add("@OtherNames", SqlDbType.NVarChar).Value = ViewModel.OtherNames;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;
            cmd.Parameters.Add("@IMDBID", SqlDbType.NVarChar).Value = ViewModel.IMDBID;
            cmd.Parameters.Add("@ReleaseYear", SqlDbType.NVarChar).Value = ViewModel.ReleaseYear;
            cmd.Parameters.Add("@Genres", SqlDbType.NVarChar).Value = ViewModel.Genres;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vTvShowsModel>(reader);
                    return t;
                }
            }

            return null;
        }

        public vTvShowsModel UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPUpdateTvShow";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;
            cmd.Parameters.Add("@PrimaryName", SqlDbType.NVarChar).Value = ViewModel.PrimaryName;
            cmd.Parameters.Add("@OtherNames", SqlDbType.NVarChar).Value = ViewModel.OtherNames;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;
            cmd.Parameters.Add("@IMDBID", SqlDbType.NVarChar).Value = ViewModel.IMDBID;
            cmd.Parameters.Add("@ReleaseYear", SqlDbType.NVarChar).Value = ViewModel.ReleaseYear;
            cmd.Parameters.Add("@Genres", SqlDbType.NVarChar).Value = ViewModel.Genres;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vTvShowsModel>(reader);
                    return t;
                }
            }

            return null;
        }

        public vTvShowsModel DeleteTvShow(Guid ShowId)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPDeleteTvShow";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vTvShowsModel>(reader);
                    return t;
                }
            }

            return null;
        }

        public List<tbSeasonsModel> GetTvShowSeasons(Guid ShowId)
        {
            var ret = new List<tbSeasonsModel>();
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPGetTvShowSeasons";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<tbSeasonsModel>(reader);
                    ret.Add(t);
                }
            }

            return ret;
        }
    }
}

