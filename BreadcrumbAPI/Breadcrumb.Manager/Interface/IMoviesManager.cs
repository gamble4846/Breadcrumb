using Microsoft.AspNetCore.Http;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using System.Threading.Tasks;
using Breadcrumb.Model.vMoviesModels;

namespace Breadcrumb.Manager.Interface
{
    public interface IMoviesManager
    {
        APIResponse GetMovies(int page, int itemsPerPage, string orderBy, string FilterQuery);
        APIResponse GetMovieById(Guid ShowId);
        APIResponse InsertMovie(vMoviesViewModel ViewModel);
        APIResponse UpdateMovie(vMoviesViewModel ViewModel, Guid ShowId);
        Task<APIResponse> InsertUpdateFullMovie(string IMDBId);
        APIResponse DeleteMovie(Guid ShowId);
    }
}

