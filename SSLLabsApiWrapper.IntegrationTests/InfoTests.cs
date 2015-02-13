using System.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLabsApiWrapper;
using SSLLabsApiWrapper.Models.Response;

namespace given_that_I_make_a_info_request
{
	[TestClass]
	public class when_i_expect_a_successful_result
	{
		private static Info _info;

		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var ssllService = new SSLLabsApiService(ConfigurationManager.AppSettings.Get("ApiUrl"));
			_info = ssllService.Info();
		}

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			_info.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_HasErrorOccurred_should_be_false()
		{
			_info.HasErrorOccurred.Should().BeFalse();
		}

		[TestMethod]
		public void then_Online_should_be_true()
		{
			_info.Online.Should().BeTrue();
		}

		[TestMethod]
		public void then_clientMaxAssessments_should_be_greater_than_zero()
		{
			_info.clientMaxAssessments.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void then_status_code_header_should_be_greater_than_zero()
		{
			_info.Header.statusCode.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void then_status_code_header_should_not_be_404()
		{
			_info.Header.statusCode.Should().NotBe(404);
		}

		[TestMethod]
		public void then_should_not_trigger_an_api_invocation_error()
		{
			_info.Header.statusCode.Should().NotBe(400);
		}
	}
}
