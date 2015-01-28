using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Tests.AnalyzeSharedTests
{
	public abstract class PositiveTests
	{
		public static Analyze AnalyzeResponse;
		public static string TestHost;

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			AnalyzeResponse.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_false()
		{
			AnalyzeResponse.HasErrorOccurred.Should().BeFalse();
		}

		[TestMethod]
		public void then_the_host_should_match_the_host_passed_into_analyze()
		{
			AnalyzeResponse.host.Should().Be(TestHost);
		}
	}
}
