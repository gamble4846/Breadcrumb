using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface ITvShowsDataAccess
    {
        dynamic GetTvShows(int page, int itemsPerPage, string orderBy, string FilterQuery);
        vTvShowsModel GetTvshowById(Guid ShowId);
        vTvShowsModel InsertTvShows(vTvShowsViewModel ViewModel);
        vTvShowsModel UpdateTvShow(vTvShowsViewModel ViewModel, Guid ShowId);
        vTvShowsModel DeleteTvShow(Guid ShowId);
        vTvShowsModel GetIfTvShowExistsByImdbID(string IMDBId);
        List<vTvShowsModel> GetAllTvshows();

        List<tbSeasonsModel> GetTvShowSeasons(Guid ShowId);
        tbSeasonsModel InsertTvShowSeason(tbSeasonsViewModel ViewModel);
        List<tbSeasonsModel> InsertUpdateTvShowSeasonMultiple(List<tbSeasonsViewModel> ViewModelList);
        tbSeasonsModel UpdateTvShowSeasons(tbSeasonsViewModel ViewModel, Guid SeasonId);
        tbSeasonsModel DeleteTvShowSeasons(Guid SeasonId);

        List<tbEpisodesModel> GetTvShowEpisodes(Guid SeasonId);
        tbEpisodesModel InsertTvShowEpisodes(tbEpisodesViewModel ViewModel);
        List<tbEpisodesModel> InsertUpdateTvShowEpisodesMultiple(List<tbEpisodesViewModel> ViewModelList);
        tbEpisodesModel UpdateTvShowEpisodes(tbEpisodesViewModel ViewModel, Guid EpisodeId);
        tbEpisodesModel DeleteTvShowEpisodes(Guid EpisodeId);
    }
}

