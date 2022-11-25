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


        public async Task<IEnumerable<Message>> GetMessages(AllReq req)
        {
           var search = await client.SearchAsync<Message>(x => x.From((req.PageIndex - 1) * req.PageSize));
           return search.Documents;
        }
    }
}
