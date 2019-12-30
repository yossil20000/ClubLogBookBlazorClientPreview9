using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Core.Entities;
using AutoMapper;
using System.Text.RegularExpressions;
using ClubLogBook.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClubLogBook.Application.AircraftManager.Commands.CreateAircraft
{
	public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, int>
	{
		private readonly IApplicationDbContext context;
		private readonly IMapper mapper;
		public CreateAircraftCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<int> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
		{
			
			
			Aircraft aircraft = new Aircraft();
			mapper.Map(request.aircraftViewModel, aircraft);
			string newString = Regex.Replace(aircraft.TailNumber, "[^0-9a-zA-Z]", "");
			aircraft.TailNumber = newString.ToUpper();
			var result =  context.Set<Aircraft>().Where(a => a.TailNumber == aircraft.TailNumber).FirstOrDefault();
			if(result != null)
			{
				context.Set<Aircraft>().Update(aircraft);
			}
			else
			{
				await context.Set<Aircraft>().AddAsync(aircraft);
			}
			 
			await context.SaveChangesAsync(cancellationToken);
			mapper.Map(aircraft, request.aircraftViewModel);
			return request.aircraftViewModel.Id;
		}
	}
}
