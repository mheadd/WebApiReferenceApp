using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiReferenceApp.Connectors;

namespace WebApiReferenceApp.Controllers
{
    [Route("api/[controller]")]
    public class SoapController : Controller
    {
        private ISoapConnector _connector;
        private string methodName = "getAllObjects";
        public SoapController(ISoapConnector connector)
        {
            _connector = connector;
        }

        // GET api/soap
        [HttpGet]
        public ContentResult Get()
        {
            // Create a new XML document to hold the response from the SOAP service.
            XmlDocument doc = new XmlDocument();

            // Load the response from the SOAP service.
            doc.LoadXml(GetSoapResponse());

            // Convert the XML document to JSON format.
            return Content(JsonConvert.SerializeObject(doc), "application/json");
        }

        private string GetSoapResponse()
        {
            var response = _connector.CallServiceAsync(methodName);
            return response.Result;
        }
    }
}
