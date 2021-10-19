using LittleJohn.Entities;
using LittleJohn.Infrastructure;
using LittleJohn.Infrastructure.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public UserController()
		{
			_userRepository = UserRepositoryFactory.GetUserRepository();
		}

		[HttpGet]
		[Route("[action]/{name}")]
		public IActionResult Register(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return Ok(_userRepository.RegisterUser(name));
			}
			return BadRequest();
		}
	}
}
