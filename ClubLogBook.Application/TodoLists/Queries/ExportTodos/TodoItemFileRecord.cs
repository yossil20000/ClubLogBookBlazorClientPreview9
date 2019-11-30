using ClubLogBook.Application.Common.Mappings;
using ClubLogBook.Core.Entities;

namespace ClubLogBook.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
