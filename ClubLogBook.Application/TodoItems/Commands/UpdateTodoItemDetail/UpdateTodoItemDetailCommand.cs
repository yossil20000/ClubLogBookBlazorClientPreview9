using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.TodoItems.Commands.UpdateTodoItemDetail
{
    public class UpdateTodoItemDetailCommand : IRequest
    {
        public long Id { get; set; }

        public int ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }

        public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<TodoItem>().FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoItem), request.Id);
                }

                entity.ListId = request.ListId;
                entity.Priority = request.Priority;
                entity.Note = request.Note;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
