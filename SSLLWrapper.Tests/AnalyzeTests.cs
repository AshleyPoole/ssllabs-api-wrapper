using System.IO;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLWrapper;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;
using SSLLWrapper.Models.Response;
using SSLLWrapper.Tests.AnalyzeSharedTests;

namespace given_that_I_make_a_analyze_request
{
	[TestClass]
	public class when_the_api_is_online_and_a_valid_request_is_made : PositiveTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "www.ashleypoole.co.uk";
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"host\":\"www.ashleypoole.co.uk\",\"port\":443,\"protocol\":\"HTTPS\",\"isPublic\":true,\"status\":\"READY\",\"" +
				          "startTime\":1422115006431,\"testTime\":1422115131804,\"engineVersion\":\"1.12.8\",\"criteriaVersion\":\"2009i\",\"" +
				          "endpoints\":[{\"ipAddress\":\"104.28.6.2\",\"statusMessage\":\"Ready\",\"grade\":\"A\",\"hasWarnings\":false,\"" +
				          "isExceptional\":false,\"progress\":100,\"duration\":64286,\"eta\":2393,\"delegation\":3},{\"ipAddress\":\"104.28.7.2\"" +
				          ",\"statusMessage\":\"Ready\",\"grade\":\"A\",\"hasWarnings\":false,\"isExceptional\":false,\"progress\":100,\"duration\"" +
				          ":61046,\"eta\":2393,\"delegation\":3}]}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = ("https://api.dev.ssllabs.com/api/fa78d5a4/analyze?host=" + TestHost)
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			AnalyzeResponse = ssllService.Analyze(TestHost);
		}

		[TestMethod]
		public void then_the_header_status_code_should_be_200()
		{
			AnalyzeResponse.Header.statusCode.Should().Be(200);
		}
	}
}
