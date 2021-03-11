using System.Collections.Generic;
using BerkeGaming.Domain.Common;
using BerkeGaming.Domain.ValueObjects;

namespace BerkeGaming.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Color Color { get; set; } = Color.White;

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
