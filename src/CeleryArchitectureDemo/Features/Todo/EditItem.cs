namespace CeleryArchitectureDemo.Features.Todo
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using MediatR;

    public static class EditItem
    {
        public class Command : IRequest<TodoItem>
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, TodoItem>
        {
            private readonly IAmazonDynamoDB _client;

            public Handler(IAmazonDynamoDB client)
            {
                _client = client;
            }

            public async Task<TodoItem> Handle(Command request, CancellationToken cancellationToken)
            {
                var context = new DynamoDBContext(_client);

                var itemFound = await context.LoadAsync<TodoItem>(request.Id, cancellationToken);
                itemFound.Description = request.Description;
                await context.SaveAsync(itemFound, cancellationToken);

                return itemFound;
            }
        }
    }
}