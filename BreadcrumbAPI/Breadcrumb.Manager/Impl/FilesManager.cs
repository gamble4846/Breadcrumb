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
using System.Linq;
using static System.Net.WebRequestMethods;
using System.Drawing;

namespace Breadcrumb.Manager.Impl
{
    public class FilesManager : IFilesManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
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
                    SqlFilesDataAccess = new FilesDataAccess(ConnectionString, CommonFunctions);

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
                            var tbFileDataChunks = new tbFileDataChunksViewModel()
                            {
                                FileDataID = tbFilesData[indexFF].Id,
                                Email = fileChunk.Email,
                                Password = fileChunk.Password,
                                OtherData = fileChunk.OtherData,
                            };

                            if (fileChunk.Id.Contains("/"))
                            {
                                tbFileDataChunks.Link = fileChunk.Id;
                            }
                            else
                            {
                                tbFileDataChunks.Link = "https://drive.google.com/file/d/" + fileChunk.Id + "/view?usp=share_link";
                            }
                            try { tbFileDataChunks.Size = Decimal.Parse(fileChunk.Size.ToString()); } catch { tbFileDataChunks.Size = 0; }

                            tbFileDataChunksViewList.Add(tbFileDataChunks);
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

        public APIResponse GetNotAssignedFiles()
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlFilesDataAccess = new FilesDataAccess(ConnectionString, CommonFunctions);

                    var vNotAssignedFilesList = SqlFilesDataAccess.GetNotAssignedFiles();
                    if (vNotAssignedFilesList != null && vNotAssignedFilesList.Count > 0)
                    {
                        return new APIResponse(ResponseCode.SUCCESS, "Records Found", vNotAssignedFilesList);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Found");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse GetFiles()
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlFilesDataAccess = new FilesDataAccess(ConnectionString, CommonFunctions);

                    var vNotAssignedFilesList = SqlFilesDataAccess.GetFiles();
                    if (vNotAssignedFilesList != null && vNotAssignedFilesList.Count > 0)
                    {
                        return new APIResponse(ResponseCode.SUCCESS, "Records Found", vNotAssignedFilesList);
                    }
                    else
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Found");
                    }
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse FilesWithChunksFromEpisodeId(Guid EpisodeId)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlFilesDataAccess = new FilesDataAccess(ConnectionString, CommonFunctions);

                    var EpisodeFiles = SqlFilesDataAccess.GetFilesByEpisodeIds("'" + EpisodeId + "'");
                    if (EpisodeFiles == null || EpisodeFiles.Count <= 0)
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Found");
                    }

                    var AllFileIds = EpisodeFiles.Select(x => x.FileId).ToList();
                    var AllFileChunks = SqlFilesDataAccess.GetChunksFromFileIds("'" + String.Join("','", AllFileIds) + "'");

                    List<FullShowFilesWithChunksModel> ShowFilesWithChunks = new List<FullShowFilesWithChunksModel>();

                    foreach (var file in EpisodeFiles)
                    {
                        var ShowFileWithChunks = new FullShowFilesWithChunksModel();
                        ShowFileWithChunks.ShowFileId = file.ShowFileId;
                        ShowFileWithChunks.FileId = file.FileId;
                        ShowFileWithChunks.Description = file.Description;
                        ShowFileWithChunks.Type = file.Type;
                        ShowFileWithChunks.Name = file.Name;
                        ShowFileWithChunks.ThumbnailLink = file.ThumbnailLink;
                        ShowFileWithChunks.ShowId = file.ShowId;
                        ShowFileWithChunks.SeasonId = file.SeasonId;
                        ShowFileWithChunks.EpisodeId = file.EpisodeId;
                        ShowFileWithChunks.Quality = file.Quality;
                        ShowFileWithChunks.AudioLanguages = file.AudioLanguages;
                        ShowFileWithChunks.SubtitleLanguages = file.SubtitleLanguages;
                        ShowFileWithChunks.Chunks = AllFileChunks.Where(x => x.FileDataID == file.FileId).ToList();
                        ShowFilesWithChunks.Add(ShowFileWithChunks);
                    }

                    return new APIResponse(ResponseCode.SUCCESS, "Records Found", ShowFilesWithChunks);
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }
    }
}

