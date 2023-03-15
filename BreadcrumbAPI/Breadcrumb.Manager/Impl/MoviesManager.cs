using Microsoft.AspNetCore.Http;
using Breadcrumb.Manager.Interface;
using Breadcrumb.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Breadcrumb.DataAccess.SQLServer.Impl;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model;
using System.Threading.Tasks;
using Breadcrumb.Model.TheMovieDBModels;
using FastMember;
using Breadcrumb.Model.vMoviesModels;

namespace Breadcrumb.Manager.Impl
{
    public class MoviesManager : IMoviesManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.IMoviesDataAccess SqlMoviesDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string ConnectionString { get; set; }
        public string ServerType { get; set; }
        public ITheMovieDBManager TheMovieDBManager { get; set; }

        public MoviesManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITheMovieDBManager theMovieDBManager)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            ConnectionString = TokenData.Servers.Find(x => x.IsSelected).ConnectionString;
            ServerType = TokenData.Servers.Find(x => x.IsSelected).DatabaseType;
            TheMovieDBManager = theMovieDBManager;
        }

        public APIResponse GetMovies(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlMoviesDataAccess = new MoviesDataAccesscs(MsSqlDatabase, CommonFunctions);

                    var result = SqlMoviesDataAccess.GetMovies(page, itemsPerPage, orderBy, FilterQuery);
                    if (result != null)
                    {
                        var response = new { records = result.Data, pageNumber = page, pageSize = itemsPerPage, totalRecords = result.TotalRecords };
                        return new APIResponse(ResponseCode.SUCCESS, "Records Found", response);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Found");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse GetMovieById(Guid ShowId)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlMoviesDataAccess = new MoviesDataAccesscs(MsSqlDatabase, CommonFunctions);

                    var result = SqlMoviesDataAccess.GetMovieById(ShowId);
                    if (result != null)
                    {
                        return new APIResponse(ResponseCode.SUCCESS, "Records Found", result);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Found");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse InsertMovie(vMoviesViewModel ViewModel)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlMoviesDataAccess = new MoviesDataAccesscs(MsSqlDatabase, CommonFunctions);

                    var result = SqlMoviesDataAccess.InsertMovie(ViewModel);
                    if (result != null)
                    {
                        return new APIResponse(ResponseCode.SUCCESS, "Records Inserted", result);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Inserted");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse UpdateMovie(vMoviesViewModel ViewModel, Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlMoviesDataAccess = new MoviesDataAccesscs(MsSqlDatabase, CommonFunctions);

                    var result = SqlMoviesDataAccess.UpdateMovie(ViewModel, ShowId);
                    if (result != null)
                    {
                        return new APIResponse(ResponseCode.SUCCESS, "Records Updated", result);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Updated");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public async Task<APIResponse> InsertUpdateFullMovie(string IMDBId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            var MovieFullData = await TheMovieDBManager.GetMovieByIMDBId(IMDBId);
            var FullTVShowView = (vMoviesViewModel)MovieFullData.Document;
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlMoviesDataAccess = new MoviesDataAccesscs(MsSqlDatabase, CommonFunctions);

                    var InsertedUpdatedMovie = new vMoviesModel();

                    var MovieExists = SqlMoviesDataAccess.GetIfMovieExistsByImdbID(IMDBId);
                    if (MovieExists == null)
                    { InsertedUpdatedMovie = SqlMoviesDataAccess.InsertMovie(FullTVShowView); }
                    else
                    { InsertedUpdatedMovie = SqlMoviesDataAccess.UpdateMovie(FullTVShowView, MovieExists.ShowId ?? new Guid()); }

                    return new APIResponse(ResponseCode.SUCCESS, "TvShow Updated", InsertedUpdatedMovie);
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse DeleteMovie(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlMoviesDataAccess = new MoviesDataAccesscs(MsSqlDatabase, CommonFunctions);

                    var result = SqlMoviesDataAccess.DeleteMovie(ShowId);
                    if (result != null)
                    {
                        return new APIResponse(ResponseCode.SUCCESS, "Records Deleted", result);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Deleted");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }
    }
}

