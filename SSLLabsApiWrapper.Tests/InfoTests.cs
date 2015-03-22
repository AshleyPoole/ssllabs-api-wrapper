using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLabsApiWrapper;
using SSLLabsApiWrapper.Interfaces;
using SSLLabsApiWrapper.Models;
using SSLLabsApiWrapper.Models.Response;
using SSLLabsApiWrapper.Tests;

namespace given_that_I_make_a_info_request
{
	[TestClass]
	public class when_the_api_is_online : GenericPositiveTests<Info>
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"engineVersion\":\"1.11.4\",\"criteriaVersion\":\"2009i\",\"clientMaxAssessments\":5,\"notice\":\"Some notice goes here\"}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = "https://api.ssllabs.com/api/v2/info"
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLabsApiService("https://api.ssllabs.com/api/v2/", mockedApiProvider.Object);
			Response = ssllService.Info();
		}

		[TestMethod]
		public void then_the_api_should_be_marked_as_online()
		{
			Response.Online.Should().BeTrue();
		}

		[TestMethod]
		public void then_the_info_response_should_be_populated_with_a_engine_number()
		{
			Response.engineVersion.Should().NotBeNullOrEmpty();
		}

		[TestMethod]
		public void then_the_info_response_header_status_code_should_be_200()
		{
			Response.Header.statusCode.Should().Be(200);
		}
	}

	[TestClass]
	public class when_the_api_is_offline : SharedNegativeTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = null,
				StatusCode = 0,
				StatusDescription = null,
				Url = "https://api.ssllabs.com/api/v2/info"
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLabsApiService("https://api.ssllabs.com/api/v2/", mockedApiProvider.Object);
			Response = ssllService.Info();
		}

		[TestMethod]
		public void then_the_info_response_header_status_code_should_indicate_failure()
		{
			Response.Header.statusCode.Should().Be(0);
		}
	}

	[TestClass]
	public class when_the_api_url_is_invalid : SharedNegativeTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "",
				StatusCode = 0,
				StatusDescription = "",
				Url = ""
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLabsApiService("https://blah-blah.dev.ssllabs.com/api/blah/", mockedApiProvider.Object);
			Response = ssllService.Info();
		}
	}

	public abstract class SharedNegativeTests : GenericNegativeTests<Info>
	{
		[TestMethod]
		public void then_the_api_should_be_marked_as_offline()
		{
			Response.Online.Should().BeFalse();
		}
	}
}
