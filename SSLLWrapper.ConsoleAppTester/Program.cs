using System;
using System.Linq;

namespace SSLLWrapper.ConsoleAppTester
{
	class Program
	{
		private const string apiUrl = "https://api.dev.ssllabs.com/api/fa78d5a4";

		static void Main(string[] args)
		{
			AnalyzeTester();
		}

		static void AnalyzeTester()
		{
			var apiService = new ApiService(apiUrl);
			var analyze = apiService.Analyze("http://www.ashleypoole.co.uk");

			Console.WriteLine("Has Error Occoured: {0}", analyze.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", analyze.Headers.statusCode);
			Console.WriteLine("Status: {0}", analyze.status);

			Console.ReadLine();
		}
	}
}
