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

namespace Breadcrumb.Manager.Impl
{
    public class TvShowsManager : ITvShowsManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ITvShowsDataAccess SqlTvShowsDataAccess { get; set; }

        public TvShowsManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
        }

        public APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
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
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", TokenData.DatabaseType);
            }
        }

        public APIResponse InsertTvShow(vTvShowsViewModel ViewModel)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
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
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", TokenData.DatabaseType);
            }
        }

        public APIResponse UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
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
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", TokenData.DatabaseType);
            }
        }

        public APIResponse DeleteTvShow(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
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
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", TokenData.DatabaseType);
            }
        }

        public APIResponse GetTvShowSeasons(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
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
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", TokenData.DatabaseType);
            }
        }
    }
}

