using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Shared.ApiModel
{
	public class OperationResponse<T>
	{
		public OperationResponse() { }
		public T Record { get; set; }
		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public DateTime OperatioDate { get; set; }
	}
}
