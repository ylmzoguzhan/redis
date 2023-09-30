using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly private IMemoryCache _memoryCache;
        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        //[HttpPost]
        //public void Set(string name)
        //{
        //    _memoryCache.Set("role", name);
        //}
        //[HttpGet]
        //public string Get()
        //{
        //    return _memoryCache.Get<string>("role");
        //}
        //[HttpPost("remove")]
        //public void Remove(string key)
        //{
        //    _memoryCache.Remove(key);
        //}
        [HttpGet("set")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }
        [HttpGet("get")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
