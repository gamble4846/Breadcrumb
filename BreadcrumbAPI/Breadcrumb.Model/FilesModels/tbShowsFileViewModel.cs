using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.FilesModels
{
    public class tbShowsFileViewModel
    {
        public Guid? Id { get; set; }
        public Guid? FileId { get; set; }
        public Guid? ShowId { get; set; }
        public Guid? SeasonId { get; set; }
        public Guid? EpisodeId { get; set; }
        public String Quality { get; set; }
        public String AudioLanguages { get; set; }
        public String SubtitleLanguages { get; set; }
    }
}
