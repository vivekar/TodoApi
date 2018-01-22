using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers {
    [Route ("api/[controller]")]
    public class TodoController : Controller {
        private readonly TodoContext _context;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">TodoContext</param>
        public TodoController (TodoContext context) {
            _context = context;

            if (_context.TodoItems.Count () == 0) {
                _context.TodoItems.Add (new TodoItem { Name = "First Item" });
                _context.SaveChanges ();
            }

        }

        #endregion

        /// <summary>
        /// This method will get all the items in the Todo List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TodoItem> GetAll () {
            return _context.TodoItems.ToList ();
        }

        /// <summary>
        /// This method is used to get the Todo list item by Id.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        [HttpGet ("{id}", Name = "GetTodo")]
        public IActionResult GetById (long id) {
            var item = _context.TodoItems.FirstOrDefault (t => t.Id == id);
            if (item == null) {
                return NotFound ();
            } else {
                return new ObjectResult (item);
            }
        }

        /// <summary>
        /// This creates a new item to the Todo list.
        /// </summary>
        /// <param name="item">Object TodoItem</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create ([FromBody] TodoItem item) {
            if (item == null) {
                return BadRequest ();
            }

            _context.TodoItems.Add (item);
            _context.SaveChanges ();

            return CreatedAtRoute ("GetTodo", new { id = item.Id }, item);
        }


        /// <summary>
        /// This method is used to update the todo list item.
        /// </summary>
        /// <param name="id">identifier which needs to be updated.</param>
        /// <param name="item">TodoItem Object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if(item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoItems.FirstOrDefault( t => t.Id == id);

            if(todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            todo.ModifiedOn = System.DateTime.Now;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }

}