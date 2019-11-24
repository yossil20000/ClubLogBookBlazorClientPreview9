using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Entities;

using ClubLogBook.Core.Interfaces;
using ClubLogBook.Application.Interfaces;

namespace ClubLogBook.Infrastructure.Data
{
	public class ClubLogbookContext : DbContext , IClubContext
	{
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
		//public DbSet<Pilot> Pilots { get; set; }
		public ClubLogbookContext() { }
		public ClubLogbookContext(DbContextOptions<ClubLogbookContext> options) :base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//builder.Entity<Address>(ConfigureAddress);
			//builder.Entity<AddressBook>(ConfigureAddressBook);
			//ConfigureMembers(builder);
			//builder.Entity<Aircraft>(ConfigureAircraft);
			//builder.Entity<Pilot>();
			//builder.Entity<Club>(ConfigureClub);
			//builder.Entity<CustomPropertyType>(ConfigureCustomPropertyType);
		}
		private void ConfigureCustomPropertyType(EntityTypeBuilder<CustomPropertyType> builder)
		{
			builder.HasKey(cpt => cpt.Id);
		}
		private void ConfigureClubNew(EntityTypeBuilder<Club> builder)
		{
			//builder.HasMany(s => s.Members).WithOne("Club");
			//builder.Property(e => e.Aircrafts);
			//builder.Property(e => e.Members);
		}
		private void ConfigureClub(EntityTypeBuilder<Club> builder)
		{
			//builder.HasKey(c => c.Id);
			//builder.Property(c => c.Name).HasMaxLength(15).IsRequired();
			//builder.Property(p => p.ClubAircrafts);
			//builder.Property(p => p.ClubMembers);
			//builder.HasMany(p => p.ClubAircrafts).WithOne();
			//builder.HasMany(p => p.ClubMembers).WithOne();
			
			//var navigation = builder.Metadata.FindNavigation(nameof(Club.Aircrafts));
			
			//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
			//var navigation1 = builder.Metadata.FindNavigation(nameof(Club.Members));
			
			//navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
		}
				
		public void ConfigureAircraft(EntityTypeBuilder<Aircraft> builder)
		{
			builder.HasKey(ar => ar.Id);
			builder.HasOne(ar => ar.AirCraftModel).WithMany().HasForeignKey(ar => ar.AirCraftModelId);
		}
		private void ConfigureAirCraftModel(EntityTypeBuilder<AirCraftModel> builder)
		{
			builder.HasKey(am => am.Id);
		}
		private void ConfigureMembers(ModelBuilder builder)
		{
			builder.Entity<Pilot>().ToTable("Pilots");
			//var navigation = builder.Metadata.FindNavigation(nameof(Member.Addresses));
			//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
			//navigation = builder.Metadata.FindNavigation(nameof(Member.EMAILs));
			//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
			//navigation = builder.Metadata.FindNavigation(nameof(Member.Phones));
			//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

		}
		private void ConfigureAddress(EntityTypeBuilder<Address> builder)
		{
			builder.Property(a => a.City).HasMaxLength(100).IsRequired();
			builder.Property(a => a.Country).HasMaxLength(90).IsRequired();
			builder.Property(a => a.State).HasMaxLength(60).IsRequired();
			builder.Property(a => a.Street).HasMaxLength(180).IsRequired();
			builder.Property(a => a.Zipcode).HasMaxLength(18).IsRequired();
		}
		private void ConfigureAddressBook(EntityTypeBuilder<ContactBook> builder)
		{
			
			
		}
		
	}
	public class Membercontext : DbContext
	{
		public DbSet<Address> Addresses { get; set; }
		public DbSet<ContactBook> AddressBooks { get; set; }
		public DbSet<Member> Members { get; set; }
		//public DbSet<AirCraftModel> AirCraftModels { get; set; }
		//public DbSet<Aircraft> Aircrafts { get; set; }
		//public DbSet<Club> Clubs { get; set; }
		//public DbSet<CustomPropertyType> CustomPropertyTypes { get; set; }
		public Membercontext(DbContextOptions<Membercontext> options) : base(options)
		{ }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//builder.Entity<Address>(ConfigureAddress);
			//builder.Entity<AddressBook>(ConfigureAddressBook);
			//builder.Entity<Member>(ConfigureMembers);
			//builder.Entity<Aircraft>(ConfigureAircraft);
			//builder.Entity<Club>(ConfigureClub);
			//builder.Entity<CustomPropertyType>(ConfigureCustomPropertyType);
		}
		private void ConfigureCustomPropertyType(EntityTypeBuilder<CustomPropertyType> builder)
		{
			builder.HasKey(cpt => cpt.Id);
		}
		private void ConfigureClub(EntityTypeBuilder<Club> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name).HasMaxLength(15).IsRequired();
			builder.HasMany(p => p.Aircrafts).WithOne();
			builder.HasMany(p => p.Members).WithOne();
			//builder.Property("ClubAircrafts");
			//builder.Property("ClubMembers");
			var navigation = builder.Metadata.FindNavigation(nameof(Club.Aircrafts));

			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
			var navigation1 = builder.Metadata.FindNavigation(nameof(Club.Members));

			navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		public void ConfigureAircraft(EntityTypeBuilder<Aircraft> builder)
		{
			builder.HasKey(ar => ar.Id);
			builder.HasOne(ar => ar.AirCraftModel).WithMany().HasForeignKey(ar => ar.AirCraftModelId);
		}
		private void ConfigureAirCraftModel(EntityTypeBuilder<AirCraftModel> builder)
		{
			builder.HasKey(am => am.Id);
		}
		private void ConfigureMembers(EntityTypeBuilder<Member> builder)
		{
			

		}
		private void ConfigureAddress(EntityTypeBuilder<Address> builder)
		{
			builder.Property(a => a.City).HasMaxLength(100).IsRequired();
			builder.Property(a => a.Country).HasMaxLength(90).IsRequired();
			builder.Property(a => a.State).HasMaxLength(60).IsRequired();
			builder.Property(a => a.Street).HasMaxLength(180).IsRequired();
			builder.Property(a => a.Zipcode).HasMaxLength(18).IsRequired();
		}
		private void ConfigureAddressBook(EntityTypeBuilder<ContactBook> builder)
		{

			
		}

	}
	public class ClubLogbookAirplanecontext : DbContext
	{
		//public DbSet<Address> Addresses { get; set; }
		//public DbSet<AddressBook> AddressBooks { get; set; }
		//public DbSet<Member> Members { get; set; }
		public DbSet<AirCraftModel> AirCraftModels { get; set; }
		public DbSet<Aircraft> Aircrafts { get; set; }
		//public DbSet<Club> Clubs { get; set; }
		public DbSet<CustomPropertyType> CustomPropertyTypes { get; set; }
		public ClubLogbookAirplanecontext(DbContextOptions<ClubLogbookAirplanecontext> options) : base(options)
		{ }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//builder.Entity<Address>(ConfigureAddress);
			//builder.Entity<AddressBook>(ConfigureAddressBook);
			//builder.Entity<Member>(ConfigureMembers);
			builder.Entity<Aircraft>(ConfigureAircraft);
			builder.Entity<AirCraftModel>(ConfigureAirCraftModel);
			//builder.Entity<Club>(ConfigureClub);
			builder.Entity<CustomPropertyType>(ConfigureCustomPropertyType);
		}
		private void ConfigureCustomPropertyType(EntityTypeBuilder<CustomPropertyType> builder)
		{
			builder.HasKey(cpt => cpt.Id);
		}
		private void ConfigureClub(EntityTypeBuilder<Club> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name).HasMaxLength(15).IsRequired();
			builder.HasMany(p => p.Aircrafts).WithOne();
			builder.HasMany(p => p.Members).WithOne();
			//builder.Property("ClubAircrafts");
			//builder.Property("ClubMembers");
			var navigation = builder.Metadata.FindNavigation(nameof(Club.Aircrafts));

			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
			var navigation1 = builder.Metadata.FindNavigation(nameof(Club.Members));

			navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		public void ConfigureAircraft(EntityTypeBuilder<Aircraft> builder)
		{
			builder.HasKey(ar => ar.Id);
			builder.HasOne(ar => ar.AirCraftModel).WithMany().HasForeignKey(ar => ar.AirCraftModelId);
		}
		private void ConfigureAirCraftModel(EntityTypeBuilder<AirCraftModel> builder)
		{
			builder.HasKey(am => am.Id);
		}
		private void ConfigureMembers(EntityTypeBuilder<Member> builder)
		{
			
		}
		private void ConfigureAddress(EntityTypeBuilder<Address> builder)
		{
			builder.Property(a => a.City).HasMaxLength(100).IsRequired();
			builder.Property(a => a.Country).HasMaxLength(90).IsRequired();
			builder.Property(a => a.State).HasMaxLength(60).IsRequired();
			builder.Property(a => a.Street).HasMaxLength(180).IsRequired();
			builder.Property(a => a.Zipcode).HasMaxLength(18).IsRequired();
		}
		private void ConfigureAddressBook(EntityTypeBuilder<ContactBook> builder)
		{

			
		}

	}
}
