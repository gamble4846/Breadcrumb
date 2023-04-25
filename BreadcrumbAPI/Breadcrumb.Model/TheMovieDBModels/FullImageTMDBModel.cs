using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.TheMovieDBModels
{
    public class FullImageTMDBModel
    {
        public string AspectRatio { get; set; } //aspect_ratio
        public string Height { get; set; } //height
        public string ISO6391 { get; set; } //iso_639_1
        public string FilePath { get; set; } //file_path
        public string VoteAverage { get; set; } //vote_average
        public string VoteCount { get; set; } //vote_count
        public string Width { get; set; } //width
    }
}
