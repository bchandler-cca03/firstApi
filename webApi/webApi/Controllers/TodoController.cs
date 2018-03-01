using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Data;
using webApi.Models;

namespace webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            // if there are no items in the array, initialize with 1 item
            if(!_context.TodoItems.Any())
            {
                _context.TodoItems.Add(new Models.TodoItem { Name = "Item 1" });
                _context.SaveChanges();
            }
        }

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            return _context.TodoItems.ToList();
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(t=> t.Id == id);
            if(item == null)
            {
                return NotFound();   // returns the 404 status code
            }

            return new ObjectResult(item);
;        }
        
        // POST: api/Create
        [HttpPost]
        public IActionResult Create([FromBody]TodoItem item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            // creates a route in the database to get back to the object
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);

        }
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            // an item is being passed in, and an id; if they don't match, bad-request
            if(item == null || item.Id != id)
            {
                return BadRequest();
            }
            var todo = _context.TodoItems.FirstOrDefault(t=>t.Id == id);
            // we determine if the item is a null
            if(todo == null)
            {
                return NotFound();
            }
            // if ok, map the item, update the context, and then save to the in-memory database
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return new NoContentResult();
            // restart at 1:44
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
