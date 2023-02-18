using Microsoft.AspNetCore.Http;
using Breadcrumb.Model;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;

namespace Breadcrumb.Manager.Interface
{
    public interface ITvShowsManager
    {
        APIResponse GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        APIResponse Insert(vTvShowsViewModel ViewModel);
        APIResponse Update(vTvShowsViewModel ViewModel, Guid ShowId);
        APIResponse Delete(Guid ShowId);
    }
}

