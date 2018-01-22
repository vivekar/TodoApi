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
    }

}