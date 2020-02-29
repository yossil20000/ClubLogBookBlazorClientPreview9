using ClubLogBook.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
		int Id { get; set; }
		DateTime IssueDate { get; set; }
		decimal EmptyWeight { get;  }
		decimal EmptyWeightLongCg { get;  }
		decimal EmptyWeightLatCg { get;}
		string Stations { get; set; }
		string StationFormat { get; set; }
		ICollection<WBProfile> Profile { get; set; }

	}
	public class WeightAndBalance : AuditableEntity, IWeightAndBalance<Aircraft>
	{
		public WeightAndBalance()
		{
			StationFormat = $"JSON";
		}

		public WeightAndBalance(DateTime issueDate,decimal emptyWeight, decimal emptyWeightLongCg, decimal emptyWeightLatCg)
		{
			IssueDate = issueDate;
			EmptyWeight = emptyWeight;
			EmptyWeightLongCg = emptyWeightLongCg;
			EmptyWeightLatCg = emptyWeightLatCg;
			StationFormat = $"JSON";
			FuelLoadingTableFormat = $"JSON";

		}
		[Required]
		public DateTime IssueDate { get; set; }
		[Required]
		public decimal EmptyWeight { get; private set; }
		[Required]
		public decimal EmptyWeightLongCg { get; private set; }
		[Required]
		public decimal EmptyWeightLatCg { get; private set; }
		public string Stations { get; set; }
		public string StationFormat { get; set; }
		public string FuelLoadingTable { get; set; }
		public string FuelLoadingTableFormat { get; set; }
		public virtual ICollection<WBProfile> Profile { get; set; } = new List<WBProfile>();
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
