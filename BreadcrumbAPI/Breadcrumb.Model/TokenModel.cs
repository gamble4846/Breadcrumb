using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breadcrumb.Model
{
    public class TokenModel
    {
        public List<Server> Servers { get; set; }
        public string TheMovieDBAPIKey { get; set; }
        public bool ShowNSFWCovers { get; set; }
        public bool ShowOnlyCustomCovers { get; set; }
        public List<GoogleAPI> GoogleAPIs { get; set;}
    }

    public class Server
    {
        public string DatabaseType { get; set; }
        public string ConnectionString { get; set; }
        public bool IsSelected { get; set; }
    }

    public class GoogleAPI
    {
        public string ApiKey { get; set; }
        public bool IsPrimary { get; set; }
    }
}
