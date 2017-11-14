using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiReferenceApp.Connectors;

namespace WebApiReferenceApp.Controllers
{
    [Route("api/[controller]")]
    public class SoapController : Controller
    {
        // Details of NASA's NASA's Heliocentric Trajectories SOAP Service
        private string methodName = "getAllObjects";
        private ISoapConnector _connector;
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

        /**
         * Private method to invoke SoapConnector and make service call.       
        */
        private string GetSoapResponse()
        {
            var response = _connector.CallServiceAsync(methodName);
            return response.Result;
        }
    }
}
