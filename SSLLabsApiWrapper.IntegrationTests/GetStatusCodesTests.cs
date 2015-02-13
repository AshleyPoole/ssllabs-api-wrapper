using System.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLabsApiWrapper;
using SSLLabsApiWrapper.Models.Response;

namespace given_that_I_make_a_GetStatusCodes_request
{
	[TestClass]
	public class when_i_expect_a_successful_result
	{
		private static StatusCodes _statusCodes;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var ssllService = new SSLLabsApiService(ConfigurationManager.AppSettings.Get("ApiUrl"));
			_statusCodes = ssllService.GetStatusCodes();
		}

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			_statusCodes.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_HasErrorOccurred_should_be_false()
		{
			_statusCodes.HasErrorOccurred.Should().BeFalse();
		}

		[TestMethod]
		public void then_status_code_header_should_be_greater_than_zero()
		{
			_statusCodes.Header.statusCode.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void then_status_code_header_should_be_not_be_404()
		{
			_statusCodes.Header.statusCode.Should().NotBe(404);
		}

		[TestMethod]
		public void then_CHECKING_REVOCATION_should_not_be_null()
		{
			_statusCodes.StatusDetails.TESTING_SUITES.Should().NotBeNullOrEmpty();
		}

		[TestMethod]
		public void then_should_not_trigger_an_api_invocation_error()
		{
			_statusCodes.Header.statusCode.Should().NotBe(400);
		}
	}
}
