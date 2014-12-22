using System;
using System.Linq;

namespace SSLLWrapper.ConsoleAppTester
{
	class Program
	{
		static void Main(string[] args)
		{
			


		}

		static void AnalyzeTester()
		{
			var apiService = new ApiService("https://api.dev.ssllabs.com/api/fa78d5a4/");
			var analyze = apiService.Analyze("www.ashleypoole.co.uk");

			Console.WriteLine(string.Format("Has Error Occoured: {0}", analyze.HasErrorOccurred));
			Console.WriteLine(string.Format("First Error Message: {0}", analyze.Errors.First().message));
		}
	}
}
