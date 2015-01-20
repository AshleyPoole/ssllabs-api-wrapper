using System.IO;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Tests
//namespace given_that_I_make_a_info_request
{
	[TestClass]
	public class when_the_api_is_online
	{
		private static Info _infoResponse;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			Stream responseStream = new MemoryStream();
			var mockedApiProvider = new Mock<IApiProvider>();
			var mockedHttpWebResponse = new Mock<HttpWebResponse>();
			var streamWriter = new StreamWriter(responseStream);

			streamWriter.Write("{\"engineVersion\":\"1.11.4\",\"criteriaVersion\":\"2009i\",\"clientMaxAssessments\":5,\"notice\":\"Some notice goes here\"}");

			mockedHttpWebResponse.Setup(x => x.GetResponseStream()).Returns(responseStream);
			mockedApiProvider.Setup(x => x.MakeGetRequest(new RequestModel())).Returns(mockedHttpWebResponse.Object);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			_infoResponse = ssllService.Info();
		}

		[TestMethod]
		public void then_no_errors_should_be_created()
		{
			_infoResponse.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_the_api_should_be_marked_as_online()
		{
			_infoResponse.Online.Should().BeTrue();
		}

		[TestMethod]
		public void then_the_info_response_should_be_populated_with_a_engine_number()
		{
			_infoResponse.engineVersion.Should().NotBeNullOrEmpty();
		}
	}
}
