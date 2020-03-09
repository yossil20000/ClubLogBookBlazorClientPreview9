using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Models
{
	public enum PageCommand
	{
		Current,
		MoveNext,
		MovePrevious
	}
	public class PaginationInfoModel
	{
		public int TotalItems { get; set; } = 0;
		public int ItemsPerPage { get; set; } = 10;
		public int ActualPage { get; set; } = 1;
		public int TotalPages { get; set; } = 1;
		public string Previous { get; set; } = "";
		public string Next { get; set; } = "";
		public PageCommand PageCommand { get; set; } = PageCommand.Current;

	}
}
