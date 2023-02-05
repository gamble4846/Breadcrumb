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
    public class ShowsManager : IShowsManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Impl.ShowsDataAccess SqlShowsDataAccess { get; set; }

        public ShowsManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
        }

        public APIResponse GetShows(int page, int itemsPerPage, List<OrderByModel> orderBy, string FilterQuery, string Type)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (TokenData.DatabaseType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(TokenData.ConnectionString);
                    SqlShowsDataAccess = new ShowsDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlShowsDataAccess.GetShows(page, itemsPerPage, orderBy, FilterQuery, Type);
                    if (result != null && result.Count > 0)
                    {
                        var totalRecords = SqlShowsDataAccess.GetShowsTotal(FilterQuery, Type);
                        var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
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
    }
}

