using Breadcrumb.Model;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface ITvShowsDataAccess
    {
        dynamic GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        vTvShowsModel InsertTvShows(vTvShowsViewModel ViewModel);
        vTvShowsModel Update(vTvShowsViewModel ViewModel, Guid ShowId);
    }
}

