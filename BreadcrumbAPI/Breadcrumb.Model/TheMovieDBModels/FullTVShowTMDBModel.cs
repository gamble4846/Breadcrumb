using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.TheMovieDBModels
{
    public class FullTVShowTMDBModel
    {
        public string PrimaryName { get; set; }
        public string OtherNames { get; set; }
        public string Description { get; set; }
        public string IMDBID { get; set; }
        public string ReleaseYear { get; set; }
        public string Genres { get; set; }
        public List<SeasonTMDBModel> Seasons { get; set; }
    }

    public class SeasonTMDBModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string TMDBId { get; set; }
        public List<EpisodeTMDBModel> Episodes { get; set; }
        public string Description { get; set; }
    }

    public class EpisodeTMDBModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime? RelaseDate { get; set; }
        public string Description { get; set; }
        public string ThumbnailLink { get; set; }
    }
}
