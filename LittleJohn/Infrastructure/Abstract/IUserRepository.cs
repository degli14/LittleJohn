using LittleJohn.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Infrastructure.Abstract
{
	public interface IUserRepository
	{
		string RegisterUser(string name);

		User Validate(HttpRequest request);
		User GetUserFromToken(string token);
	}
}
