using System;

namespace SSLLWrapper.ConsoleAppTester
{
	class Program
	{
		private const string ApiUrl = "https://api.dev.ssllabs.com/api/fa78d5a4";
		static readonly SSLLService SSLLService = new SSLLService(ApiUrl);

		static void Main(string[] args)
		{
			//AnalyzeTester();
			//InfoTester();
			//GetEndpointData();
			GetStatusCodes();
		}

		static void InfoTester()
		{
			var info = SSLLService.Info();

			Console.WriteLine("Has Error Occoured: {0}", info.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", info.Header.statusCode);
			Console.WriteLine("Engine Version: {0}", info.engineVersion);
			Console.WriteLine("Online: {0}", info.Online);

			Console.ReadLine();
		}

		static void AnalyzeTester()
		{
			var analyze = SSLLService.Analyze("http://www.ashleypoole.co.uk");

			Console.WriteLine("Has Error Occoured: {0}", analyze.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", analyze.Header.statusCode);
			Console.WriteLine("Status: {0}", analyze.status);

			Console.ReadLine();
		}

		static void GetEndpointData()
		{
			var endpointDetails = SSLLService.GetEndpointData("http://www.ashleypoole.co.uk", "104.28.6.2");

			Console.WriteLine("Has Error Occoured: {0}", endpointDetails.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", endpointDetails.Header.statusCode);
			Console.WriteLine("IP Adress: {0}", endpointDetails.ipAddress);
			Console.WriteLine("Grade: {0}", endpointDetails.grade);
			Console.WriteLine("Status Message: {0}", endpointDetails.statusMessage);

			Console.ReadLine();
		}

		static void GetStatusCodes()
		{
			var statusDetails = SSLLService.GetStatusCodes();

			Console.WriteLine("Has Error Occoured: {0}", statusDetails.HasErrorOccurred);
			Console.WriteLine("Status Code: {0}", statusDetails.Header.statusCode);

			Console.ReadLine();
		}
	}
}
