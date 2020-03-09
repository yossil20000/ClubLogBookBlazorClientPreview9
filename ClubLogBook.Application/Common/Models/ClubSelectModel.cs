using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Application.Models
{
	public class ClubSelectModel
	{
		public int Id { get; set; }
		[Display(Name = "Club Name")]
		public string ClubName { get; set; }
	}
}
