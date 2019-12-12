using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ClubLogBook.Core.Entities;
using NUnit.Framework;
using System.Linq;
using ClubLogBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Infrastructure.Persistence;
using ClubLogBook.Application.ViewModels;

namespace UnitsTest.AccountManager
{

	public class CreateAccountTest
	{
		IMapper mapper = AutoMapperConstructor.Instance.Mapper;
		ApplicationDbContext context;
		[Test]
		public void CreateAccount()
		{

			var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
			   .UseInMemoryDatabase(databaseName: "TestCatalog")
			   .Options;
			context = new ApplicationDbContext(dbOptions);
			List<Account> accounts = Accounts();
			List<AircraftPrice> aircraftPrices = AircraftPrices();
			List<Invoice> invoices = Invoices();
			context.Invoices.AddRange(invoices);
			context.AircraftPrices.AddRange(aircraftPrices);
			context.Accounts.UpdateRange(accounts);
			context.SaveChanges();
			AddTranaction();
			var ac = context.Accounts.Where(i => i.Id == 1).SingleOrDefault();
			System.Diagnostics.Debug.WriteLine(ac.ToString());
			System.Diagnostics.Debug.WriteLine(ac.Transactions.ToString());
			var accountObj = ac.GetJason<Account>();
			ac = context.Accounts.Where(i => i.Id == 2).SingleOrDefault();
			accountObj = ac.GetJason<Account>();

			AccountViewModel accountViewModel = new AccountViewModel();
			mapper.Map<Account, AccountViewModel>(ac, accountViewModel);

		}
		[Test]
		public void AddTranaction()
		{

			var aircraftPrices = context.AircraftPrices.Where(t => t.TailNumber == "4X1");
			var price = aircraftPrices.FirstOrDefault();

			var invoice = context.Invoices.Where(i => i.Id == 1).FirstOrDefault();
			invoice.InvoiceReferance = price.Id;
			invoice.InvoiceState = ClubLogBook.Core.Interfaces.InvoiceState.InTransaction;
			invoice.InvoiceType = ClubLogBook.Core.Interfaces.InvoiceType.Flight;
			invoice.Amount = price.PerHour;
			Transaction transaction = new Transaction();
			transaction.Invoice = invoice;
			var ac = context.Accounts.Where(i => i.MemeberId == 1).FirstOrDefault();
			ac.Transactions.Add(transaction);
			context.Accounts.Update(ac);
			context.SaveChanges();
			ac = context.Accounts.Where(i => i.MemeberId == 2).FirstOrDefault();
			ac.Transactions.Add(transaction);
			context.Accounts.Update(ac);
			context.SaveChanges();

		}
		List<AircraftPrice> AircraftPrices()
		{
			List<AircraftPrice> aircraftPrices = new List<AircraftPrice>();
			for (int i = 1; i <= 10; i++)
			{
				aircraftPrices.Add(new AircraftPrice() { Id = 0, TailNumber = $"4X{i}" });

			}
			return aircraftPrices;
		}
		List<Account> Accounts()
		{
			List<Account> accounts = new List<Account>();
			for (int i = 1; i <= 10; i++)
			{
				accounts.Add(new Account() { CashBalance = i, FlightBalance = 2 * i, Id = 0, MemeberId = i, MemberInfo = $"Member{i}", Description = "I ma AutoMatic Account No:{i}" });
			}

			return accounts;
		}
		List<Invoice> Invoices()
		{
			List<Invoice> invoices = new List<Invoice>();
			for (int i = 1; i <= 100; i++)
			{
				invoices.Add(new Invoice() { Id = 0, Amount = i * 2, InvoiceType = ClubLogBook.Core.Interfaces.InvoiceType.Flight });
			}
			return invoices;
		}
	}
}
