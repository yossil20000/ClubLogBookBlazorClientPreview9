using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.Models
{
	public class AirplaneSelectModel
	{
		public int Id { get; set; }
		[Display(Name = "Tail Number")]
		public string TailNumber { get; set; }
	}
}
