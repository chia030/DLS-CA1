using System.Threading.Tasks;
using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace SearchAPI.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        private static SearchLogic searchLogic = new SearchLogic(new Database());
        private readonly RestClient _restClient = new();
        private readonly ILoadBalancer _loadBalancer = LoadBalancer.LoadBalancer.LoadBalancer.GetInstance();


        // [HttpGet]
        // public async Task<ActionResult<int>> Get(long number)
        // {
        //     var result = await CallService<int>("/Cache?number=" + number, Method.Get);
        //     return result;
        // }
        //
        // [HttpPost]
        // public async Task<ActionResult<object>> Post([FromQuery] long number, [FromQuery] int divisorCounter)
        // {
        //     return await CallService<object>("/Cache?number=" + number + "&divisorCounter=" + divisorCounter, Method.Post);
        // }
        //
        // private async Task<ActionResult<TResult>> CallService<TResult>(
        //     string url, Method method
        // )
        // {
        //     var service = _loadBalancer.NextService();
        //     if (service == null)
        //     {
        //         return StatusCode(503);
        //     }
        //
        //     var result = await _restClient.ExecuteAsync<TResult>(
        //         new RestRequest(service.Url + url), method
        //     );
        //     int statusCode = (int)result.StatusCode;
        //     if (statusCode is 0 or >= 500)
        //     {
        //         Console.WriteLine("Service at URL " + service.Url + "returned status code " + (int)result.StatusCode + " and will be removed.");
        //         _loadBalancer.RemoveService(service.Id);
        //         return await CallService<TResult>(url, method);
        //     }
        //
        //     return Ok(result.Data);
        // }

        [HttpGet]
        [Route("{query}/{maxAmount}")]
        public async Task<string> SearchByQuery(string query, int maxAmount)
        {
            var result = await searchLogic.Search(query.Split(","), maxAmount);
            var resultStr = JsonConvert.SerializeObject(result, Formatting.Indented);
            return resultStr; 
        }
    }
}