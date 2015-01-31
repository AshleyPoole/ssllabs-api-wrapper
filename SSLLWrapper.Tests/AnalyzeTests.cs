using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLWrapper;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;
using SSLLWrapper.Models.Response;
using SSLLWrapper.Tests;

namespace given_that_I_make_a_analyze_request
{
	[TestClass]
	public class when_a_valid_request_is_made_with_just_a_hostname_and_the_scan_has_completed : PositiveTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "https://www.ashleypoole.co.uk";
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
			Response = ssllService.Analyze(TestHost);
		}

		[TestMethod]
		public void then_the_scan_results_should_not_be_public()
		{
			Response.isPublic.Should().BeFalse();
		}
	}

	[TestClass]
	public class when_a_valid_request_is_made_with_just_a_hostname_and_the_scan_is_at_resolving_stage: PositiveTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "https://www.ashleypoole.co.uk";
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"host\":\"www.ashleypoole.co.uk\",\"port\":443,\"protocol\":\"HTTP\",\"isPublic\":false,\"status\":\"DNS\"" +
				          ",\"statusMessage\":\"Resolving domain names\",\"startTime\":1422475200798,\"engineVersion\":\"1.12.8\"," +
				          "\"criteriaVersion\":\"2009i\"}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = ("https://api.dev.ssllabs.com/api/fa78d5a4/analyze?host=" + TestHost)
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			Response = ssllService.Analyze(TestHost);
		}

		[TestMethod]
		public void then_the_scan_results_should_not_be_public()
		{
			Response.isPublic.Should().BeFalse();
		}
	}

	[TestClass]
	public class when_a_valid_request_is_made_with_all_the_inputs_and_the_scan_is_at_endpoint_scanning_stage : PositiveTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "https://www.ashleypoole.co.uk";
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"host\":\"www.ashleypoole.co.uk\",\"port\":443,\"protocol\":\"HTTP\",\"isPublic\":false,\"" +
				          "status\":\"IN_PROGRESS\",\"startTime\":1422479488403,\"engineVersion\":\"1.12.8\",\"criteriaVersion\":\"2009i\"" +
				          ",\"endpoints\":[{\"ipAddress\":\"104.28.6.2\",\"statusMessage\":\"In progress\",\"statusDetails\":\"TESTING_HTTPS\"" +
				          ",\"statusDetailsMessage\":\"Sending one complete HTTPS request\",\"progress\":-1,\"eta\":-1,\"delegation\":3}," +
				          "{\"ipAddress\":\"104.28.7.2\",\"statusMessage\":\"Pending\",\"progress\":-1,\"eta\":-1,\"delegation\":3}]}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = ("https://api.dev.ssllabs.com/api/fa78d5a4/analyze?host=" + TestHost + "&publish=on&clearCache=on&all=done")
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			Response = ssllService.Analyze(TestHost, SSLLService.Publish.On, SSLLService.ClearCache.On,
				SSLLService.FromCache.Ignore, SSLLService.All.Done);
		}

		[TestMethod]
		public void then_the_scan_results_should_be_public()
		{
			Response.isPublic.Should().BeTrue();
		}
	}

	[TestClass]
	public class when_a_invalid_request_is_made_with_all_the_inputs_and_the_scan_is_unable_to_resolve_hostname : NegativeTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "https://www.ashleypoole.co.uk";
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"host\":\"www2.ashleypoole.co.uk\",\"port\":443,\"protocol\":\"HTTP\",\"isPublic\":false,\"status\":\"ERROR\"," +
				          "\"statusMessage\":\"Unable to resolve domain name\",\"startTime\":1422478797953,\"testTime\":1422478798017," +
				          "\"engineVersion\":\"1.12.8\",\"criteriaVersion\":\"2009i\",\"cacheExpiryTime\":1422478858017}",
				StatusCode = 500,
				StatusDescription = "Bad Request",
				Url = ("https://api.dev.ssllabs.com/api/fa78d5a4/analyze?host=" + TestHost)
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			Response = ssllService.Analyze(TestHost, SSLLService.Publish.On, SSLLService.ClearCache.On,
				SSLLService.FromCache.Ignore, SSLLService.All.Done);
		}
	}

	public abstract class PositiveTests : GenericPositiveTests<Analyze>
	{
		public static string TestHost;

		[TestMethod]
		public void then_the_host_property_should_match_the_requested_hostname()
		{
			Response.host.Should().Be(TestHost.Substring(8));
		}

		[TestMethod]
		public void then_the_header_status_code_should_be_200()
		{
			Response.Header.statusCode.Should().Be(200);
		}

		[TestMethod]
		public void then_the_port_should_be_that_of_a_ssl_connection()
		{
			Response.port.Should().Be(443);
		}
	}

	public abstract class NegativeTests : GenericNegativeTests<Analyze>
	{
		public static string TestHost;

		[TestMethod]
		public void then_an_error_occurred_should_be_marked()
		{
			Response.HasErrorOccurred.Should().BeTrue();
		}

		[TestMethod]
		public void then_a_error_message_should_be_returned()
		{
			Response.Errors.Count.Should().BeGreaterOrEqualTo(1);
		}
	}
}
