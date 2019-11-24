using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.ViewModels
{
	public class AirplaneSelectViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Tail Number")]
		public string TailNumber { get; set; }
	}
}
