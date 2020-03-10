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
	public class CreateContactCommand : IRequest<ClubContactsModel>
	{
		public ClubContactsModel ClubContactsModel { get; set; }
		public CreateContactCommand(ClubContactsModel clubContactsModel)
		{
			ClubContactsModel = clubContactsModel;
		}
	}
	public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ClubContactsModel>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public CreateContactCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async  Task<ClubContactsModel> Handle(CreateContactCommand request, CancellationToken cancellationToken)
		{
			Pilot pilot;
			pilot = ClubContactModelToPilotMapping.ClubContactsModelToPilot(_mapper, request.ClubContactsModel);
			
			Contact contact = new Contact();
			pilot.Contact = contact;
			_context.Set<Pilot>().Update(pilot);
			var result = await _context.SaveChangesAsync(cancellationToken);
			return ClubContactModelToPilotMapping.PilotToClubContactsModel(_mapper, pilot);
		}
	}
	public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
	{
		public CreateContactCommandValidator()
		{
			RuleFor(x => x.ClubContactsModel.Id).Equal(0).WithMessage("New Contact Id must be 0");
		}
	}
}
