namespace CeleryArchitectureDemo.Features.Todo
{
    using System;

    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? WhenCompleted { get; set; }
        public bool IsCompleted { get; set; }
    }
}