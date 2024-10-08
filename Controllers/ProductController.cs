using Microsoft.AspNetCore.Mvc;
using practices.CQRS.Commands;
using practices.Model;
using practices.CQRS.Queries;  
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace practices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly CommandHandler _commandHandler;
        private readonly QueryHandler _queryHandler;
        public ProductController(CommandHandler commandHandler, QueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler= queryHandler;
        }
        [HttpGet]
       // [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Getall()
        {
            var product = await _queryHandler.HandleGetall();
            return Ok(product); 
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var product = await _queryHandler.HandleGetById(query);
            return Ok(product);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            await _commandHandler.Handlecreate(command);
            return Ok(true);
        }
        [HttpPut]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            await _commandHandler.Handleupdate(command);
            return Ok(true);
        }
        [HttpDelete("{id}")]
       //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _commandHandler.Handledelete(command);
            return Ok(true);
        }
    }
}
