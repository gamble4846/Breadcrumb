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

namespace Breadcrumb.Manager.Impl
{
    public class CoversManager : ICoversManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ICoversDataAccess SqlCoversDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string ConnectionString { get; set; }
        public string ServerType { get; set; }

        public CoversManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            ConnectionString = TokenData.Servers.Find(x => x.IsSelected).ConnectionString;
            ServerType = TokenData.Servers.Find(x => x.IsSelected).DatabaseType;
        }

        public APIResponse GetCoverByBreadId(Guid BreadId)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlCoversDataAccess = new CoversDataAccess(MsSqlDatabase, CommonFunctions);

                    var result = SqlCoversDataAccess.GetCoverByBreadId(BreadId);
                    if (result != null && result.Count > 0)
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
    }
}

