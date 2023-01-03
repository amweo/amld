using Amld.Platform.Models;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Amld.Platform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
     
        private readonly ILogger<HomeController> _logger;
        private readonly ElasticsearchClient elasticsearchClient;
        public HomeController(ILogger<HomeController> logger, ElasticsearchClient elasticsearchClient)
        {
            _logger = logger;
            this.elasticsearchClient = elasticsearchClient;
        }

        [HttpPost("SeedData")]
        public async Task<bool> SeedData()
        {

           var res = await  elasticsearchClient.IndexAsync(new LogEntry 
           {
               AppId= "amld.platform.web",
               ChainId = Guid.NewGuid().ToString("N")
           }, "my-tweet-index");
           return res.IsValidResponse;
        }

        [HttpGet("GetSeedData")]
        public async Task<LogEntry> GetSeedData()
        {

            var response = await elasticsearchClient.GetAsync<LogEntry>(1, idx => idx.Index("my-tweet-index"));
            return response.Source;
        }
    }
}