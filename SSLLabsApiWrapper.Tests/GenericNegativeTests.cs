using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLabsApiWrapper.Interfaces;

namespace SSLLabsApiWrapper.Tests
{
	public class GenericNegativeTests<T> where T : IBaseResponse
	{
		public static T Response;

		[TestMethod]
		public void then_at_least_one_error_should_be_thrown()
		{
			Response.Errors.Count.Should().BeGreaterOrEqualTo(1);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_true()
		{
			Response.HasErrorOccurred.Should().BeTrue();
		}

		[TestMethod]
		public void then_the_status_code_should_be_valid_for_response()
		{
			Response.Header.statusCode.Should().NotBe(200);
		}
	}
}