using Microsoft.AspNetCore.Http;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;
using Breadcrumb.Model.vTvShowsModels;

namespace Breadcrumb.Manager.Interface
{
    public interface ITvShowsManager
    {
        APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        APIResponse InsertTvShow(vTvShowsViewModel ViewModel);
        APIResponse UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId);
        APIResponse DeleteTvShow(Guid ShowId);
        APIResponse GetTvShowSeasons(Guid ShowId);
    }
}

