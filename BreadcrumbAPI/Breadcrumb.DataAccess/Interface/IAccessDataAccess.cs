using Breadcrumb.Model;
using Breadcrumb.Utility;
using System.Collections.Generic;

namespace Breadcrumb.DataAccess.Interface
{
    public interface IAccessDataAccess
    {
        List<AccessModel> GetAllAccess(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<AccessModel> SearchAccess(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<AccessModel> FilterAccess(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        int GetFilterTotalRecordAccess(List<FilterModel> filterBy,string andOr);
        int GetAllTotalRecordAccess();
        int GetSearchTotalRecordAccess(string searchKey);
    }
}

