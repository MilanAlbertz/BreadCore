using Microsoft.AspNetCore.Mvc;
using BreadCore.Models;
using BreadCore;
using BreadCore.Controllers;
using BreadCore;
using BreadCore.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreadcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // GET: api/<ToDoListController>
        // GET: api/<ToDoListController>
        [HttpGet]
        public IEnumerable<BreadCore.Controllers.BroodController> Get()
        {
            return BreadCore.ReadAll();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ToDoListController>/5
        [HttpGet("{beginDatum, eindDatum}")]
        public List<int?> Get(DateTime beginDatum, DateTime eindDatum)
        {
            List<BroodType> broodTypes = new List<BroodType>();
            List<Brood> broden = new List<Brood>();

            foreach (var brood in database.Brood
                .Where(d => d.TijdGebakken >= beginDatum)
                .Where(d => d.TijdGebakken <= eindDatum)
                .Include(d => d.BroodType))
            {
                broden.Add(brood);
                if (!broodTypes.Contains(brood.BroodType))
                {
                    broodTypes.Add(brood.BroodType);
                }
            }

            return ToDoTask.Read(id);
        }

        // POST api/<ToDoListController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ToDoListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string assignedTo)
        {
            var task = ToDoTask.Read(id);
            task.AssignPerson(assignedTo);
        }

        // DELETE api/<ToDoListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
