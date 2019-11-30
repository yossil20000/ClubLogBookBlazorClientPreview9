using ClubLogBook.Core.Common;
using ClubLogBook.Core.Enums;
using System;

namespace ClubLogBook.Core.Entities
{
    public class TodoItem : AuditableEntity
    {
        

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public bool Done { get; set; }

        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; }


        public TodoList List { get; set; }
    }
}
