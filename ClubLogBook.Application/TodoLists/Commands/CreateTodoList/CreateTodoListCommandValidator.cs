using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Set<TodoList>().AllAsync(l => l.Title != title);
        }
    }
}
