using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.TodoItems.Commands.UpdateTodoItem
{
    public partial class UpdateTodoItemCommand : IRequest
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateTodoItemCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<TodoItem>().FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoItem), request.Id);
                }

                entity.Title = request.Title;
                entity.Done = request.Done;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
