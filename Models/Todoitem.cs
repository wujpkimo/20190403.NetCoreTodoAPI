using System.Collections.Generic;

namespace Todoapi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Names { get; set; }
        public bool IsComplete { get; set; }
    }
}