using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.GoogleApiModels
{
    public class FolderAPI
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<FilesApi> Files { get; set; }
        public List<FolderAPI> Folders { get; set; }
    }

    public class FilesApi
    {
        public string Name { get; set; }
        public string ThumbnailLink { get; set; }
        public string Type { get; set; }
        public decimal? Size { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string OtherData { get; set; }
    }

    public class FinalFile
    {
        public List<FilesApi> FileChunks { get; set; }
        public string Name { get; set; }
        public string ThumbnailLink { get; set; }
        public string Type { get; set; }
    }
}
