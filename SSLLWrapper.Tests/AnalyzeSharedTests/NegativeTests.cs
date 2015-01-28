using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Tests.AnalyzeSharedTests
{
	public abstract class NegativeTests
	{
		public static Analyze AnalyzeResponse;

		[TestMethod]
		public void then_the_error_count_should_be_greater_than_zero()
		{
			AnalyzeResponse.Errors.Count.Should().BeGreaterOrEqualTo(1);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_true()
		{
			AnalyzeResponse.HasErrorOccurred.Should().BeTrue();
		}
	}
}
