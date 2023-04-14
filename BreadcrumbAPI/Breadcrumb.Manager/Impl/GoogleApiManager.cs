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
using System.Collections;
using Breadcrumb.Model.GoogleApiModels;
using static System.Net.WebRequestMethods;

namespace Breadcrumb.Manager.Impl
{
    public class GoogleApiManager : IGoogleApiManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        public TokenModel TokenData { get; set; }
        public string ConnectionString { get; set; }
        public string ServerType { get; set; }
        public string GoogleApiKey { get; set; }

        public GoogleApiManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            ConnectionString = TokenData.Servers.Find(x => x.IsSelected).ConnectionString;
            ServerType = TokenData.Servers.Find(x => x.IsSelected).DatabaseType;
            GoogleApiKey = (TokenData.GoogleAPIs.Find(x => x.IsPrimary)).ApiKey;
        }

        public async Task<APIResponse> GetFilesFromFolderId(string FolderID)
        {
            var MainFolder = new FolderAPI()
            {
                Name = "Main",
                Id = FolderID,
                Files = new List<FilesApi>(),
                Folders = new List<FolderAPI>()
            };

            MainFolder = await CommonFunctions.GetFilesByFolderID(MainFolder, GoogleApiKey);

            return new APIResponse(ResponseCode.SUCCESS, "Records Found", MainFolder);
        }
    }
}

