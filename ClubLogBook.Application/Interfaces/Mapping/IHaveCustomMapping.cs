﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
namespace ClubLogBook.Application.Interfaces.Mapping
{
	public interface IHaveCustomMapping
	{
		void CreateMappings(Profile configuration);
	}
}
