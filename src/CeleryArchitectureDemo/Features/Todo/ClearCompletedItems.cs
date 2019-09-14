using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CeleryArchitectureDemo.Infrastructure;
using MediatR;

namespace CeleryArchitectureDemo.Features.Todo
{
    public static class ClearCompletedItems
    {
        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly TodoContext _context;

            public Handler(TodoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.RemoveRange(_context.TodoItems.Where(i => i.IsCompleted));
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}