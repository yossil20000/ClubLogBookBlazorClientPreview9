using ClubLogBook.Core.Common;
using System.Collections.Generic;

namespace ClubLogBook.Core.Entities
{
    public class TodoList : AuditableEntity
    {
        public TodoList()
        {
            Items = new List<TodoItem>();
        }

       

        public string Title { get; set; } = string.Empty;

        public string Colour { get; set; } = string.Empty;

        public IList<TodoItem> Items { get; set; }
    }
}
