using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SearchAPI.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
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