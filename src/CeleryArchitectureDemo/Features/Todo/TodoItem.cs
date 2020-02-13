namespace CeleryArchitectureDemo.Features.Todo
{
    using System;
    using Amazon.DynamoDBv2.DataModel;

    public class TodoItem
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime? WhenCompleted { get; set; }
        public bool IsCompleted { get; set; }
    }
}