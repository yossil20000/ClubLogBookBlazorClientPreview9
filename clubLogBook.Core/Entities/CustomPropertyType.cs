using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface ICustomPropertyType : IBasicEntity
	{



		int CustomPropertyTypeId { get; set; }
		string Title { get; set; }
		string SortKey { get; set; }
		string FormatString { get; set; }
		short Type { get; set; }
		int Flags { get; set; }
		string Description { get; set; }

	}
	public class CustomPropertyType : BaseEntity
	{
		public CustomPropertyType()
		{ }
	
		
		public string Title { get; set; }
		public string SortKey { get; set; }
		public string FormatString { get; set; }
		public short Type { get; set; }
		public int Flags { get; set; }
		public string Description { get; set; }

		//public StringBuilder DumpAll()
		//{
		//	return  new StringBuilder();
		//}
	}
}
