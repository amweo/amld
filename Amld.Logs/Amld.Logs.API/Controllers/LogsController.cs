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
        public GR<List<Message>> All([FromQuery] AllReq listReq)
        {
            return GR.Success(new List<Message>());
        }

        //链路列表
        [HttpGet("chian-logs")]
        public GR<List<Message>> ChainLogs([FromBody] ChainReq chainReq)
        {
            return GR.Success(new List<Message>());
        }

        //日志详情
        [HttpGet("detail")]
        public GR<Message> Detail(string id)
        {
            return GR.Success(new Message());
        }

    }
}
