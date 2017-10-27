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
        // Details of NASA's NASA's Heliocentric Trajectories SOAP Service
        private XNamespace ns = XNamespace.Get("http://helio.spdf.gsfc.nasa.gov/");
        private Uri endpoint = new Uri("http://sscweb.gsfc.nasa.gov:80/WS/helio/1/HeliocentricTrajectoriesService");
        private string methodName = "getAllObjects";

        // GET api/soap
        [HttpGet]
        public ContentResult Get()
        {
            // Create a new XML document to hold the response from the SOAP service.
            XmlDocument doc = new XmlDocument();

            // Load the response from the SOAP service.
            doc.LoadXml(CallNasaAsync().Result);

            // Convert the XML document to JSON format.
            return Content(JsonConvert.SerializeObject(doc), "application/json");
        }
        /**
         * Private method to call SOAP service.
         */
        private async Task<string> CallNasaAsync()
        {
            // Construct the SOAP body
            var body = new XElement(ns.GetName(methodName));

            // Make the call to the SOAP service.
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
