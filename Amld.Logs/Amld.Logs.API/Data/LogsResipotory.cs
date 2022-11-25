using Amld.Logs.API.Models.DTO;
using Amld.Logs.API.Models.Entities;
using Nest;

namespace Amld.Logs.API.Data
{
    public class LogsResipotory
    {

        private readonly IElasticClient client;

        public LogsResipotory(IElasticClient elasticClient)
        {
            this.client = elasticClient;
        }


        public async Task<IEnumerable<LogMessage>> GetMessages(AllReq req)
        {
            var mustQuerys = new List<Func<QueryContainerDescriptor<LogMessage>, QueryContainer>>();
            if (!string.IsNullOrEmpty(req.AppId))
            {
                mustQuerys.Add(t => t.Term(f => f.AppId, req.AppId));
            }
            if (req.LogLevel>0)
            {
                mustQuerys.Add(t => t.Term(f => f.LogLevel, req.LogLevel));
            }

            if (req.StartTime.HasValue && req.EndTime.HasValue)
            {

            }
            var search = await client.SearchAsync<LogMessage>(x => x.Query(
                q => q.Bool(b => b.Must(mustQuerys))
                ));

           
           return search.Documents;
        }
    }
}
