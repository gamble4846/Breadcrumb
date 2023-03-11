using Microsoft.AspNetCore.Http;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using System.Threading.Tasks;

namespace Breadcrumb.Manager.Interface
{
    public interface ICoversManager
    {
        APIResponse GetCoverByBreadId(Guid BreadId);
        APIResponse GetCoverByBreadIds(List<Guid> BreadIds);
    }
}

