using System.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLWrapper;
using SSLLWrapper.Models.Response;

namespace given_that_I_make_a_GetEndpointDetails_request
{
	// Before running GetEndpointDetails() Analyze must be called to first test the Endpoints.
	// Failure to do so will result in failed tests.

	[TestClass]
	public class when_i_expect_a_successful_result
	{
		private static Endpoint _endpoint;
		private static string _endpointHost;
		private static string _endpointIp;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			_endpointHost = ConfigurationManager.AppSettings.Get("EndpointHost");
			_endpointIp = ConfigurationManager.AppSettings.Get("EndpointIP");

			var ssllService = new SSLLService(ConfigurationManager.AppSettings.Get("ApiUrl"));
			_endpoint = ssllService.GetEndpointData(_endpointHost, _endpointIp);
		}

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			_endpoint.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_HasErrorOccurred_should_be_false()
		{
			_endpoint.HasErrorOccurred.Should().BeFalse();
		}

		[TestMethod]
		public void then_status_code_header_should_be_greater_than_zero()
		{
			_endpoint.Header.statusCode.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void then_status_code_header_should_not_be_404()
		{
			_endpoint.Header.statusCode.Should().NotBe(404);
		}

		[TestMethod]
		public void then_should_not_trigger_an_api_invocation_error()
		{
			_endpoint.Header.statusCode.Should().NotBe(400);
		}

		[TestMethod]
		public void then_the_ip_in_the_response_should_match_the_request()
		{
			_endpoint.ipAddress.Should().Match(_endpointIp);
		}
		
		[TestMethod]
		public void then_the_status_message_should_not_be_null()
		{
			_endpoint.statusMessage.Should().NotBeNullOrEmpty();
		}

		[TestMethod]
		public void then_the_cert_subject_should_not_be_null()
		{
			_endpoint.Details.cert.subject.Should().NotBeNullOrEmpty();
		}
	}
}
