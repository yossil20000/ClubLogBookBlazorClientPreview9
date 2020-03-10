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
	public class UpdateContactCommand : IRequest<int>
	{
		public ClubContactsModel ClubContactsModel { get; set; }
		public UpdateContactCommand(ClubContactsModel clubContactsModel)
		{
			ClubContactsModel = clubContactsModel;
		}
	}
	public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, int>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public UpdateContactCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<int> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
		{
			Pilot pilot;
			pilot = ClubContactModelToPilotMapping.ClubContactsModelToPilot(_mapper, request.ClubContactsModel);
			_context.Set<Pilot>().Update(pilot);
			var result = await _context.SaveChangesAsync(cancellationToken);
			return result;
		}
	}
	public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
	{
		public UpdateContactCommandValidator()
		{
			RuleFor(x => x.ClubContactsModel.Id).GreaterThan(0).WithMessage("New Contact Id Must Be Grater Then 0");
		}
	}
}
