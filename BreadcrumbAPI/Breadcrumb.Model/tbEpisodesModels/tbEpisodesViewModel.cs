using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.tbEpisodesModels
{
    public class tbEpisodesViewModel
    {
        public Guid? SeasonId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime RelaseDate { get; set; }
    }
}
