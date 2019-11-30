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

       

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItem> Items { get; set; }
    }
}
