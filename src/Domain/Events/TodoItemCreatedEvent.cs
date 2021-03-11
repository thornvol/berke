using BerkeGaming.Domain.Common;
using BerkeGaming.Domain.Entities;

namespace BerkeGaming.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
