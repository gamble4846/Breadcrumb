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
using Breadcrumb.Model.tbCoversModels;
using System.Linq;

namespace Breadcrumb.Manager.Impl
{
    public class CoversManager : ICoversManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
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
                    SqlCoversDataAccess = new CoversDataAccess(ConnectionString, CommonFunctions);

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

        public APIResponse GetCoverByBreadIds(List<Guid> BreadIds)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlCoversDataAccess = new CoversDataAccess(ConnectionString, CommonFunctions);

                    var BreadIdsSTR = "'" + String.Join("','", BreadIds.ToArray()) + "'";

                    var result = SqlCoversDataAccess.GetCoverByBreadIds(BreadIdsSTR);
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

        public APIResponse InsertUpdateDeleteCoversForSingleBread(List<tbCoversModel> coversData)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlCoversDataAccess = new CoversDataAccess(ConnectionString, CommonFunctions);
                    var oldCovers = SqlCoversDataAccess.GetCoverByBreadId(coversData[0].BreadId ?? new Guid());
                    

                    var CoversToUpdate = coversData.Where(x => x.Id != null).ToList();
                    var newCoversIds = CoversToUpdate.Select(x => x.Id).ToList();
                    var CoversToInsert = coversData.Where(x => x.Id == null).ToList();
                    var CoversToDelete = new List<tbCoversModel>();
                    foreach(var cover in oldCovers)
                    {
                        if (!(newCoversIds.Contains(cover.Id)))
                        {
                            CoversToDelete.Add(cover);
                        }
                    }
                    var toDeletedIds = CoversToDelete.Select(x => x.Id).ToList();

                    var CoversToInsertQueries = new List<string>();
                    var CoversToUpdateQueries = new List<string>();
                    var CoversToDeleteQuery = "";

                    if (toDeletedIds.Count > 0)
                    {
                        CoversToDeleteQuery = "DELETE [tbCovers] WHERE [Id] IN ('" + string.Join("','", toDeletedIds) + "')";
                    }

                    foreach(var cover in CoversToInsert)
                    {
                        var currentQuery = "INSERT INTO [tbCovers] ([BreadId] ,[Link] ,[Dimensions] ,[isNSFW]) VALUES ('"+cover.BreadId+"' ,'"+cover.Link+"' ,'"+cover.Dimensions+"' ,'"+cover.isNSFW+"')";
                        CoversToInsertQueries.Add(currentQuery);
                    }

                    foreach (var cover in CoversToUpdate)
                    {
                        var currentQuery = "UPDATE [tbCovers] SET [BreadId] = '"+cover.BreadId+"' ,[Link] = '"+cover.Link+"' ,[Dimensions] = '"+cover.Dimensions+"' ,[isNSFW] = '"+cover.isNSFW+"' WHERE [Id] = '"+cover.Id+"'";
                        CoversToUpdateQueries.Add(currentQuery);
                    }

                    var result = SqlCoversDataAccess.InsertUpdateDeleteCoversForSingleBread(CoversToInsertQueries, CoversToUpdateQueries, CoversToDeleteQuery);

                    if (result)
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

