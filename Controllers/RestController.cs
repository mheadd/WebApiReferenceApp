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
        // Data.gov API endpoint.
        private Uri endpoint = new Uri("https://catalog.data.gov");
        // Path to list packages.
        private string packageSearch = "/api/3/action/package_search";
        // Path to show package details.
        private string packageDetails = "/api/3/action/package_show?id={0}";

        // GET api/rest
        [HttpGet("search")]
        public ContentResult Get()
        {
            string response = GetPackageData(packageSearch);
            return Content(response, "application/json");
        }

        [HttpGet("details")]
        public ContentResult Get(string id)
        {
            string path = String.Format(packageDetails, id);
            string response = GetPackageData(path);
            return Content(response, "application/json");
        }

        private string GetPackageData(string path)
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