using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApiReferenceApp.Controllers
{
    [Route("api/[controller]")]
    public class RestController : Controller
    {
        // Data.gov API endpoint
        private Uri endpoint = new Uri("https://catalog.data.gov");

        // GET api/rest
        [HttpGet("search")]
        public ContentResult Get()
        {
            string response = CallDataDotGov("/api/3/action/package_search");
            return Content(response, "application/json");
        }

        [HttpGet("details")]
        public ContentResult Get(string id)
        {
            string response = CallDataDotGov("/api/3/action/package_show?id=" + id);
            return Content(response, "application/json");
        }

        private string CallDataDotGov(string path)
        {
            JObject result = JObject.Parse(CallServiceAsync(path).Result);
            return result.GetValue("result").ToString();
        }
        
        private async Task<string> CallServiceAsync(string path)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = endpoint;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetStringAsync(path);
        }
    }

}