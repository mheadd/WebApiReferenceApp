using Microsoft.AspNetCore.Mvc;
using WebApiReferenceApp.Models;
using System.Linq;

namespace WebApiReferenceApp.Controllers
{
    [Route("api/[controller]")]
    public class SqlController : Controller
    {
        private readonly ApiContext _context;

        public SqlController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _context.People.ToList();
            return Ok(new { People = model });
        }

        [HttpGet("find")]
        public IActionResult Get(int id)
        {
            var model = _context.People.Find(id);
            return Ok(new { Person = model });
        }
    }
}