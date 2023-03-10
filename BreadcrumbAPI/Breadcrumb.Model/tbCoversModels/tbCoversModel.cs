using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.tbCoversModels
{
    public class tbCoversModel
    {
        public Guid? Id { get; set; }
        public Guid? BreadId { get; set; }
        public string Link { get; set; }
        public string Dimensions { get; set; }
        public bool isNSFW { get; set; }
    }
}
