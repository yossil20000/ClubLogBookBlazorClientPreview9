﻿using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public interface ICustomPropertyType : IAuditableEntity
	{



		int CustomPropertyTypeId { get; set; }
		string Title { get; set; }
		string SortKey { get; set; }
		string FormatString { get; set; }
		short Type { get; set; }
		int Flags { get; set; }
		string Description { get; set; }

	}
	public class CustomPropertyType : AuditableEntity
	{
		public CustomPropertyType()
		{ }


		public string Title { get; set; } = "";
		public string SortKey { get; set; } = "";
		public string FormatString { get; set; } = "";
		public short Type { get; set; } = 0;
		public int Flags { get; set; } = 0;
		public string Description { get; set; } = "";

		//public StringBuilder DumpAll()
		//{
		//	return  new StringBuilder();
		//}
	}
}
