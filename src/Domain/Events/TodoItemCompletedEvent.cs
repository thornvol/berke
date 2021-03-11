using BerkeGaming.Domain.Common;
using BerkeGaming.Domain.Entities;

namespace BerkeGaming.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
