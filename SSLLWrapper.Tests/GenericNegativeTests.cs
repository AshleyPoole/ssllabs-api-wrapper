using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLWrapper.Interfaces;

namespace SSLLWrapper.Tests
{
	public class GenericNegativeTests<T> where T : IBaseResponse
	{
		public static T Response;

		[TestMethod]
		public void then_the_error_count_should_be_greater_than_zero()
		{
			Response.Errors.Count.Should().BeGreaterOrEqualTo(1);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_true()
		{
			Response.HasErrorOccurred.Should().BeTrue();
		}
	}
}