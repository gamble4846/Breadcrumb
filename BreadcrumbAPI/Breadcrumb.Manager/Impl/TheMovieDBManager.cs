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
using Azure;

namespace Breadcrumb.Manager.Impl
{
    public class TheMovieDBManager : ITheMovieDBManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ITvShowsDataAccess SqlTvShowsDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string TheMovieDBAPIKey { get; set; }

        public TheMovieDBManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            TheMovieDBAPIKey = TokenData.TheMovieDBAPIKey;
        }

        public async Task<APIResponse> GetTvshowByIMDBId(string IMDBId)
        {
            var tvShowFromIMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/find/" + IMDBId + "?api_key=" + TheMovieDBAPIKey + "&external_source=imdb_id");
            return new APIResponse(ResponseCode.SUCCESS, "Records Found", tvShowFromIMDB);
        }
    }
}

