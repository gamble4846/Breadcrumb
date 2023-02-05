using Microsoft.AspNetCore.Http;
using Breadcrumb.Model;
using Breadcrumb.Utility;
using System.Collections.Generic;

namespace Breadcrumb.Manager.Interface
{
    public interface IShowsManager
    {
        APIResponse GetShows(int page, int itemsPerPage, List<OrderByModel> orderBy, string FilterQuery, string Type);
    }
}

