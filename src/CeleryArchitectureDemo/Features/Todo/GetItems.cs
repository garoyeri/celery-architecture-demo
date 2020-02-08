namespace CeleryArchitectureDemo.Features.Todo
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public static class GetItems
    {
        public class Query : IRequest<TodoItemList>
        {
        }

        public class Handler : IRequestHandler<Query, TodoItemList>
        {
            private readonly TodoContext _context;
            private readonly IMapper _mapper;

            public Handler(TodoContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<TodoItemList> Handle(Query request, CancellationToken cancellationToken)
            {
                var results = await _context.TodoItems
                    .OrderBy(i => i.WhenCompleted)
                    .ProjectTo<TodoItem>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new TodoItemList {TodoItems = results};
            }
        }
    }
}