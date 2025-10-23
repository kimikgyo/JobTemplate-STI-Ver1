using Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResourceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private List<Worker> _workers = new List<Worker>();

        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<Worker>> Get()
        {

            var WorkerAdd_1 = new Worker
            {
                _id = "6892f625b135d525c72aea71",
                source = "mir",
                workerId = "1",
                __v = 0,
                capabilities = new List<capabilities>(),
                createdAt = DateTime.Now,
                createdBy = "SYSTEM_SYNC",
                ipAddress = "192.168.1.32",
                isActive = true,
                isOccupied = false,
                isOnline = true,
                loginId = "",
                modelName = "Unknown",
                password = ""
            };
            var WorkerAdd_2 = new Worker
            {
                _id = "6892f625b135d525c72aea72",
                source = "mir",
                workerId = "2",
                __v = 0,
                capabilities = new List<capabilities>(),
                createdAt = DateTime.Now,
                createdBy = "SYSTEM_SYNC",
                ipAddress = "192.168.1.34",
                isActive = true,
                isOccupied = false,
                isOnline = true,
                loginId = "",
                modelName = "Unknown",
                password = ""
            };
            _workers.Add(WorkerAdd_1);
            _workers.Add(WorkerAdd_2);
            return _workers;
        }

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public ActionResult<Worker> Get(string id)
        //{
        //    return workers.FirstOrDefault(m=>m._id== id);
        //}

        // POST api/<ValuesController>
        [HttpPost("mission_queue")]
        public ActionResult Post([FromBody] string value)
        {
            var Test = value;
            return Ok("Crate");
        }

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}