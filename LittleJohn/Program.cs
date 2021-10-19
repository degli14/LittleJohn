using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new WebHostBuilder()
				.UseKestrel()
				.UseStartup<Startup>()
				.UseUrls("http://localhost:8080")
				.Build()
				.Run();
		}
	}
}
