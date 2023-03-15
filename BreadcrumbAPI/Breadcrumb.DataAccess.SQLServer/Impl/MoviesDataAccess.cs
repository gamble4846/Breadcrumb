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
using Breadcrumb.Model.vMoviesModels;
using Microsoft.Identity.Client;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class MoviesDataAccesscs : IMoviesDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public MoviesDataAccesscs(MSSqlDatabase msSqlDatabase, CommonFunctions commonFunctions)
        {
            MSSqlDatabase = msSqlDatabase;
            CommonFunctions = commonFunctions;
        }

        public dynamic GetMovies(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            int totalRecords = 0;
            var ret = new List<vTvShowsModel>();

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPGetMovies";
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

        public vMoviesModel GetMovieById(Guid ShowId)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM vMovies t WHERE ShowId = @ShowId";
            cmd.Parameters.AddWithValue("@ShowId", ShowId);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vMoviesModel>(reader);
                    return t;
                }
            }
            return null;
        }

        public vMoviesModel InsertMovie(vMoviesViewModel ViewModel)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPInsertMovie";
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
                    var t = UtilityCustom.ConvertReaderToObject<vMoviesModel>(reader);
                    return t;
                }
            }

            return null;
        }

        public vMoviesModel UpdateMovie(vMoviesViewModel ViewModel, Guid ShowId)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SPUpdateMovie";
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
                    var t = UtilityCustom.ConvertReaderToObject<vMoviesModel>(reader);
                    return t;
                }
            }

            return null;
        }

        public vMoviesModel GetIfMovieExistsByImdbID(string IMDBId)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT * FROM vMovies WHERE IMDBID = @IMDBId ";
            cmd.Parameters.AddWithValue("@IMDBId", IMDBId);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var t = UtilityCustom.ConvertReaderToObject<vMoviesModel>(reader);
                    return t;
                }
            }
            return null;
        }
    }
}

