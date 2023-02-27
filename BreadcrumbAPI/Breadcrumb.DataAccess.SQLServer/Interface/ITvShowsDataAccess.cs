using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface ITvShowsDataAccess
    {
        dynamic GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        vTvShowsModel InsertTvShows(vTvShowsViewModel ViewModel);
        vTvShowsModel UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId);
        vTvShowsModel DeleteTvShow(Guid ShowId);
        List<tbSeasonsModel> GetTvShowSeasons(Guid ShowId);
    }
}

