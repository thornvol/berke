using System.Collections.Generic;
using BerkeGaming.Application.TodoLists.Queries.ExportTodos;

namespace BerkeGaming.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
