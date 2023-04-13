using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.FilesModels
{
    public class FullShowFilesWithChunksModel
    {
        public Guid ShowFileId { get; set; }
        public Guid FileId { get; set; }
        public String Description { get; set; }
        public String Type { get; set; }
        public String Name { get; set; }
        public String ThumbnailLink { get; set; }
        public Guid? ShowId { get; set; }
        public Guid? SeasonId { get; set; }
        public Guid? EpisodeId { get; set; }
        public String Quality { get; set; }
        public String AudioLanguages { get; set; }
        public String SubtitleLanguages { get; set; }
        public List<tbFileDataChunksModel> Chunks { get; set; }
    }
}
