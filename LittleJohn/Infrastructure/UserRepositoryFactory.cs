using LittleJohn.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Infrastructure
{
	public static class UserRepositoryFactory
	{
		public static IUserRepository GetUserRepository()
		{
			return new Concrete.StupidUserRepository();
		}
	}
}
