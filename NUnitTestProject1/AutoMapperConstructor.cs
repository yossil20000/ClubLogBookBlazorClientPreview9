using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ClubLogBook.Server;
namespace UnitsTest
{
	public sealed class AutoMapperConstructor
	{
		private static readonly AutoMapperConstructor instance = new AutoMapperConstructor();
		IMapper mapper;
		AutoMapperConstructor()
		{
			if(mapper == null)
			{
				var configuration = new MapperConfiguration(cfg => {
					cfg.AddProfile(new MappingProfile());
				});
				mapper = configuration.CreateMapper();
			}
		}
		public static AutoMapperConstructor Instance
		{
			get { return instance; }
		}

		public IMapper Mapper
		{
			get{ return mapper; }

		}

		
	}
}
