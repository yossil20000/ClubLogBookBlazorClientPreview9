using ClubLogBook.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace ClubLogBook.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
