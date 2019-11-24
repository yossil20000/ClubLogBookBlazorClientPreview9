using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Services
{
	public class AccountManagerService : IAccountManagerService
	{
		IAppLogger<AccountManagerService> _appLogger;
		public AccountManagerService(IAppLogger<AccountManagerService> appLogger)
		{
			_appLogger = appLogger;

		}
	}
}
