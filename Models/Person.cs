using System.ComponentModel.DataAnnotations;

namespace WebApiReferenceApp.Models
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string enabled { get; set; }
    }
}