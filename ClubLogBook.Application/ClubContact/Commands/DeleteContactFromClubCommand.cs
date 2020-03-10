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

namespace ClubLogBook.Application.ClubContact.Commands
{
	public class DeleteContactFromClubCommand : IRequest<int>
	{
		public string ClubName { get; set; }
		public int ContactId { get; set; }
		public DeleteContactFromClubCommand(string clubName, int contactId)
		{
			ClubName = clubName;
			ContactId = contactId;
		}
	}
	public class DeleteContactFromClubCommandHandler : IRequestHandler<DeleteContactFromClubCommand, int>
	{
		IApplicationDbContext _context;
		public DeleteContactFromClubCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(DeleteContactFromClubCommand request, CancellationToken cancellationToken)
		{
			var pilot = _context.Set<Pilot>().Find(request.ContactId);
			if (pilot != null)
			{
				var club = _context.Set<Club>().Where(i => i.Name.ToUpper() == request.ClubName.ToUpper()).FirstOrDefault();
				if (club != null)
				{
					var exist = club.Members.Where(i => i.Id == pilot.Id);
					if (exist.Any())
					{
						club.Members.Remove(pilot);
						_context.Set<Club>().Update(club);
						_context.Set<Pilot>().Remove(pilot);
						
						return await _context.SaveChangesAsync(cancellationToken);
					}
				}
			}
			return 0;
		}
	}
	public class DeleteContactFromClubCommandValidator : AbstractValidator<DeleteContactFromClubCommand>
	{
		public DeleteContactFromClubCommandValidator()
		{
			RuleFor(x => x.ClubName).NotEmpty().WithMessage("Club Name Must Not Be Empty");
			RuleFor(x => x.ContactId).GreaterThan(0).WithMessage("ContactId Must Be Greater Then 0");
		}
	}
}
