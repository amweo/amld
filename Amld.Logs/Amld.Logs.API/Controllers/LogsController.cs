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
        public IEnumerable<Message> All([FromQuery] AllReq listReq)
        { 
            throw new NotImplementedException();
        }

        //链路列表
        [HttpGet("chian-logs")]
        public IEnumerable<Message> ChainLogs([FromBody] ChainReq chainReq)
        {
            throw new NotImplementedException();
        }

        //日志详情
        [HttpGet("detail")]
        public Message Detail(string id)
        {
            throw new NotImplementedException();
        }

    }
}
