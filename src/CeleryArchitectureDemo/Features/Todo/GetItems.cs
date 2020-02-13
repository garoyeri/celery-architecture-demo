namespace CeleryArchitectureDemo.Features.Todo
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using MediatR;

    public static class GetItems
    {
        public class Query : IRequest<TodoItemList>
        {
        }

        public class Handler : IRequestHandler<Query, TodoItemList>
        {
            private readonly IAmazonDynamoDB _client;

            public Handler(IAmazonDynamoDB client)
            {
                _client = client;
            }

            public async Task<TodoItemList> Handle(Query request, CancellationToken cancellationToken)
            {
                var context = new DynamoDBContext(_client);

                var itemsFound = context.ScanAsync<TodoItem>(Enumerable.Empty<ScanCondition>());
                var nextItemSet = await itemsFound.GetNextSetAsync(cancellationToken);
                return new TodoItemList
                {
                    TodoItems = nextItemSet
                };
            }
        }
    }
}