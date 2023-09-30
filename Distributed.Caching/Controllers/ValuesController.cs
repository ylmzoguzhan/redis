using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Distributed.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly private IDistributedCache _distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpPost]
        public void Set(string key, string value)
        {
            _distributedCache.SetString(key, value, options: new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }
        [HttpGet]
        public string Get(string key)
        {
            return _distributedCache.GetString(key);
        }
    }
}
