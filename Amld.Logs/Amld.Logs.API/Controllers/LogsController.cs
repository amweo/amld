using Amld.Logs.API.Models;
using Amld.Logs.API.Models.DTO;
using Amld.Logs.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Amld.Logs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        //查询列表
        [HttpGet("list")]
        public GR<List<LogMessage>> All([FromQuery] AllReq listReq)
        {
            return GR.Success(new List<LogMessage>());
        }

        //链路列表
        [HttpGet("chian-logs")]
        public GR<List<LogMessage>> ChainLogs([FromBody] ChainReq chainReq)
        {
            return GR.Success(new List<LogMessage>());
        }

        //日志详情
        [HttpGet("detail")]
        public GR<LogDetailMessage> Detail(string id)
        {
            return GR.Success(new LogDetailMessage());
        }

    }
}
