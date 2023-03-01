using Microsoft.AspNetCore.Http;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;

namespace Breadcrumb.Manager.Interface
{
    public interface ITvShowsManager
    {
        APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        APIResponse InsertTvShow(vTvShowsViewModel ViewModel);
        APIResponse UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId);
        APIResponse DeleteTvShow(Guid ShowId);

        APIResponse GetTvShowSeasons(Guid ShowId);
        APIResponse InsertTvShowSeason(tbSeasonsViewModel ViewModel);
        APIResponse UpdateTvShowSeasons(tbSeasonsViewModel ViewModel, Guid SeasonId);
        APIResponse DeleteTvShowSeasons(Guid SeasonId);

        APIResponse GetTvShowEpisodes(Guid SeasonId);
        APIResponse InsertTvShowEpisodes(tbEpisodesViewModel ViewModel);
    }
}

