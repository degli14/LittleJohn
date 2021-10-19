using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn.Entities
{
	public class Ticker
	{
		public string Id { get; }
		public decimal BaseValue { get; }

		Ticker()
		{

		}

		public Ticker(string id, decimal _baseValue)
		{
			Id = id;
			BaseValue = _baseValue;
		}
	}
}
