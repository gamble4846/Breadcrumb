using Breadcrumb.Model;
using Breadcrumb.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breadcrumb.Manager.Interface
{
    public interface IAccessManager
    {
        APIResponse GetAccess(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchAccess(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterAccess(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
    }
}

