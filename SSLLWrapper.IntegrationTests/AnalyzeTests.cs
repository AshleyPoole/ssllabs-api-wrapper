using System.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLWrapper;
using SSLLWrapper.Models.Response;

namespace given_that_I_make_a_analyze_request
{
	[TestClass]
	public class when_i_expect_a_successful_result
	{
		private static SSLLService _ssllService;
		private static Analyze _analyze;
		private static string _host;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			_host = ConfigurationManager.AppSettings.Get("EndpointHost");
			_ssllService = new SSLLService(ConfigurationManager.AppSettings.Get("ApiUrl"));
			_analyze = _ssllService.Analyze(_host);
		}

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			_analyze.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_HasErrorOccurred_should_be_false()
		{
			_analyze.HasErrorOccurred.Should().BeFalse();
		}

		[TestMethod]
		public void then_status_code_header_should_be_greater_than_zero()
		{
			_analyze.Header.statusCode.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void then_status_code_header_should_not_be_404()
		{
			_analyze.Header.statusCode.Should().NotBe(404);
		}

		[TestMethod]
		public void then_should_not_trigger_an_api_invocation_error()
		{
			_analyze.Header.statusCode.Should().NotBe(400);
		}

		[TestMethod]
		public void then_analyze_status_should_not_be_null()
		{
			_analyze.status.Should().NotBeNullOrEmpty();
		}

		[TestMethod]
		public void then_start_time_should_be_greater_than_zero()
		{
			_analyze.startTime.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void then_the_host_in_the_response_should_match_the_request()
		{
			_analyze.host.Should().Match(_host);
		}
	}
}
