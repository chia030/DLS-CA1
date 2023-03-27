using System.Threading.Tasks;
using System.Data;
using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using MySqlConnector;

namespace SearchAPI.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        //maybe this is not needed
        // private IDbConnection searchDBConnection = new MySqlConnection("Server=search-db;Database=search-database;Uid=div-search;Pwd=s3@rchd1v;");
        // private readonly RestClient _restClient = new();
        // private readonly ILoadBalancer _loadBalancer = LoadBalancer.LoadBalancer.LoadBalancer.GetInstance();
        private static SearchLogic searchLogic = new SearchLogic(new Database());
    
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