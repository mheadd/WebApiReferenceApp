using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using SoapHttpClient;
using SoapHttpClient.Extensions;
using SoapHttpClient.Enums;
using Newtonsoft.Json;

namespace WebApiReferenceApp.Controllers
{
    [Route("api/[controller]")]
    public class SoapController : Controller
    {
        private XNamespace ns = XNamespace.Get("http://helio.spdf.gsfc.nasa.gov/");
        private Uri endpoint = new Uri("http://sscweb.gsfc.nasa.gov:80/WS/helio/1/HeliocentricTrajectoriesService");
        private string methodName = "getAllObjects";

        // GET api/soap
        [HttpGet]
        public ContentResult Get()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(CallNasaAsync().Result);

            return Content(JsonConvert.SerializeObject(doc), "application/json");
        }
        private async Task<string> CallNasaAsync()
        {
            var body = new XElement(ns.GetName(methodName)) ;

            using (var soapClient = new SoapClient())
            {
                var response =
                  await soapClient.PostAsync(
                          endpoint: endpoint,
                          soapVersion: SoapVersion.Soap11,
                          body: body);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
