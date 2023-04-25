using Breadcrumb.Model.tbCoversModels;
using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface ICoversDataAccess
    {
        List<tbCoversModel> GetCoverByBreadId(Guid BreadId);
        List<tbCoversModel> GetCoverByBreadIds(string BreadIds);
        bool InsertUpdateDeleteCoversForSingleBread(List<string> CoversToInsertQueries, List<string> CoversToUpdateQueries, string CoversToDeleteQuery);
        List<tbCoversModel> InsertUpdateCoversFull(List<tbCoversModel> covers, Guid BreadId);
    }
}

