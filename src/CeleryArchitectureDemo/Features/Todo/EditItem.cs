using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CeleryArchitectureDemo.Infrastructure;
using MediatR;

namespace CeleryArchitectureDemo.Features.Todo
{
    public static class EditItem
    {
        public class Command : IRequest<TodoItem>
        {
            public int Id { get; set; }
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
                var item = await _context.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);
                if (item == null)
                    throw new TodoItemNotFoundException();

                item.Description = request.Description;

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<TodoItem>(item);
            }
        }
    }
}