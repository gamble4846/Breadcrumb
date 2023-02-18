using Microsoft.AspNetCore.Http;
using Breadcrumb.Manager.Interface;
using Breadcrumb.Model;
using Breadcrumb.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Breadcrumb.DataAccess.SQLServer.Impl;

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

        public APIResponse Insert(vTvShowsViewModel ViewModel)
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

        public APIResponse Update(vTvShowsViewModel ViewModel, Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.Update(ViewModel, ShowId);
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

        public APIResponse Delete(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
                    SqlTvShowsDataAccess = new TvShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlTvShowsDataAccess.Delete(ShowId);
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
    }
}

