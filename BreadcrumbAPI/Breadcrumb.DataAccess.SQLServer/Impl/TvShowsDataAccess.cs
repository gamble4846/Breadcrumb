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
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class TvShowsDataAccess : ITvShowsDataAccess
    {
        private String ConnectionString { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public TvShowsDataAccess(String connectionString, CommonFunctions commonFunctions)
        {
            CommonFunctions = commonFunctions;
            ConnectionString = connectionString;
        }

        public dynamic GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            int totalRecords = 0;
            var ret = new List<vTvShowsModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPGetTvShows";
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

        public vTvShowsModel GetTvshowById(Guid ShowId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM vTvShows t WHERE ShowId = @ShowId";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.Parameters.AddWithValue("@ShowId", ShowId);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vTvShowsModel>();
                return ret;
            }
        }

        public vTvShowsModel InsertTvShows(vTvShowsViewModel ViewModel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertTvShow";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PrimaryName", SqlDbType.NVarChar).Value = ViewModel.PrimaryName;
                cmd.Parameters.Add("@OtherNames", SqlDbType.NVarChar).Value = ViewModel.OtherNames;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;
                cmd.Parameters.Add("@IMDBID", SqlDbType.NVarChar).Value = ViewModel.IMDBID;
                cmd.Parameters.Add("@ReleaseYear", SqlDbType.NVarChar).Value = ViewModel.ReleaseYear;
                cmd.Parameters.Add("@Genres", SqlDbType.NVarChar).Value = ViewModel.Genres;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vTvShowsModel>();
                return ret;
            }
        }

        public vTvShowsModel UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPUpdateTvShow";
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
                var ret = dt.DataTableToObject<vTvShowsModel>();
                return ret;
            }
        }

        public vTvShowsModel DeleteTvShow(Guid ShowId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPDeleteTvShow";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vTvShowsModel>();
                return ret;
            }
        }

        public vTvShowsModel GetIfTvShowExistsByImdbID(string IMDBId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT * FROM vTvShows WHERE IMDBID = @IMDBId";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.Parameters.AddWithValue("@IMDBId", IMDBId);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<vTvShowsModel>();
                return ret;
            }
        }

        public List<vTvShowsModel> GetAllTvshows()
        {
            var ret = new List<vTvShowsModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM vTvShows t order by t.PrimaryName";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<vTvShowsModel>();
                return ret;
            }
        }



        public List<tbSeasonsModel> GetTvShowSeasons(Guid ShowId)
        {
            var ret = new List<tbSeasonsModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPGetTvShowSeasons";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ShowId;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbSeasonsModel>();
                return ret;
            }
        }

        public tbSeasonsModel InsertTvShowSeason(tbSeasonsViewModel ViewModel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertTvShowSeason";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ViewModel.ShowId;
                cmd.Parameters.Add("@Number", SqlDbType.Int).Value = ViewModel.Number;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = ViewModel.Name;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<tbSeasonsModel>();
                return ret;
            }
        }

        public List<tbSeasonsModel> InsertUpdateTvShowSeasonMultiple(List<tbSeasonsViewModel> ViewModelList)
        {
            var SeasonsMultipleInsertUpdateSP = UtilityCustom.ToDataTable<tbSeasonsViewModel>(ViewModelList);
            var ret = new List<tbSeasonsModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertUpdateTvShowSeasonsMultiple";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = SeasonsMultipleInsertUpdateSP;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbSeasonsModel>();
                return ret;
            }
        }

        public tbSeasonsModel UpdateTvShowSeasons(tbSeasonsViewModel ViewModel, Guid SeasonId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPUpdateTvShowSeason";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SeasonId", SqlDbType.UniqueIdentifier).Value = SeasonId;
                cmd.Parameters.Add("@Number", SqlDbType.NVarChar).Value = ViewModel.Number;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = ViewModel.Name;
                cmd.Parameters.Add("@ShowId", SqlDbType.UniqueIdentifier).Value = ViewModel.ShowId;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<tbSeasonsModel>();
                return ret;
            }
        }

        public tbSeasonsModel DeleteTvShowSeasons(Guid SeasonId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPDeleteTvShowSeason";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SeasonId", SqlDbType.UniqueIdentifier).Value = SeasonId;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<tbSeasonsModel>();
                return ret;
            }
        }



        public List<tbEpisodesModel> GetTvShowEpisodes(Guid SeasonId)
        {
            var ret = new List<tbEpisodesModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPGetTvShowEpisodes";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SeasonId", SqlDbType.UniqueIdentifier).Value = SeasonId;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbEpisodesModel>();
                return ret;
            }
        }

        public tbEpisodesModel InsertTvShowEpisodes(tbEpisodesViewModel ViewModel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertTvShowEpisode";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SeasonId", SqlDbType.UniqueIdentifier).Value = ViewModel.SeasonId;
                cmd.Parameters.Add("@Number", SqlDbType.Int).Value = ViewModel.Number;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = ViewModel.Name;
                cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = ViewModel.RelaseDate;
                cmd.Parameters.Add("@ThumbnailLink", SqlDbType.NVarChar).Value = ViewModel.ThumbnailLink;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<tbEpisodesModel>();
                return ret;
            }
        }

        public List<tbEpisodesModel> InsertUpdateTvShowEpisodesMultiple(List<tbEpisodesViewModel> ViewModelList)
        {
            var EpisodesMultipleInsertUpdateSP = UtilityCustom.ToDataTable<tbEpisodesViewModel>(ViewModelList);
            var ret = new List<tbEpisodesModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertUpdateTvShowEpisodesMultiple";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = EpisodesMultipleInsertUpdateSP;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbEpisodesModel>();
                return ret;
            }
        }

        public tbEpisodesModel UpdateTvShowEpisodes(tbEpisodesViewModel ViewModel, Guid EpisodeId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPUpdateTvShowEpisode";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EpisodeId", SqlDbType.UniqueIdentifier).Value = EpisodeId;
                cmd.Parameters.Add("@Number", SqlDbType.NVarChar).Value = ViewModel.Number;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = ViewModel.Name;
                cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = ViewModel.RelaseDate;
                cmd.Parameters.Add("@SeasonId", SqlDbType.UniqueIdentifier).Value = ViewModel.SeasonId;
                cmd.Parameters.Add("@ThumbnailLink", SqlDbType.NVarChar).Value = ViewModel.ThumbnailLink;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ViewModel.Description;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<tbEpisodesModel>();
                return ret;
            }
        }

        public tbEpisodesModel DeleteTvShowEpisodes(Guid EpisodeId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPDeleteTvShowEpisode";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EpisodeId", SqlDbType.UniqueIdentifier).Value = EpisodeId;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                var ret = dt.DataTableToObject<tbEpisodesModel>();
                return ret;
            }
        }



        public List<tbShowsFileModel> InsertUpdateShowFiles(List<tbShowsFileViewModel> ViewModelList)
        {
            var ShowsFileInsertUpdateSP = UtilityCustom.ToDataTable<tbShowsFileViewModel>(ViewModelList);
            var ret = new List<tbShowsFileModel>(); 
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SPInsertUpdateShowsFileMultiple";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Values", SqlDbType.Structured).Value = ShowsFileInsertUpdateSP;

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbShowsFileModel>();
                return ret;
            }
        }

        public bool DeleteMultipleShowFiles(string ShowFileIds)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"DELETE FROM tbShowsFile Where Id IN (" + ShowFileIds + ")";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                SqlTransaction transaction = cmd.Connection.BeginTransaction("");
                cmd.Transaction = transaction;

                try
                {
                    connection.Open();
                    var recs = cmd.ExecuteNonQuery();
                    if (recs > 0)
                    {
                        transaction.Commit();
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return true;
                    }
                }
                catch (Exception)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return false;
            }
        }

        public List<Guid> GetEpisodeIdsFromSeasonID(Guid SeasonId)
        {
            var ret = new List<Guid>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"Select Id as EpisodeId from [tbEpisodes] Where [SeasonId] = @SeasonId";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.Parameters.AddWithValue("@SeasonId", SeasonId);

                try
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(reader.GetValue<Guid>("EpisodeId"));
                        }
                    }
                }
                catch (Exception)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return ret;
            }
        }
    }
}

