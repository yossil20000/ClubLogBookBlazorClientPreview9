using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.AircraftManager.Commands.DeleteAircraft
{
    public class DeleteAircraftCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteAircraftCommand(int id)
        {
            Id = id;
        }

        public class DeleteTodoListCommandHandler : IRequestHandler<DeleteAircraftCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteTodoListCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteAircraftCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Aircraft>()
                    .Where(l => l.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Aircraft), request.Id);
                }

                _context.Set<Aircraft>().Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
