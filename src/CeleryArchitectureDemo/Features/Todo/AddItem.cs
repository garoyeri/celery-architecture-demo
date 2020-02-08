namespace CeleryArchitectureDemo.Features.Todo
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Infrastructure;
    using MediatR;

    public static class AddItem
    {
        public class Command : IRequest<TodoItem>
        {
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, TodoItem>
        {
            private readonly TodoContext _context;
            private readonly IMapper _mapper;

            public Handler(TodoContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<TodoItem> Handle(Command request, CancellationToken cancellationToken)
            {
                var newItem = await _context.TodoItems.AddAsync(
                    _mapper.Map<Domain.TodoItem>(request), cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<TodoItem>(newItem.Entity);
            }
        }
    }
}