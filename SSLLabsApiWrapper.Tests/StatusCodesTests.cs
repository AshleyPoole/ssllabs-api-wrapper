using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLabsApiWrapper;
using SSLLabsApiWrapper.Interfaces;
using SSLLabsApiWrapper.Models;
using SSLLabsApiWrapper.Models.Response;
using SSLLabsApiWrapper.Tests;

namespace given_that_I_make_a_status_codes_request
{
	[TestClass]
	public class when_the_api_is_online : SharedPositiveTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"statusDetails\":{\"TESTING_PROTOCOL_INTOLERANCE_399\":\"Testing Protocol Intolerance (TLS 1.99)\",\"" +
				          "PREPARING_REPORT\":\"Preparing the report\",\"TESTING_SESSION_RESUMPTION\":\"Testing session resumption\",\"" +
				          "TESTING_NPN\":\"Testing NPN\",\"RETRIEVING_CERT_V3__NO_SNI\":\"Retrieving certificate\",\"RETRIEVING_CERT_V3__SNI_APEX\"" +
				          ":\"Retrieving certificate\",\"TESTING_CVE_2014_0224\":\"Testing CVE-2014-0224\",\"TESTING_CAPABILITIES\":\"" +
				          "Determining server capabilities\",\"TESTING_HEARTBLEED\":\"Testing Heartbleed\",\"TESTING_PROTO_3_3_V2H\":\"Testing TLS 1.1 (v2 handshake)\"" +
				          ",\"TESTING_SESSION_TICKETS\":\"Testing Session Ticket support\",\"VALIDATING_TRUST_PATHS\":\"Validating trust paths\",\"TESTING_RENEGOTIATION\"" +
				          ":\"Testing renegotiation\",\"TESTING_HTTPS\":\"Sending one complete HTTPS request\",\"TESTING_V2H_HANDSHAKE\":\"Testing v2 handshake\",\"" +
				          "TESTING_STRICT_RI\":\"Testing Strict Renegotiation\",\"TESTING_SUITES_DEPRECATED\":\"Testing deprecated cipher suites\",\"TESTING_HANDSHAKE_SIMULATION" +
				          "\":\"Simulating handshakes\",\"TESTING_STRICT_SNI\":\"Testing Strict SNI\",\"TESTING_PROTO_3_1_V2H\":\"Testing TLS 1.0 (v2 handshake)\",\"" +
				          "TESTING_PROTOCOL_INTOLERANCE_499\":\"Testing Protocol Intolerance (TLS 2.99)\",\"TESTING_TLS_VERSION_INTOLERANCE\":\"Testing TLS version intolerance" +
				          "\",\"TESTING_PROTOCOL_INTOLERANCE_304\":\"Testing Protocol Intolerance (TLS 1.3)\",\"TESTING_SUITES_BULK\":\"Bulk-testing less common cipher suites\",\"" +
				          "TESTING_BEAST\":\"Testing for BEAST\",\"TESTING_PROTO_2_0\":\"Testing SSL 2.0\",\"BUILDING_TRUST_PATHS\":\"Building trust paths\",\"TESTING_PROTO_3_1\":\"" +
				          "Testing TLS 1.0\",\"TESTING_PROTO_3_0_V2H\":\"Testing SSL 3.0 (v2 handshake)\",\"TESTING_PROTO_3_0\":\"Testing SSL 3.0\",\"TESTING_PROTOCOL_INTOLERANCE_300" +
				          "\":\"Testing Protocol Intolerance (SSL 3.0)\",\"TESTING_PROTOCOL_INTOLERANCE_301\":\"Testing Protocol Intolerance (TLS 1.0)\",\"TESTING_PROTOCOL_INTOLERANCE_302" +
				          "\":\"Testing Protocol Intolerance (TLS 1.1)\",\"TESTING_PROTOCOL_INTOLERANCE_303\":\"Testing Protocol Intolerance (TLS 1.2)\",\"TESTING_OCSP_STAPLING_PRIME\":" +
				          "\"Trying to prime OCSP stapling\",\"TESTING_EXTENSION_INTOLERANCE\":\"Testing Extension Intolerance (might take a while)\",\"TESTING_SSL2_SUITES\":\"" +
				          "Checking if SSL 2.0 has any ciphers enabled\",\"TESTING_OCSP_STAPLING\":\"Testing OCSP stapling\",\"TESTING_SUITES\":\"Determining available cipher suites\"," +
				          "\"TESTING_PROTO_3_2_V2H\":\"Testing TLS 1.1 (v2 handshake)\",\"TESTING_POODLE_TLS\":\"Testing POODLE against TLS\",\"RETRIEVING_CERT_V3__SNI_WWW\":\"" +
				          "Retrieving certificate\",\"CHECKING_REVOCATION\":\"Checking for revoked certificates\",\"TESTING_COMPRESSION\":\"Testing compression\",\"TESTING_SUITE_PREFERENCE" +
				          "\":\"Determining cipher suite preference\",\"TESTING_PROTO_3_2\":\"Testing TLS 1.1\",\"TESTING_PROTO_3_3\":\"Testing TLS 1.2\",\"TESTING_LONG_HANDSHAKE\":\"" +
				          "Testing Long Handshake (might take a while)\"}}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = "https://api.ssllabs.com/api/v2/info"
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLabsApiService("https://api.ssllabs.com/api/v2/", mockedApiProvider.Object);
			Response = ssllService.GetStatusCodes();
		}

		[TestMethod]
		public void then_the_info_response_header_status_code_should_be_200()
		{
			Response.Header.statusCode.Should().Be(200);
		}
	}

	[TestClass]
	public class when_the_api_is_offline : SharedNegativeTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var webResponseModel = new WebResponseModel()
			{
				Payloay = null,
				StatusCode = 0,
				StatusDescription = null,
				Url = "https://api.ssllabs.com/api/v2/info"
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLabsApiService("https://api.ssllabs.com/api/v2/", mockedApiProvider.Object);
			Response = ssllService.GetStatusCodes();
		}

		[TestMethod]
		public void then_the_info_response_header_status_code_should_indicate_failure()
		{
			Response.Header.statusCode.Should().Be(0);
		}
	}

	public abstract class SharedPositiveTests : GenericPositiveTests<StatusCodes>
	{
		[TestMethod]
		public void then_an_error_should_not_be_recorded()
		{
			Response.Errors.Count.Should().Be(0);
		}
	}

	public abstract class SharedNegativeTests : GenericNegativeTests<StatusCodes>
	{
		[TestMethod]
		public void then_at_least_one_error_should_be_recorded()
		{
			Response.Errors.Count.Should().BeGreaterOrEqualTo(1);
		}
	}
}
