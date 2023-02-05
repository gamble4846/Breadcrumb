using Breadcrumb.Model;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface IShowsDataAccess
    {
        List<ShowsModel> GetShows(int page, int itemsPerPage, List<OrderByModel> orderBy, string FilterQuery, string Type);
        int GetShowsTotal(string FilterQuery, string Type);
    }
}

