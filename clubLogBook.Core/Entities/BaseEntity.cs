using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public class BaseEntity : IBasicEntity
	{
		public int Id { get; set; } = 0;
	}
}
