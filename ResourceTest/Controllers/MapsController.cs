using Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResourceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        private List<Map> _maps = new List<Map>();

        // GET: api/<MapsController>
        [HttpGet]
        public ActionResult<List<Map>> Get()
        {
            var map1 = new Map
            {
                _id = "6892d2b5b135d525c72aea6d",
                mapId = "mirconst-guid-0000-0001-maps00000000",
                source = "mir",
                __v = 0,
                createdAt = DateTime.Now,
                imageId = "6893f3883413fa91c0060380",
                level = 1,
                name = "ConfigurationMap",
                originTheta = 0,
                originX = -12.5,
                originY = -12.5,
                resolution = 0.05,
                updatedAt = DateTime.Now,
            };
            var map2 = new Map
            {
                _id = "6892d2b5b135d525c72aea6e",
                mapId = "38fcd151-0dfd-11f0-ab26-94c6911adcd7",
                source = "mir",
                __v = 0,
                createdAt = DateTime.Now,
                imageId = "6893f3883413fa91c006037e",
                level = 1,
                name = "사내테스트",
                originTheta = 0,
                originX = 0,
                originY = 0,
                resolution = 0,
                updatedAt = DateTime.Now,
            };
            _maps.Add(map1);
            _maps.Add(map2);

            return _maps;
        }

        //// GET api/<MapsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<MapsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<MapsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MapsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}