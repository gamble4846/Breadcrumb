using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.vMoviesModels;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface IMoviesDataAccess
    {
        dynamic GetMovies(int page, int itemsPerPage, string orderBy, string FilterQuery);
        vMoviesModel GetMovieById(Guid ShowId);
        vMoviesModel InsertMovie(vMoviesViewModel ViewModel);
        vMoviesModel UpdateMovie(vMoviesViewModel ViewModel, Guid ShowId);
        vMoviesModel GetIfMovieExistsByImdbID(string IMDBId);
    }
}

