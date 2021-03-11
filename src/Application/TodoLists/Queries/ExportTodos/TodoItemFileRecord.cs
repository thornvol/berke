using BerkeGaming.Application.Common.Mappings;
using BerkeGaming.Domain.Entities;

namespace BerkeGaming.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
