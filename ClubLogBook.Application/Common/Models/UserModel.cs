﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClubLogBook.Application.Models
{
	public class UserModel
	{
		public UserModel() { }
		public string UserId { get; set; }
		[Required]
		public string LoginEmail;
		[Required] 
		public string UserName { get; set; }
		public List<string> Claims { get; set; }
		public List<string> Roles { get; set; }
	}
}
