using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubLogBook.Application.ViewModels
{
	public enum PageCommand
	{
		Current,
		MoveNext,
		MovePrevious
	}
	public class PaginationInfoViewModel
	{
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int ActualPage { get; set; }
		public int TotalPages { get; set; }
		public string Previous { get; set; } = "";
		public string Next { get; set; } = "";
		public PageCommand PageCommand { get; set; } = PageCommand.Current;

	}
}
