using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LittleJohn.Test
{
	[TestClass]
	public class WebAppTest
	{
		private static WebApplicationFactory<Startup> _factory;
		private static readonly string[] _validTickerTypes = new string[] { "AAPL", "MSFT", "GOOG", "AMZN", "FB", "TSLA", "NVDA", "JPM", "BABA", "JNJ", "WMT", "PG", "PYPL", "DIS", "ADBE", "PFE", "V", "MA", "CRM", "NFLX" };

		[ClassInitialize]
		public static void ClassInit(TestContext testContext)
		{
			_factory = new WebApplicationFactory<Startup>()
				.WithWebHostBuilder(builder =>
				builder.UseSetting("http_port", "8080"));
		}

		[TestMethod]
		public async Task ShouldReturnUnauthorized()
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync("tickers");

			Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

			response = await client.GetAsync($"tickers/{_validTickerTypes[new Random().Next(0, _validTickerTypes.Length)]}/history");

			Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[TestMethod]
		public async Task ShouldCreateUserAndGetPortFolio()
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync("user/register/test");

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			var userToken = await response.Content.ReadAsStringAsync();

			client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(userToken + ":")));

			response = await client.GetAsync("tickers");
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
		}

		[TestMethod]
		public async Task ShouldCreateUserAndHistoryOfATicker()
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync("user/register/test");

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			var userToken = await response.Content.ReadAsStringAsync();

			client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(userToken + ":")));

			response = await client.GetAsync($"tickers/{_validTickerTypes[new Random().Next(0, _validTickerTypes.Length)]}/history");
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
		}

		[TestMethod]
		public async Task PortfolioShouldBeBetween1And10over20iterations()
		{
			var client = _factory.CreateClient();

			for (int i = 0; i < 20; i++)
			{
				if (client.DefaultRequestHeaders.Contains("Authorization"))
					client.DefaultRequestHeaders.Remove("Authorization");

				var response = await client.GetAsync("user/register/test");

				Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
				var userToken = await response.Content.ReadAsStringAsync();

				client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(userToken + ":")));
				response = await client.GetAsync("tickers");
				Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
				Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

				var portfolio = JArray.Parse(await response.Content.ReadAsStringAsync());

				Assert.IsTrue(portfolio.Count >= 1 && portfolio.Count < 100);
			}
		}
	}
}
