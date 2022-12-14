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
        public async Task<string> SeedData([FromBody] string name)
        {

            //var res = await  elasticsearchClient.IndexAsync(new LogEntry 
            //{
            //    AppId= "amld.platform.web",
            //    ChainId = Guid.NewGuid().ToString("N")
            //}, "my-tweet-index");
            //return res.IsValidResponse;
            _logger.LogInformation("666666666666666666");
            _logger.LogWarning("888888");
            _logger.LogError("dddddddddddddddd");
            return "hello my log";
        }
        public class MyClass
        {
            public string Name { get; set; }
        }
    }
}