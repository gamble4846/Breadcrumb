using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.vMoviesModels
{
    public class vMoviesModel
    {
        public Guid? BreadId { get; set; }
        public Guid? ShowId { get; set; }
        public string PrimaryName { get; set; }
        public string OtherNames { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsStared { get; set; }
        public string IMDBID { get; set; }
        public string ReleaseYear { get; set; }
        public string Genres { get; set; }
        public string GenreGUIDs { get; set; }
    }
}
