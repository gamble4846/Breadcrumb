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
    public interface IGoogleApiManager
    {
        Task<APIResponse> GetFilesFromFolderId(string FolderID);
    }
}

