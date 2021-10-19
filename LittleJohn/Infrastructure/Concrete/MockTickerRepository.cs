using LittleJohn.Entities;
using LittleJohn.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Infrastructure.Concrete
{
	internal class MockTickerRepository : ITickerRepository
	{
		private static readonly string[] _tickerTypes = new string[] { "AAPL", "MSFT", "GOOG", "AMZN", "FB", "TSLA", "NVDA", "JPM", "BABA", "JNJ", "WMT", "PG", "PYPL", "DIS", "ADBE", "PFE", "V", "MA", "CRM", "NFLX" };

		private const int minPortfolioTickersCount = 1;
		private const int maxPortfolioTickersCount = 10;

		public Ticker Get(string id)
		{
			if (_tickerTypes.Contains(id))
			{
				return new Ticker(id, id.ToInt32());
			}

			return null;
		}

		public IEnumerable<HistoricalPrice> GetHistory(string tickerId)
		{
			Ticker ticker = Get(tickerId);

			if (ticker != null)
			{
				List<HistoricalPrice> history = new List<HistoricalPrice>();
				for (DateTime dt = DateTime.Today.AddDays(-89); dt <= DateTime.Today; dt = dt.AddDays(1))
				{
					history.Add(new HistoricalPrice { Date = dt, Price = GetPrice(ticker, dt) });
				}
				return history;
			}

			return null;
		}

		public IEnumerable<Ticker> GetUserPortfolio(string userId)
		{
			List<Ticker> portfolio = new List<Ticker>();
			Random selector = new Random(userId.ToInt32());

			int portfolioItems = selector.Next(minPortfolioTickersCount, maxPortfolioTickersCount + 1);

			string nextTickerId;
			for (int i = 0; i < portfolioItems; i++)
			{
				do
				{
					nextTickerId = _tickerTypes[selector.Next(0, _tickerTypes.Length)];

				} while (portfolio.Any(t=>t.Id == nextTickerId));

				portfolio.Add(Get(nextTickerId));
			}

			return portfolio;
		}

		public decimal GetPrice(Ticker ticker, DateTime dateTime)
		{
			return decimal.Parse(new string((ticker.BaseValue / dateTime.Ticks).ToString().Reverse().Take(5).ToArray()).PadRight(5, '0')) / 100;
		}
	}
}
