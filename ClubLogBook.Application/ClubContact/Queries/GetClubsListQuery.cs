using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.Models;
using System.Collections.Generic;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.Common.Models;

namespace ClubLogBook.Application.ClubContact.Queries
{
	public class ClubListViewModel
	{
		public List<ClubModel> ClubViewModels { get; set; } = new List<ClubModel>();
	}

	public class GetClubsListQuery : IRequest<ClubListViewModel>
	{

		public GetClubsListQuery(QueryBy queryBy, string clubName, int id)
		{
			QueryBy = queryBy;
			ClubName = clubName;
			Id = id;
		}
		public int Id { get; set; }
		public QueryBy QueryBy { get; set; }
		public string ClubName { get; set; } = "";
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
	public class GetClubsListQueryHandler : IRequestHandler<GetClubsListQuery, ClubListViewModel>
	{
		private readonly IApplicationDbContext context;
		private readonly IMapper mapper;
		public GetClubsListQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<ClubListViewModel> Handle(GetClubsListQuery request, CancellationToken cancellationToken)
		{

			var clubs = request.QueryBy switch
			{
				QueryBy.Name => await context.Set<Club>().Where(a => (request.ClubName == "" ? true : a.Name.ToUpper() == request.ClubName.ToUpper())).ToListAsync(),
				QueryBy.ID => await context.Set<Club>().Where(a => (request.Id == 0 ? true : a.Id == request.Id)).ToListAsync()
			};

			ClubListViewModel clubListViewModel = new ClubListViewModel();
			clubListViewModel.ClubViewModels = mapper.Map<List<Club>, List<ClubModel>>(clubs);
			return clubListViewModel;
		}
	}
}
