using ClubLogBook.Core.Entities;
using System;
using System.Linq;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClubLogBook.Application.Interfaces.Mapping;

namespace ClubLogBook.Application.Models
{


	public class ClubFlightModel : IHaveCustomMapping
	{
		public ClubFlightModel()
		{
			Pilot = new PilotSelectModel();
			Aircraft = new AircraftModel();
		}

		public ClubFlightModel(DateTime date, string routh, Decimal es, Decimal ee)
		{
			Date = date;
			Routh = routh;
			EngineStart = es;
			EngineEnd = ee;
			HobbsStart = es;
			HobbsEnd = ee;
			Pilot = new PilotSelectModel();
			Aircraft = new AircraftModel();


		}
		public ClubFlightModel(decimal maxEngien, decimal maxHobbs)
		{
			EngineStart = EngineEnd = maxEngien;
			HobbsStart = HobbsEnd = maxHobbs;
			Pilot = new PilotSelectModel();
			Aircraft = new AircraftModel();

		}
		public ClubFlightModel(Pilot pilot, Aircraft aircraft)
		{
			Pilot = new PilotSelectModel(pilot);
			Aircraft = new AircraftModel(aircraft);
		}
		public int Id { get; set; }

		public DateTime Date { get; set; } = DateTime.Now;

		virtual public AircraftModel Aircraft { get; set; }
		virtual public PilotSelectModel Pilot { get; set; }
		public string Routh { get; set; }

		public Decimal EngineStart { get; set; }

		public Decimal EngineEnd { get; set; }
		public Decimal HobbsStart { get; set; }
		public Decimal HobbsEnd { get; set; }
		public bool IsValid { get; set; } = true;
		public InvoiceStateModel InvoiceStateViewModel  { get;set;} = InvoiceStateModel.Initial;
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"Id:{Id} Date:{Date} Routh:{Routh} EngienStart:{EngineStart} EngienEnd:{EngineEnd}");
			return sb.ToString();
		}
		public static bool operator >(ClubFlightModel f1, ClubFlightModel f2)
		{
			return f1?.EngineStart > f2?.EngineEnd;
		}

		public static bool operator >=(ClubFlightModel f1, ClubFlightModel f2)
		{
			return f1?.EngineStart >= f2?.EngineEnd;
		}
		public static bool operator <=(ClubFlightModel f1, ClubFlightModel f2)
		{
			return f1?.EngineEnd <= f2?.EngineStart;
		}
		public static bool operator <(ClubFlightModel f1, ClubFlightModel f2)
		{
			return f1?.EngineEnd < f2?.EngineStart;
		}
		public static bool operator ==(ClubFlightModel f1, ClubFlightModel f2)
		{

			return f1?.EngineEnd == f2?.EngineEnd && f1?.EngineStart == f2?.EngineStart;
		}
		public static bool operator !=(ClubFlightModel f1, ClubFlightModel f2)
		{
			return !(f1 == f2);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (this.GetType() != obj.GetType()) return false;

			ClubFlightModel other = (ClubFlightModel)obj;
			if (other.EngineStart >= this.EngineStart && other.EngineStart < this.EngineEnd || other.EngineEnd <= this.EngineEnd && other.EngineEnd > this.EngineStart)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public virtual object Clone()
		{
			return this;
		}

		protected virtual ClubFlightModel Create()
		{
			return new ClubFlightModel();
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<ClubFlightModel, Flight>();
			configuration.CreateMap<Flight,ClubContactsModel>();
		}
	}
	public class ClubFlightViewModel22
	{
		public ClubFlightViewModel22() { }
		//[DataType(DataType.DateTime)]
		
		//[DisplayFormat(DataFormatString = "{yyyy-mm-ddThh:mm}")]
		//public DateTime Date { get; set; }
		//public ClubFlightViewModel(Flight flight)
		//{

		//	//this.Licenses = pilot.Licenses;
		//}
		//public int Id { get; set; }
		//public DateTime Date { get; set; }

		//virtual public Aircraft Aircraft { get; set; }
		//virtual public Pilot Pilot { get; set; }
		//public string Routh { get; set; }

		//public Decimal EngineStart { get; set; }

		//public Decimal EngineEnd { get; set; }
		//public Decimal HobbsStart { get; set; }
		//public Decimal HobbsEnd { get; set; }
	}
	//public class ClubFlightViewModel1 
	//{
	//	public ClubFlightViewModel1() { }

	//	//public ClubFlightViewModel1(Flight flight)
	//	//{

	//	//	this.Licenses = pilot.Licenses;
	//	//}
	//	//public int Id { get; set; }
	//	//public DateTime Date { get; set; }

	//	////virtual public Aircraft Aircraft { get; set; }
	//	////virtual public Pilot Pilot { get; set; }
	//	//public string Routh { get; set; }

	//	//public Decimal EngineStart { get; set; }

	//	//public Decimal EngineEnd { get; set; }
	//	//public Decimal HobbsStart { get; set; }
	//	//public Decimal HobbsEnd { get; set; }
	//	Aircraft Aircraft { get; set; } = new Aircraft();
	//	Pilot pilot { get; set; } = new Pilot();
	//	public int Id { get; set; }
	//	public DateTime Date { get; set; }

	//	//virtual public Aircraft Aircraft { get; set; }
	//	//virtual public Pilot Pilot { get; set; }
	//	public string Routh { get; set; }

	//	public Decimal EngineStart { get; set; }

	//	public Decimal EngineEnd { get; set; }
	//	public Decimal HobbsStart { get; set; }
	//	public Decimal HobbsEnd { get; set; }
	//}

	//public class ClubFlightViewModel2
	//{
	//	public ClubFlightViewModel2() { }

	//	public ClubFlightViewModel2(Flight flight , Pilot p)
	//	{

	//		Aircraft = flight.Aircraft;
	//		pilot = p;
	//	}
	//	Aircraft Aircraft { get; set; }
	//	Pilot pilot { get; set; }
	//	public int Id { get; set; }
	//	public DateTime Date { get; set; }

	//	//virtual public Aircraft Aircraft { get; set; }
	//	//virtual public Pilot Pilot { get; set; }
	//	public string Routh { get; set; }

	//	public Decimal EngineStart { get; set; }

	//	public Decimal EngineEnd { get; set; }
	//	public Decimal HobbsStart { get; set; }
	//	public Decimal HobbsEnd { get; set; }
	//}
}
