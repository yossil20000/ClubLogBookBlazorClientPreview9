using System;
using System.Collections.Generic;

namespace ClubLogBook.Core.Entities
{
	public enum WBProfileType
	{
		Utility,
		Standart,
		Aerobatic
	}

	public class WBProfile
	{
		public WBProfile() { }
		public int ProfileId { get; set; }
		public WBProfileType WbProfileType { get; set; }
		public string ProfileFormat { get; set; }
		public string Profile { get; set; }

	}
	public interface IWeightAndBalance<TAircraft>
	{
		int WeightAndBalanceId { get; set; }
		DateTime IssueDate { get; set; }
		decimal EmptyWeight { get;  }
		decimal EmptyWeightLongCg { get;  }
		decimal EmptyWeightLatCg { get;}
		string Stations { get; set; }
		string StationFormat { get; set; }
		ICollection<WBProfile> Profile { get; set; }

	}
	public class WeightAndBalance : IWeightAndBalance<Aircraft>
	{
		public WeightAndBalance()
		{
			StationFormat = $"Protobuf";
		}

		public WeightAndBalance(DateTime issueDate,decimal emptyWeight, decimal emptyWeightLongCg, decimal emptyWeightLatCg)
		{
			IssueDate = issueDate;
			EmptyWeight = emptyWeight;
			EmptyWeightLongCg = emptyWeightLongCg;
			EmptyWeightLatCg = emptyWeightLatCg;
			StationFormat = $"PROTOBUF";
			FuelLoadingTableFormat = $"PROTOBUF";

		}
		public int WeightAndBalanceId { get; set; }
		public DateTime IssueDate { get; set; }
		public decimal EmptyWeight { get; private set; }
		public decimal EmptyWeightLongCg { get; private set; }
		public decimal EmptyWeightLatCg { get; private set; }
		public string Stations { get; set; }
		public string StationFormat { get; set; }
		public string FuelLoadingTable { get; set; }
		public string FuelLoadingTableFormat { get; set; }
		public virtual ICollection<WBProfile> Profile { get; set; } = new List<WBProfile>();
	}
}
