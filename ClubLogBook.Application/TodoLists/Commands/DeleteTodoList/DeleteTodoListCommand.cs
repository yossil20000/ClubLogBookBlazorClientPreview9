﻿using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteTodoListCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<TodoList>()
                    .Where(l => l.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }

                _context.Set<TodoList>().Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
