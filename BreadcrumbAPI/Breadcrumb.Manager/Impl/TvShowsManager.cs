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
using System.Linq;
using Breadcrumb.Model.FilesModels;
using static System.Net.WebRequestMethods;
using Breadcrumb.Model.tbCoversModels;

namespace Breadcrumb.Manager.Impl
{
    public class TvShowsManager : ITvShowsManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ITvShowsDataAccess SqlTvShowsDataAccess { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.IFilesDataAccess SqlFilesDataAccess { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ICoversDataAccess SqlCoversDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string ConnectionString { get; set; }
        public string ServerType { get; set; }
        public ITheMovieDBManager TheMovieDBManager { get; set; }
        public ICoversManager CoversManager { get; set; }

        public TvShowsManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITheMovieDBManager theMovieDBManager, ICoversManager coversManager)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            ConnectionString = TokenData.Servers.Find(x => x.IsSelected).ConnectionString;
            ServerType = TokenData.Servers.Find(x => x.IsSelected).DatabaseType;
            TheMovieDBManager = theMovieDBManager;
            CoversManager = coversManager;
        }

        public APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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

        public APIResponse GetTvshowById(Guid ShowId)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

                    var result = SqlTvShowsDataAccess.GetTvshowById(ShowId);
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

        public APIResponse InsertTvShow(vTvShowsViewModel ViewModel)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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

        public APIResponse GetAllTvshows()
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

                    var result = SqlTvShowsDataAccess.GetAllTvshows();
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



        public APIResponse GetTvShowSeasons(Guid ShowId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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

        public APIResponse GetTvShowEpisodesWithFiles(Guid SeasonId)
        {
            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);
                    SqlFilesDataAccess = new FilesDataAccess(ConnectionString, CommonFunctions);

                    List<EpisodesWithFilesModel> EpisodesWithFilesData = new List<EpisodesWithFilesModel>();
                    var EpisodesData = SqlTvShowsDataAccess.GetTvShowEpisodes(SeasonId);

                    if(EpisodesData == null || EpisodesData.Count <= 0)
                    {
                        return new APIResponse(ResponseCode.ERROR, "No Records Found");
                    }

                    var CurrentEpisodeIds = "'" + String.Join("','", EpisodesData.Select(x => x.Id).ToList()) + "'";

                    var AllFilesData = SqlFilesDataAccess.GetFilesByEpisodeIds(CurrentEpisodeIds);

                    foreach(var episode in EpisodesData)
                    {
                        EpisodesWithFilesModel episodeWithFile = new EpisodesWithFilesModel();
                        episodeWithFile.Id = episode.Id;
                        episodeWithFile.SeasonId = episode.SeasonId;
                        episodeWithFile.Number = episode.Number;
                        episodeWithFile.Name = episode.Name;
                        episodeWithFile.RelaseDate = episode.RelaseDate;
                        episodeWithFile.Description = episode.Description;
                        episodeWithFile.ThumbnailLink = episode.ThumbnailLink;
                        EpisodesWithFilesData.Add(episodeWithFile);
                    }

                    if(AllFilesData != null && EpisodesData.Count > 0)
                    {
                        foreach(var episode in EpisodesWithFilesData)
                        {
                            var currentFiles = AllFilesData.Where(x => x.EpisodeId== episode.Id).ToList();
                            episode.Files = currentFiles;
                        }
                    }


                    return new APIResponse(ResponseCode.SUCCESS, "Records Found", EpisodesWithFilesData);
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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);

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



        public async Task<APIResponse> InsertUpdateFullTvShow(string IMDBId)
        {
            var tvShowFullData = await TheMovieDBManager.GetTvshowByIMDBId(IMDBId);
            var FullTVShowTMDBData = (FullTVShowTMDBModel)tvShowFullData.Document;

            var tvShowFullImageData = await TheMovieDBManager.GetImagesByIMDBId(IMDBId, "tv");
            var FullImagesTMDBModel = (List<FullImageTMDBModel>)tvShowFullImageData.Document;
            var AllCovers = CommonFunctions.TMDBCoverstoMyCovers(FullImagesTMDBModel);

            var TokenData = CommonFunctions.GetTokenData();
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);
                    SqlCoversDataAccess = new CoversDataAccess(ConnectionString, CommonFunctions);

                    var InsertedUpdatedTvShow = new vTvShowsModel();
                    var InsertedUpdatedTvShowSeasons = new List<tbSeasonsModel>();
                    var InsertedUpdatedTvShowEpisodes = new List<tbEpisodesModel>();

                    var VTvShowData = new vTvShowsViewModel()
                    {
                        PrimaryName = FullTVShowTMDBData.PrimaryName,
                        OtherNames = FullTVShowTMDBData.OtherNames,
                        Description = FullTVShowTMDBData.Description,
                        IMDBID = FullTVShowTMDBData.IMDBID,
                        ReleaseYear = FullTVShowTMDBData.ReleaseYear,
                        Genres = FullTVShowTMDBData.Genres
                    };
                    var VTvShowSeasonsData = new List<tbSeasonsViewModel>();
                    var VTvShowEpisodesData = new List<tbEpisodesViewModel>();

                    var TvShowExists = SqlTvShowsDataAccess.GetIfTvShowExistsByImdbID(IMDBId);
                    if (TvShowExists == null)
                    { InsertedUpdatedTvShow = SqlTvShowsDataAccess.InsertTvShows(VTvShowData); }
                    else
                    { InsertedUpdatedTvShow = SqlTvShowsDataAccess.UpdateTvShow(VTvShowData, TvShowExists.ShowId ?? new Guid()); }

                    foreach(var XSeason in FullTVShowTMDBData.Seasons)
                    {
                        var vSea = new tbSeasonsViewModel()
                        {
                            Name = XSeason.Name,
                            Number = XSeason.Number,
                            ShowId = InsertedUpdatedTvShow.ShowId
                        };
                        VTvShowSeasonsData.Add(vSea);
                    }

                    InsertedUpdatedTvShowSeasons = SqlTvShowsDataAccess.InsertUpdateTvShowSeasonMultiple(VTvShowSeasonsData);

                    foreach(var XSeason in InsertedUpdatedTvShowSeasons)
                    {
                        var curSeas = FullTVShowTMDBData.Seasons.Find(x => x.Number == XSeason.Number && x.Name == XSeason.Name);
                        foreach (var XEpisode in curSeas.Episodes)
                        {
                            var vEp = new tbEpisodesViewModel()
                            {
                                SeasonId = XSeason.Id,
                                Number = XEpisode.Number,
                                Name = XEpisode.Name,
                                RelaseDate = XEpisode.RelaseDate ?? new DateTime(),
                                Description= XEpisode.Description,
                                ThumbnailLink= XEpisode.ThumbnailLink,
                            };
                            VTvShowEpisodesData.Add(vEp);
                        }
                    }

                    InsertedUpdatedTvShowEpisodes = SqlTvShowsDataAccess.InsertUpdateTvShowEpisodesMultiple(VTvShowEpisodesData);
                    List<tbCoversModel> InsertedUpdatedCovers = new List<tbCoversModel>();
                    if (InsertedUpdatedTvShow.BreadId != null)
                    {
                        InsertedUpdatedCovers = SqlCoversDataAccess.InsertUpdateCoversFull(AllCovers, InsertedUpdatedTvShow.BreadId ?? new Guid());
                    }

                    dynamic FinalResult = new System.Dynamic.ExpandoObject();
                    FinalResult.InsertedUpdatedTvShow = InsertedUpdatedTvShow;
                    FinalResult.InsertedUpdatedTvShowSeasons = InsertedUpdatedTvShowSeasons;
                    FinalResult.InsertedUpdatedTvShowEpisodes = InsertedUpdatedTvShowEpisodes;
                    FinalResult.InsertedUpdatedCovers = InsertedUpdatedCovers;

                    return new APIResponse(ResponseCode.SUCCESS, "TvShow Updated", FinalResult);
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }

        public APIResponse InsertUpdateDeleteEpisodesFiles(List<tbShowsFileViewModel> ViewModel, Guid SeasonId)
        {
            switch (ServerType)
            {
                case "SQLServer":
                    SqlTvShowsDataAccess = new TvShowsDataAccess(ConnectionString, CommonFunctions);
                    SqlFilesDataAccess = new FilesDataAccess(ConnectionString, CommonFunctions);

                    var AllEpisodesIds = SqlTvShowsDataAccess.GetEpisodeIdsFromSeasonID(SeasonId);
                    var AllOldFilesForEpisode = SqlFilesDataAccess.GetFilesByEpisodeIds("'" + String.Join("','", AllEpisodesIds) + "'");

                    var ToDeleteShowFiles = AllOldFilesForEpisode.Where(x => !ViewModel.Select(y => y.Id).ToList().Contains(x.ShowFileId)).ToList();

                    var InsertedUpdated = SqlTvShowsDataAccess.InsertUpdateShowFiles(ViewModel);

                    if(ToDeleteShowFiles != null && ToDeleteShowFiles.Count > 0)
                    {
                        var DeleteSuccess = SqlTvShowsDataAccess.DeleteMultipleShowFiles("'" + String.Join("','", ToDeleteShowFiles.Select(x => x.ShowFileId).ToList()) + "'");
                    }

                    return new APIResponse(ResponseCode.SUCCESS, "Insert Update Delete Files Completed", InsertedUpdated);
                default:
                    return new APIResponse(ResponseCode.ERROR, "Invalid Database Type", ServerType);
            }
        }
    }
}

