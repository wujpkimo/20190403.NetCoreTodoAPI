using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todoapi.Models;
using Microsoft.EntityFrameworkCore;

namespace Todoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;
            if (this.context.TodoItems.Count() == 0)
            {
                this.context.TodoItems.Add(new TodoItem { Names = "這是沒有資料的時候，自動建立的" });
                this.context.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var item = await this.context.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await this.context.TodoItems.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostItem(TodoItem item)
        {
            this.context.TodoItems.Add(item);
            await this.context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            this.context.Entry(item).State = EntityState.Modified;
            await this.context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var item = await this.context.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            this.context.TodoItems.Remove(item);
            this.context.SaveChanges();
            return NoContent();
        }
    }
}
