using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.Sentinel.Services;

namespace Redis.Sentinel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("set")]
        public IActionResult SetValue(string key, string value)
        {
            var redis = RedisService.RedisMasterDB();
            redis.StringSet(key, value);
            return Ok();
        }
        [HttpGet("get")]
        public IActionResult GetValue(string key)
        {
            var redis = RedisService.RedisMasterDB();
            var data = redis.StringGet(key);
            return Ok(data.ToString());
        }
    }
}
