using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Entities;
using TodoWebApi.Models;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoWebController : ControllerBase
    {

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
        public async Task<ActionResult<List<TodoWeb>>> AddTodo(TodoModel todo)
        {
            var entity = new TodoWeb
            {
                Title = todo.Title,
                Description = todo.Description,
                Createdby = todo.Createdby
            };
            _context.TodoWebs.Add(entity);
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
