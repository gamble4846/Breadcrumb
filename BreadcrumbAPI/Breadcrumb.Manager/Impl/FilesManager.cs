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
using Breadcrumb.Model.FilesModels;
using Breadcrumb.Model.GoogleApiModels;

namespace Breadcrumb.Manager.Impl
{
    public class FilesManager : IFilesManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.IFilesDataAccess SqlFilesDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string ConnectionString { get; set; }
        public string ServerType { get; set; }

        public FilesManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            ConnectionString = TokenData.Servers.Find(x => x.IsSelected).ConnectionString;
            ServerType = TokenData.Servers.Find(x => x.IsSelected).DatabaseType;
        }

        public APIResponse AddFinalFiles(List<FinalFile> FinalFiles)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    MsSqlDatabase = new MSSqlDatabase(ConnectionString);
                    SqlFilesDataAccess = new FilesDataAccess(MsSqlDatabase, CommonFunctions);

                    var tbFilesDataViewList = new List<tbFilesDataViewModel>();

                    foreach(var finalFile in FinalFiles)
                    {
                        var tbFile = new tbFilesDataViewModel()
                        {
                            Description = null,
                            Type = finalFile.Type,
                            Name = finalFile.Name,
                            ThumbnailLink = finalFile.ThumbnailLink,
                        };

                        tbFilesDataViewList.Add(tbFile);
                    }

                    var tbFilesData = SqlFilesDataAccess.SPInsertMultipleFiles(tbFilesDataViewList);

                    if(FinalFiles.Count != tbFilesData.Count)
                    {
                        return new APIResponse(ResponseCode.ERROR, "There was a problem adding all the file (tbFilesData)", tbFilesData);
                    }


                    var tbFileDataChunksViewList = new List<tbFileDataChunksViewModel>();

                    for (int indexFF = 0; indexFF < FinalFiles.Count; indexFF++)
                    {
                        foreach(var fileChunk in FinalFiles[indexFF].FileChunks)
                        {
                            var tbFileDataChunksViewModel = new tbFileDataChunksViewModel()
                            {
                                FileDataID = tbFilesData[indexFF].Id,
                                Email = fileChunk.Email,
                                Size = fileChunk.Size,
                                Password = null,
                                Link = "https://docs.google.com/document/d/"+ fileChunk.Id + "/edit?usp=share_link",
                                OtherData = null,
                            };

                            tbFileDataChunksViewList.Add(tbFileDataChunksViewModel);
                        }
                    }

                    var tbFilesDataChunks = SqlFilesDataAccess.SPInsertMultipleFilesChunks(tbFileDataChunksViewList);

                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.tbFilesData = tbFilesData;
                    result.tbFilesDataChunks = tbFilesDataChunks;


                    return new APIResponse(ResponseCode.SUCCESS, "Files And Data Chunks Added", result);
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }
    }
}

