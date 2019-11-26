using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.ViewModels;
using System.Collections.Generic;
namespace ClubLogBook.Application.AircraftManager.Queries
{
	public class GetClubAircraftListQuery : IRequest<AircraftListViewModel>
	{
		public string ClubName { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
	public class GetClubAircraftListQueryHandler : IRequestHandler<GetClubAircraftListQuery, AircraftListViewModel>
	{
		private readonly IClubContext context;
		private readonly IMapper mapper;
		public GetClubAircraftListQueryHandler(IClubContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<AircraftListViewModel> Handle(GetClubAircraftListQuery request, CancellationToken cancellationToken)
		{
			var ar = await context.Set<Club>().Where(a => a.Name.ToUpper() == request.ClubName.ToUpper()).SelectMany(a => a.Aircrafts).ToListAsync();
			
			IList<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
			mapper.Map(ar, aircraftViewModel);
			return new AircraftListViewModel
			{
				//AircraftList = await context.Set<Aircraft>().ProjectTo<AircraftViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
				AircraftList = aircraftViewModel
			};
		}
	}
}
