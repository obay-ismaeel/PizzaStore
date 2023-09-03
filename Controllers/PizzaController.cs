using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using PizzaStore.Services;

namespace PizzaStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        // GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> Index() => PizzaService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if(pizza == null)
                return NotFound();

            return pizza;
        }

        // POST action

        // PUT action

        // DELETE action
    }
}
