using Amld.Platform.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Amld.Platform.Repoitories
{
    public class LogEntryRepoitory
    {
        private readonly ElasticsearchClient elasticsearch;

        public LogEntryRepoitory(ElasticsearchClient elasticsearch)
        {
            this.elasticsearch = elasticsearch;
        }


        public async Task<bool> Add(LogEntry logEntry)
        {
            var c = new QueryDescriptor<LogEntry>();

            if (!string.IsNullOrEmpty(logEntry.AppId))
            {
                //var q = new TermQuery(new Field { }) //{ Field = "AppId", Value = logEntry.AppId };
                //c =  c && new QueryDescriptor<LogEntry>().(f => f.AppId, logEntry.AppId);
            }
            var res = await elasticsearch.IndexAsync(logEntry);
            return res.IsValidResponse;
        }
        public (int count, IEnumerable<LogEntry>) Query(LogEntryReq req)
        {
            return (0,new List<LogEntry> { });
        }

        //public LogEntry
    }
}
