using System;
using System.Linq;

namespace SSLLWrapper.ConsoleAppTester
{
	class Program
	{
		private const string ApiUrl = "https://api.dev.ssllabs.com/api/fa78d5a4";
		static readonly Service ApiService = new Service(ApiUrl);

		static void Main(string[] args)
		{
			//AnalyzeTester();
			//InfoTester();
			GetEndpointData();
		}

		static void InfoTester()
		{
			var info = ApiService.Info();

			Console.WriteLine("Has Error Occoured: {0}", info.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", info.Headers.statusCode);
			Console.WriteLine("Engine Version: {0}", info.engineVersion);
			Console.WriteLine("Online: {0}", info.Online);

			Console.ReadLine();
		}

		static void AnalyzeTester()
		{
			var analyze = ApiService.Analyze("http://www.ashleypoole.co.uk");

			Console.WriteLine("Has Error Occoured: {0}", analyze.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", analyze.Headers.statusCode);
			Console.WriteLine("Status: {0}", analyze.status);

			Console.ReadLine();
		}

		static void GetEndpointData()
		{
			var endpointDataModel = ApiService.GetEndpointData("http://www.ashleypoole.co.uk", "104.28.6.2");

			Console.WriteLine("Has Error Occoured: {0}", endpointDataModel.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", endpointDataModel.Headers.statusCode);
			Console.WriteLine("IP Adress: {0}", endpointDataModel.ipAddress);
			Console.WriteLine("Grade: {0}", endpointDataModel.grade);
			Console.WriteLine("Status Message: {0}", endpointDataModel.statusMessage);

			Console.ReadLine();
		}
	}
}
