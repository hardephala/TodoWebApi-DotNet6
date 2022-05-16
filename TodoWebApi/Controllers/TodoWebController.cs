using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoWebController : ControllerBase
    {
        private static List<TodoWeb> todos = new List<TodoWeb> {
                new TodoWeb { 
                    Id = 1, Title = "Learn C sharp", 
                    Description = "Build a Test API in C# to demo to Olugbenga", 
                    Createdby = "Taiwo Adefala", 
                    Createdat = DateTime.Now
                },
                new TodoWeb {
                    Id = 2, Title = "Develop API for OMS",
                    Description = "Build Endpoints to be consumed by frontend team",
                    Createdby = "Taiwo Adefala",
                    Createdat = DateTime.Now
                }
        };

        private readonly DataContext _context;

        public TodoWebController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoWeb>>> Get()
        {
            return Ok(await _context.TodoWebs.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoWeb>> Get(int id)
        {
            var todo = await _context.TodoWebs.FindAsync(id);
            if(todo == null)
               return BadRequest("Todo Was Never created");
            return Ok(todo);
        }
        [HttpPost]
        public async Task<ActionResult<List<TodoWeb>>> AddTodo(TodoWeb todo)
        {
            _context.TodoWebs.Add(todo);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoWebs.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<TodoWeb>>> UpdateTodo(TodoWeb request)
        {
            var dbTodo = await _context.TodoWebs.FindAsync(request.Id);
            if (dbTodo == null)
                return BadRequest("Todo Was Never created");

            dbTodo.Title = request.Title;
            dbTodo.Description = request.Description;
            dbTodo.Createdby = request.Createdby;
            dbTodo.Createdat = request.Createdat;

            await _context.SaveChangesAsync();

            return Ok(await _context.TodoWebs.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TodoWeb>>> Delete(int id)
        {
            var dbTodo = await _context.TodoWebs.FindAsync(id);
            if (dbTodo == null)
                return BadRequest("Todo Was Never created");

            _context.TodoWebs.Remove(dbTodo);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoWebs.ToListAsync());
        }
    }
}
