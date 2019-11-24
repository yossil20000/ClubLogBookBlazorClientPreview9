using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Application.ViewModels
{
	public class ClubSelectViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Club Name")]
		public string ClubName { get; set; }
	}
}
