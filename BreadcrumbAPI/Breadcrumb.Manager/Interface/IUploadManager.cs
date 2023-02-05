using Microsoft.AspNetCore.Http;
using Breadcrumb.Model;
using Breadcrumb.Utility;
using System.Collections.Generic;

namespace Breadcrumb.Manager.Interface
{
    public interface IUploadManager
    {
        APIResponse UploadImages(List<IFormFile> images);
    }
}

