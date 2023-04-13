using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model.FilesModels
{
    public class tbFileDataChunksViewModel
    {
        public Guid FileDataID { get; set; }
        public string Email { get; set; }
        public decimal Size { get; set; }
        public string Password { get; set; }
        public string Link { get; set; }
        public string OtherData { get; set; }
    }
}
