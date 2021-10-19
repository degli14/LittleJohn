using LittleJohn.Infrastructure.Abstract;

namespace LittleJohn.Infrastructure
{
	public static class TickerRepositoryFactory
	{
		public static ITickerRepository GetTickerRepository()
		{
			return new Concrete.MockTickerRepository();
		}
	}
}
