using Microsoft.AspNetCore.Http;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using System.Threading.Tasks;
using Breadcrumb.Model.FilesModels;

namespace Breadcrumb.Manager.Interface
{
    public interface ITvShowsManager
    {
        APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        APIResponse GetTvshowById(Guid ShowId);
        APIResponse InsertTvShow(vTvShowsViewModel ViewModel);
        APIResponse UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId);
        APIResponse DeleteTvShow(Guid ShowId);
        APIResponse GetAllTvshows();

        APIResponse GetTvShowSeasons(Guid ShowId);
        APIResponse InsertTvShowSeason(tbSeasonsViewModel ViewModel);
        APIResponse InsertUpdateTvShowSeasonMultiple(List<tbSeasonsViewModel> ViewModelList);
        APIResponse UpdateTvShowSeasons(tbSeasonsViewModel ViewModel, Guid SeasonId);
        APIResponse DeleteTvShowSeasons(Guid SeasonId);

        APIResponse GetTvShowEpisodes(Guid SeasonId);
        APIResponse GetTvShowEpisodesWithFiles(Guid SeasonId);
        APIResponse InsertTvShowEpisodes(tbEpisodesViewModel ViewModel);
        APIResponse InsertUpdateTvShowEpisodesMultiple(List<tbEpisodesViewModel> ViewModelList);
        APIResponse UpdateTvShowEpisodes(tbEpisodesViewModel ViewModel, Guid EpisodeId);
        APIResponse DeleteTvShowEpisodes(Guid EpisodeId);

        Task<APIResponse> InsertUpdateFullTvShow(string IMDBId);
        APIResponse InsertUpdateDeleteEpisodesFiles(List<tbShowsFileViewModel> ViewModelStrings, Guid SeasonId);
    }
}

