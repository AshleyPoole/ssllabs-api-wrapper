using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLabsApiWrapper.Interfaces;

namespace SSLLabsApiWrapper.Tests
{
	public class GenericPositiveTests<T> where T : IBaseResponse
	{
		public static T Response;

		[TestMethod]
		public void then_the_error_count_should_be_zero()
		{
			Response.Errors.Count.Should().Be(0);
		}

		[TestMethod]
		public void then_the_HasErrorOccurred_should_be_false()
		{
			Response.HasErrorOccurred.Should().BeFalse();
		}
	}
}