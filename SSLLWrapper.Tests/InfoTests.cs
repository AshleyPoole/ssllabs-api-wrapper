using System.IO;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLWrapper;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;
using SSLLWrapper.Models.Response;

namespace given_that_I_make_a_info_request
{
	[TestClass]
	public class when_the_api_is_online
	{
		private static Info _infoResponse;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"engineVersion\":\"1.11.4\",\"criteriaVersion\":\"2009i\",\"clientMaxAssessments\":5,\"notice\":\"Some notice goes here\"}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = "https://api.dev.ssllabs.com/api/fa78d5a4/info"
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			_infoResponse = ssllService.Info();
		}

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			_infoResponse.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_false()
		{
			_infoResponse.HasErrorOccurred.Should().BeFalse();
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

		[TestMethod]
		public void then_the_info_response_header_status_code_should_be_200()
		{
			_infoResponse.Header.statusCode.Should().Be(200);
		}
	}

	[TestClass]
	public class when_the_api_is_offline
	{
		private static Info _infoResponse;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "",
				StatusCode = 400,
				StatusDescription = "Bad Request",
				Url = "https://api.dev.ssllabs.com/api/fa78d5a4/info"
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			_infoResponse = ssllService.Info();
		}

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			_infoResponse.Errors.Count.Should().BeGreaterOrEqualTo(1);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_true()
		{
			_infoResponse.HasErrorOccurred.Should().BeTrue();
		}

		[TestMethod]
		public void then_the_api_should_be_marked_as_offline()
		{
			_infoResponse.Online.Should().BeFalse();
		}

		[TestMethod]
		public void then_the_info_response_header_status_code_should_be_400()
		{
			_infoResponse.Header.statusCode.Should().Be(400);
		}
	}
}
