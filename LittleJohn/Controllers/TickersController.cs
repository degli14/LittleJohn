using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LittleJohn.Entities;
using LittleJohn.Infrastructure.Abstract;
using LittleJohn.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LittleJohn.Controllers
{
	[ApiController]
	public class TickersController : ControllerBase
	{
		private readonly ITickerRepository _tickerRepository;
		private readonly IUserRepository _userRepository;

		public TickersController()
		{
			_tickerRepository = TickerRepositoryFactory.GetTickerRepository();
			_userRepository = UserRepositoryFactory.GetUserRepository();
		}

		[HttpGet]
		[Route("tickers")]
		public IActionResult Get()
		{
			User user = _userRepository.Validate(Request);

			if (user != null)
			{
				IEnumerable<Ticker> portfolio = _tickerRepository.GetUserPortfolio(user.Id);

				return new JsonResult(portfolio.Select(t => new
				{
					symbol = t.Id,
					price = _tickerRepository.GetPrice(t, DateTime.Today).ToString("#.00", System.Globalization.CultureInfo.InvariantCulture)
				}).ToArray());
			}

			return Unauthorized();
		}

		[HttpGet]
		[Route("tickers/{id}/history")]
		public IActionResult History(string id)
		{
			User user = _userRepository.Validate(Request);

			if (user != null)
			{
				Ticker ticker = _tickerRepository.Get(id);

				if (ticker != null)
				{
					return new JsonResult(_tickerRepository.GetHistory(ticker.Id).Select(h => new
					{
						date = h.Date.ToString("yyyy-MM-dd"),
						price = h.Price.ToString("#.00", System.Globalization.CultureInfo.InvariantCulture)
					}).ToArray());
				}

				return NotFound();
			}

			return Unauthorized();
		}
	}
}
