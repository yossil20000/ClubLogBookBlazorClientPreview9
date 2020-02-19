using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Accounts.Commands.UpdateAccount
{
	public class UpdateAccountBalanceCommand : IRequest<int>
	{
		public decimal FlightBalance { get; set; }
		public decimal CashBalance { get; set; }
		public int MemeberId { get; set; }
		public class UpdateAccountBalanceCommandHandler : IRequestHandler<UpdateAccountBalanceCommand, int>
		{
			private readonly IApplicationDbContext _context;
			public UpdateAccountBalanceCommandHandler(IApplicationDbContext context) => _context = context;
			public async Task<int> Handle(UpdateAccountBalanceCommand request, CancellationToken cancellationToken)
			{
				var entity = _context.Set<Account>().Where(val => val.MemeberId == request.MemeberId).FirstOrDefault();
				if (entity == null)
				{
					return 0;
				}

				entity.FlightBalance = request.FlightBalance;
				entity.CashBalance = request.CashBalance;
				_context.Set<Account>().Add(entity);

				return await _context.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
