using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title is already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
        {
            return await _context.Set<TodoList>()
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Title != title);
        }
    }
}
