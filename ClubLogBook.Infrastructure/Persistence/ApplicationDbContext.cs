using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        //private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //_currentUserService = currentUserService;
            //_dateTime = dateTime;
        }

        

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactBook> ContactBooks { get; set; }
        public DbSet<Pilot> Members { get; set; }
        public DbSet<AirCraftModel> AirCraftModels { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<CustomPropertyType> CustomPropertyTypes { get; set; }
        public DbSet<LogBook> LogBooks { get; set; }
        public DbSet<FlightRecord> FlightRecords { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<AircraftLogBook> AircraftLogBooks { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<EMAIL> EMAILs { get; set; }
        public DbSet<Endorsment> Endorsments { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Checkride> Checkrides { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AircraftReservation> AircraftReservations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<AircraftPrice> AircraftPrices { get; set; }

        



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
