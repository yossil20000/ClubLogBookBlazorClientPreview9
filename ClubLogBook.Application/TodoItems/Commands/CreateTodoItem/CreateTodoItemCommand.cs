using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<long>
    {
        public int ListId { get; set; }

        public string Title { get; set; }

        public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateTodoItemCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new TodoItem
                {
                    ListId = request.ListId,
                    Title = request.Title,
                    Done = false
                };

                _context.Set<TodoItem>().Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
