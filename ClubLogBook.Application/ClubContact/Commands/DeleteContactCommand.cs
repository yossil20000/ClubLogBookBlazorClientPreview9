using ClubLogBook.Application.Common.Exceptions;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Text.RegularExpressions;
using FluentValidation;
using ClubLogBook.Application.Common.Mappings;

namespace ClubLogBook.Application.ClubContact.Commands
{
	public class DeleteContactCommand : IRequest<int>
	{
		public int ContactId  { get; set; }
		public DeleteContactCommand(int contactId)
		{
			ContactId = contactId;
		}
	}
	public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, int>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public DeleteContactCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<int> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
		{
			var result = 0;
			Pilot pilot = _context.Set<Pilot>().Find(request.ContactId);
			if (pilot != null)
			{
				_context.Set<Pilot>().Remove(pilot);
				result = await _context.SaveChangesAsync(cancellationToken);
			
			}
			return result;
		}
	}
	public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
	{
		public DeleteContactCommandValidator()
		{
			RuleFor(x => x.ContactId).Equal(0).WithMessage("New Contact Id must be 0");
		}
	}
}
