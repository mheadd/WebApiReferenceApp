using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace WebApiReferenceApp.Controllers
{
    [Route("api/[controller]")]
    public class SqlController : Controller
    {
        private string connectionString = "Server=localhost,1401;Database=TestDB;User ID=SA;Password=<YourStrong!Passw0rd>";


        [HttpGet]
        public ContentResult Get()
        {
            string sqlQuery = "SELECT * FROM people";
            string response = CallSqlServer(sqlQuery).ToString();
            return Content(response, "application/json");

        }

        [HttpGet("find")]
        public ContentResult Get(int id)
        {
            string sqlQuery = String.Format("SELECT * FROM people WHERE id = {0}", id);
            string response = CallSqlServer(sqlQuery).ToString();
            return Content(response, "application/json");
        }

        private JArray CallSqlServer(string query)
        {
            JArray resultArrray = new JArray();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string resultTemplate = @"{{""id"": ""{0}"", ""name"": ""{1}"", ""enabled"": ""{2}""}}";
                        JObject result = JObject.Parse(String.Format(resultTemplate, reader[0], reader[1], reader[2]));
                        resultArrray.Add(result);
                    }
                }
                return resultArrray;
            }
        }
    }
}