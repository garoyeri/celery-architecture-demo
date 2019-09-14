using System;

namespace CeleryArchitectureDemo.Domain
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? WhenCompleted { get; set; }
        public bool IsCompleted { get; set; }
    }
}