using LittleJohn.Entities;
using LittleJohn.Infrastructure.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Infrastructure.Concrete
{
	internal class StupidUserRepository : IUserRepository
	{
		private static List<User> _registeredUsers = new List<User>();

		public User GetUserFromToken(string token)
		{
			if(token == "gd")
			{
				return new User { Id = "gd", Name = "Giulio" };
			}

			return _registeredUsers.FirstOrDefault(u => u.Id == token);
		}

		public string RegisterUser(string name)
		{
			User user = new User { Name = name };

			do
			{
				user.Id = Guid.NewGuid().ToString().Split('-').Last();
			} while (_registeredUsers.Any(u => u.Id == user.Id));

			_registeredUsers.Add(user);

			return user.Id;
		}

		public User Validate(HttpRequest request)
		{
			if (request.Headers.ContainsKey("Authorization"))
			{
				string token = request.Headers["Authorization"];

				if (token.StartsWith("Basic "))
				{
					token = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(token.Replace("Basic ", ""))).Split(':')[0];

					return GetUserFromToken(token);
				}
			}
			return null;
		}
	}
}
