using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amld.Extensions.Logging.AspNetCore
{
    internal class RequestEntry:LogEntry
    {
        public string Url { get; set; }
        public List<string> Headers { get; set; }
        public string Body { get; set; }
    }
}
