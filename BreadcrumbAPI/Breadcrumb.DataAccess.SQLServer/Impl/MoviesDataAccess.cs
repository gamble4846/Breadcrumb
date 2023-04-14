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
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class MoviesDataAccesscs : IMoviesDataAccess
    {
        private String ConnectionString { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public MoviesDataAccesscs(String connectionString, CommonFunctions commonFunctions)
        {
            CommonFunctions = commonFunctions;
            ConnectionString = connectionString;
        }

        public dynamic GetMovies(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            int totalRecords = 0;
            var ret = new List<vTvShowsModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPGetMovies";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@totalRows", SqlDbType.Int).Value = totalRecords;
                cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                cmd.Parameters.Add("@itemsPerPage", SqlDbType.Int).Value = itemsPerPage;
                cmd.Parameters.Add("@orderByQuery", SqlDbType.VarChar).Value = orderBy;
                cmd.Parameters.Add("@filterQuery", SqlDbType.VarChar).Value = FilterQuery;
                cmd.Parameters["@totalRows"].Direction = ParameterDirection.Output;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<vTvShowsModel>();
                totalRecords = Convert.ToInt32(cmd.Parameters["@totalRows"].Value);
                dynamic Result = new System.Dynamic.ExpandoObject();

                Result.Data = ret;
                Result.TotalRecords = totalRecords;
                return Result;
            }
        }

        public vMoviesModel GetMovieById(Guid ShowId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM vMovies t WHERE ShowId = @ShowId";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.Parameters.AddWithValue("@ShowId", ShowId);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vMoviesModel>();
                return ret;
            }
        }

        public vMoviesModel InsertMovie(vMoviesViewModel ViewModel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertMovie";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PrimaryName", SqlDbType.NVarChar).Value = ViewModel.PrimaryName;
                cmd.Parameters.Add("@OtherNames", SqlDbType.NVarChar).Value = ViewModel.OtherNames;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;
                cmd.Parameters.Add("@IMDBID", SqlDbType.NVarChar).Value = ViewModel.IMDBID;
                cmd.Parameters.Add("@ReleaseYear", SqlDbType.NVarChar).Value = ViewModel.ReleaseYear;
                cmd.Parameters.Add("@Genres", SqlDbType.NVarChar).Value = ViewModel.Genres;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vMoviesModel>();
                return ret;
            }
        }

        public vMoviesModel UpdateMovie(vMoviesViewModel ViewModel, Guid ShowId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPUpdateMovie";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;
                cmd.Parameters.Add("@PrimaryName", SqlDbType.NVarChar).Value = ViewModel.PrimaryName;
                cmd.Parameters.Add("@OtherNames", SqlDbType.NVarChar).Value = ViewModel.OtherNames;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;
                cmd.Parameters.Add("@IMDBID", SqlDbType.NVarChar).Value = ViewModel.IMDBID;
                cmd.Parameters.Add("@ReleaseYear", SqlDbType.NVarChar).Value = ViewModel.ReleaseYear;
                cmd.Parameters.Add("@Genres", SqlDbType.NVarChar).Value = ViewModel.Genres;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vMoviesModel>();
                return ret;
            }
        }

        public vMoviesModel GetIfMovieExistsByImdbID(string IMDBId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT * FROM vMovies WHERE IMDBID = @IMDBId";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.Parameters.AddWithValue("@IMDBId", IMDBId);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vMoviesModel>();
                return ret;
            }
        }

        public vMoviesModel DeleteMovie(Guid ShowId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPDeleteMovie";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vMoviesModel>();
                return ret;
            }
        }
    }
}

