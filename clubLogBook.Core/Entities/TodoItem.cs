using ClubLogBook.Core.Common;
using ClubLogBook.Core.Enums;
using System;

namespace ClubLogBook.Core.Entities
{
    public class TodoItem : AuditableEntity
    {



        public int ListId { get; set; } = 0;

        public string Title { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public bool Done { get; set; }

        public DateTime? Reminder { get; set; }

        public PriorityLevel Priority { get; set; } = PriorityLevel.None;


        public TodoList List { get; set; }
    }
}
