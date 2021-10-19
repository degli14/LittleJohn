using LittleJohn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Infrastructure.Abstract
{
	public interface ITickerRepository
	{
		public Ticker Get(string id);

		public decimal GetPrice(Ticker ticker, DateTime dateTime);

		public IEnumerable<HistoricalPrice> GetHistory(string tickerId);

		public IEnumerable<Ticker> GetUserPortfolio(string userId);
	}
}
