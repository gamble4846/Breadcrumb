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

namespace Breadcrumb.Manager.Impl
{
    public class TvShowsManager : ITvShowsManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ITvShowsDataAccess SqlTvShowsDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string ConnectionString { get; set; }
        public string ServerType { get; set; }

        public TvShowsManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            ConnectionString = TokenData.Servers.Find(x => x.IsSelected).ConnectionString;
            ServerType = TokenData.Servers.Find(x => x.IsSelected).DatabaseType;
        }

        public APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.GetTvShows(page, itemsPerPage, orderBy, FilterQuery);
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

        public APIResponse InsertTvShow(vTvShowsViewModel ViewModel)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.InsertTvShows(ViewModel);
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

        public APIResponse UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.UpdateTvShow(ViewModel, ShowId);
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

        public APIResponse DeleteTvShow(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.DeleteTvShow(ShowId);
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



        public APIResponse GetTvShowSeasons(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.GetTvShowSeasons(ShowId);
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

        public APIResponse InsertTvShowSeason(tbSeasonsViewModel ViewModel)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.InsertTvShowSeason(ViewModel);
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

        public APIResponse InsertUpdateTvShowSeasonMultiple(List<tbSeasonsViewModel> ViewModelList)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.InsertUpdateTvShowSeasonMultiple(ViewModelList);
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

        public APIResponse UpdateTvShowSeasons(tbSeasonsViewModel ViewModel, Guid SeasonId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.UpdateTvShowSeasons(ViewModel, SeasonId);
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

        public APIResponse DeleteTvShowSeasons(Guid SeasonId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.DeleteTvShowSeasons(SeasonId);
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



        public APIResponse GetTvShowEpisodes(Guid SeasonId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.GetTvShowEpisodes(SeasonId);
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

        public APIResponse InsertTvShowEpisodes(tbEpisodesViewModel ViewModel)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.InsertTvShowEpisodes(ViewModel);
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

        public APIResponse InsertUpdateTvShowEpisodesMultiple(List<tbEpisodesViewModel> ViewModelList)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.InsertUpdateTvShowEpisodesMultiple(ViewModelList);
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

        public APIResponse UpdateTvShowEpisodes(tbEpisodesViewModel ViewModel, Guid EpisodeId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.UpdateTvShowEpisodes(ViewModel, EpisodeId);
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

        public APIResponse DeleteTvShowEpisodes(Guid EpisodeId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.DeleteTvShowEpisodes(EpisodeId);
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

