using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSLLWrapper;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;
using SSLLWrapper.Models.Response;
using SSLLWrapper.Tests;

namespace given_that_I_make_a_get_endpoint_details_request
{
	[TestClass]
	public class when_a_valid_request_is_made_with_a_valid_endpoint_with_no_security_issues : PositiveTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "https://www.ashleypoole.co.uk";
			TestIP = "104.28.6.2";
			TestAltName = "ashleypoole.co.uk";
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"ipAddress\":\"104.28.6.2\",\"statusMessage\":\"Ready\",\"grade\":\"A\",\"hasWarnings\":false,\"isExceptional\":false,\"progress\":100," +
				          "\"duration\":72382,\"eta\":2,\"delegation\":3,\"details\":{\"hostStartTime\":1422796942752,\"key\":{\"size\":  256,\"alg\":\"EC\",\"strength\"" +
				          ":3072},\"cert\":{\"subject\":  \"CN\u003dsni25535.cloudflaressl.com,OU\u003dPositiveSSL Multi-Domain,OU\u003dDomain Control Validated\",  \"commonNames\"" +
				          ":[\"sni25535.cloudflaressl.com\"],\"altNames\":[\"sni25535.cloudflaressl.com\",\"*.ashleypoole.co.uk\",\"ashleypoole.co.uk\"],\"notBefore\":1412294400000,\"" +
				          "notAfter\":1443657599000, \"issuerSubject\": \"CN\u003dCOMODO ECC Domain Validation Secure Server CA 2,O\u003dCOMODO CA Limited,L\u003dSalford,ST\u003d" +
				          "Greater Manchester,C\u003dGB\",\"sigAlg\":\"SHA256withECDSA\",\"issuerLabel\":\"COMODO ECC Domain Validation Secure Server CA 2\",\"revocationInfo\":3,\"" +
				          "crlURIs\":[\"http://crl.comodoca4.com/COMODOECCDomainValidationSecureServerCA2.crl\"],\"ocspURIs\":[\"http://ocsp.comodoca4.com\"],\"revocationStatus\":2," +
				          "\"sgc\": 0,\"validationType\":\"D\",\"issues\":0},\"chain\":{\"certs\":[{\"subject\":\"CN\u003dsni25535.cloudflaressl.com,OU\u003dPositiveSSL Multi-Domain,OU\u003dDomain Control Validated\"," +
				          "\"label\":\"sni25535.cloudflaressl.com\",\"issuerSubject\":\"CN\u003dCOMODO ECC Domain Validation Secure Server CA 2,O\u003dCOMODO CA Limited,L\u003dSalford," +
				          "ST\u003dGreater Manchester,C\u003dGB\",\"issuerLabel\":\"COMODO ECC Domain Validation Secure Server CA 2\",\"raw\":\"-----BEGIN CERTIFICATE-----\n" +
				          "MIIGvjCCBmWgAwIBAgIRALF5GuZKQORJW1QMc46ynb0wCgYIKoZIzj0EAwIwgZIxCzAJBgNVBAYT\r\nAkdCMRswGQYDVQQIExJHcmVhdGVyIE1hbmNoZXN0ZXIxEDAOBgNVBAcTB1NhbGZvcmQxGjAYBgNV\r" +
				          "\nBAoTEUNPTU9ETyBDQSBMaW1pdGVkMTgwNgYDVQQDEy9DT01PRE8gRUNDIERvbWFpbiBWYWxpZGF0\r\naW9uIFNlY3VyZSBTZXJ2ZXIgQ0EgMjAeFw0xNDEwMDMwMDAwMDBaFw0xNTA5MzAyMzU5NTlaMGsx\r\n" +
				          "ITAfBgNVBAsTGERvbWFpbiBDb250cm9sIFZhbGlkYXRlZDEhMB8GA1UECxMYUG9zaXRpdmVTU0wg\r\nTXVsdGktRG9tYWluMSMwIQYDVQQDExpzbmkyNTUzNS5jbG91ZGZsYXJlc3NsLmNvbTBZMBMGByqG\r\n" +
				          "SM49AgEGCCqGSM49AwEHA0IABORKHcGfCBQWiWrulPF3cKWXMbma/cST468asBpJ5gakOx1CqIbA\r\nTaqVsoGbMYdOVLe53eNsID9MI3RqxRobozOjggTAMIIEvDAfBgNVHSMEGDAWgBRACWFn8LyDcU/e\r\n" +
				          "Eggsb9TUK3Y9ljAdBgNVHQ4EFgQUY3rb5UBQMdvLQvjzLEROu0aOvyUwDgYDVR0PAQH/BAQDAgeA\r\nMAwGA1UdEwEB/wQCMAAwHQYDVR0lBBYwFAYIKwYBBQUHAwEGCCsGAQUFBwMCME8GA1UdIARIMEYw\r\n" +
				          "OgYLKwYBBAGyMQECAgcwKzApBggrBgEFBQcCARYdaHR0cHM6Ly9zZWN1cmUuY29tb2RvLmNvbS9D\r\nUFMwCAYGZ4EMAQIBMFYGA1UdHwRPME0wS6BJoEeGRWh0dHA6Ly9jcmwuY29tb2RvY2E0LmNvbS9D\r\n" +
				          "T01PRE9FQ0NEb21haW5WYWxpZGF0aW9uU2VjdXJlU2VydmVyQ0EyLmNybDCBiAYIKwYBBQUHAQEE\r\nfDB6MFEGCCsGAQUFBzAChkVodHRwOi8vY3J0LmNvbW9kb2NhNC5jb20vQ09NT0RPRUNDRG9tYWlu\r\n" +
				          "VmFsaWRhdGlvblNlY3VyZVNlcnZlckNBMi5jcnQwJQYIKwYBBQUHMAGGGWh0dHA6Ly9vY3NwLmNv\r\nbW9kb2NhNC5jb20wggMHBgNVHREEggL+MIIC+oIac25pMjU1MzUuY2xvdWRmbGFyZXNzbC5jb22C\r\n" +
				          "EyouYXNobGV5cG9vbGUuY28udWuCIiouYXNzZXRkZXZlbG9wZXJzaW50ZXJuYXRpb25hbC5jb22C\r\nEyouYmxvZ2VzY2VwdGljby5jb22CECouYm93bG1hbnByaXYudGuCESouY2FzYW5lcm8uY29tLmJy\r\n" +
				          "ghAqLmNoZXptaXMuY29tLmJygggqLmRuei5iZYIWKi5lc3BvcnRzdW5saW1pdGVkLmNvbYIPKi5o\r\nZHRyYWlsZXJzLnJvghAqLmhnbWFuaWEuY29tLmJyghcqLmhvdGVsYWxhbW9zcG9zYWRhLmNvbYIX\r\n" +
				          "Ki5rYXJuYWtwcm9idWlsZGVycy5jb22CEioubG91YmVlemFydC5jby51a4IOKi5tZWdvYWZlay5j\r\nb22CESoubXlkcnVnY29zdHMuY29tghgqLm5lcmRjdWJlZHNlcnZlcnMuY28udWuCEioucG9saXRj\r\n" +
				          "YXN0LXVyaS5jaIISKi53aGl0bWFubG9kZ2Uub3JnggsqLnd5bHN0LmNvbYIRYXNobGV5cG9vbGUu\r\nY28udWuCIGFzc2V0ZGV2ZWxvcGVyc2ludGVybmF0aW9uYWwuY29tghFibG9nZXNjZXB0aWNvLmNv\r\n" +
				          "bYIOYm93bG1hbnByaXYudGuCD2Nhc2FuZXJvLmNvbS5icoIOY2hlem1pcy5jb20uYnKCBmRuei5i\r\nZYIUZXNwb3J0c3VubGltaXRlZC5jb22CDWhkdHJhaWxlcnMucm+CDmhnbWFuaWEuY29tLmJyghVo\r\n" +
				          "b3RlbGFsYW1vc3Bvc2FkYS5jb22CFWthcm5ha3Byb2J1aWxkZXJzLmNvbYIQbG91YmVlemFydC5j\r\nby51a4IMbWVnb2FmZWsuY29tgg9teWRydWdjb3N0cy5jb22CFm5lcmRjdWJlZHNlcnZlcnMuY28u\r\n" +
				          "dWuCEHBvbGl0Y2FzdC11cmkuY2iCEHdoaXRtYW5sb2RnZS5vcmeCCXd5bHN0LmNvbTAKBggqhkjO\r\nPQQDAgNHADBEAiBvjsgIaYlNMYg2ZkQ+i4zvWb5emdogr7Of5YoPbnAZigIgG9TjdWNBqfGL4Q6O\r\n" +
				          "FYr4C7e/1G6x/0Fz+LdWk3pbD3s\u003d\r\n-----END CERTIFICATE-----\n\"},    {\"subject\":\"CN\u003dCOMODO ECC Domain Validation Secure Server CA 2,O\u003d" +
				          "COMODO CA Limited,L\u003dSalford,ST\u003dGreater Manchester,C\u003dGB\",\"label\":\"COMODO ECC Domain Validation Secure Server CA 2\",\"issuerSubject\":\"" +
				          "CN\u003dCOMODO ECC Certification Authority,O\u003dCOMODO CA Limited,L\u003dSalford,ST\u003dGreater Manchester,C\u003dGB\",\"issuerLabel\":\"COMODO ECC Certification Authority\"" +
				          ",\"raw\":\"-----BEGIN CERTIFICATE-----\nMIIDnzCCAyWgAwIBAgIQWyXOaQfEJlVm0zkMmalUrTAKBggqhkjOPQQDAzCBhTELMAkGA1UEBhMC\r\n" +
				          "R0IxGzAZBgNVBAgTEkdyZWF0ZXIgTWFuY2hlc3RlcjEQMA4GA1UEBxMHU2FsZm9yZDEaMBgGA1UE\r\nChMRQ09NT0RPIENBIExpbWl0ZWQxKzApBgNVBAMTIkNPTU9ETyBFQ0MgQ2VydGlmaWNhdGlvbiBB\r\n" +
				          "dXRob3JpdHkwHhcNMTQwOTI1MDAwMDAwWhcNMjkwOTI0MjM1OTU5WjCBkjELMAkGA1UEBhMCR0Ix\r\nGzAZBgNVBAgTEkdyZWF0ZXIgTWFuY2hlc3RlcjEQMA4GA1UEBxMHU2FsZm9yZDEaMBgGA1UEChMR\r\n" +
				          "Q09NT0RPIENBIExpbWl0ZWQxODA2BgNVBAMTL0NPTU9ETyBFQ0MgRG9tYWluIFZhbGlkYXRpb24g\r\nU2VjdXJlIFNlcnZlciBDQSAyMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEAjgZgTrJaYRwWQKO\r\n" +
				          "qIofMN+83gP8eR06JSxrQSEYgur5PkrkM8wSzypD/A7yZADA4SVQgiTNtkk4DyVHkUikraOCAWYw\r\nggFiMB8GA1UdIwQYMBaAFHVxpxlIGbydnepBR9+UxEh3mdN5MB0GA1UdDgQWBBRACWFn8LyDcU/e\r\n" +
				          "Eggsb9TUK3Y9ljAOBgNVHQ8BAf8EBAMCAYYwEgYDVR0TAQH/BAgwBgEB/wIBADAdBgNVHSUEFjAU\r\nBggrBgEFBQcDAQYIKwYBBQUHAwIwGwYDVR0gBBQwEjAGBgRVHSAAMAgGBmeBDAECATBMBgNVHR8E\r\n" +
				          "RTBDMEGgP6A9hjtodHRwOi8vY3JsLmNvbW9kb2NhLmNvbS9DT01PRE9FQ0NDZXJ0aWZpY2F0aW9u\r\nQXV0aG9yaXR5LmNybDByBggrBgEFBQcBAQRmMGQwOwYIKwYBBQUHMAKGL2h0dHA6Ly9jcnQuY29t\r\n" +
				          "b2RvY2EuY29tL0NPTU9ET0VDQ0FkZFRydXN0Q0EuY3J0MCUGCCsGAQUFBzABhhlodHRwOi8vb2Nz\r\ncC5jb21vZG9jYTQuY29tMAoGCCqGSM49BAMDA2gAMGUCMQCsaEclgBNPE1bAojcJl1pQxOfttGHL\r\n" +
				          "KIoKETKm4nHfEQGJbwd6IGZrGNC5LkP3Um8CMBKFfI4TZpIEuppFCZRKMGHRSdxv6+ctyYnPHmp8\r\n7IXOMCVZuoFwNLg0f+cB0eLLUg\u003d\u003d\r\n-----END CERTIFICATE-----\n\"}," +
				          "{\"subject\":\"CN\u003dCOMODO ECC Certification Authority,O\u003dCOMODO CA Limited,L\u003dSalford,ST\u003dGreater Manchester,C\u003dGB\",\"label\":" +
				          "\"COMODO ECC Certification Authority\",\"issuerSubject\":\"CN\u003dAddTrust External CA Root,OU\u003dAddTrust External TTP Network,O\u003dAddTrust AB,C\u003dSE\"," +
				          "\"issuerLabel\":\"AddTrust External CA Root\",\"raw\":\"-----BEGIN CERTIFICATE-----\nMIID0DCCArigAwIBAgIQQ1ICP/qokB8Tn+P05cFETjANBgkqhkiG9w0BAQwFADBvMQswCQYDVQQG\r\n" +
				          "EwJTRTEUMBIGA1UEChMLQWRkVHJ1c3QgQUIxJjAkBgNVBAsTHUFkZFRydXN0IEV4dGVybmFsIFRU\r\nUCBOZXR3b3JrMSIwIAYDVQQDExlBZGRUcnVzdCBFeHRlcm5hbCBDQSBSb290MB4XDTAwMDUzMDEw\r\n" +
				          "NDgzOFoXDTIwMDUzMDEwNDgzOFowgYUxCzAJBgNVBAYTAkdCMRswGQYDVQQIExJHcmVhdGVyIE1h\r\nbmNoZXN0ZXIxEDAOBgNVBAcTB1NhbGZvcmQxGjAYBgNVBAoTEUNPTU9ETyBDQSBMaW1pdGVkMSsw\r\n" +
				          "KQYDVQQDEyJDT01PRE8gRUNDIENlcnRpZmljYXRpb24gQXV0aG9yaXR5MHYwEAYHKoZIzj0CAQYF\r\nK4EEACIDYgAEA0d7L3XJghWF+3XkkRbUq2KZ9T5SCwbOQQB/l+EKJDwdAQTuPdKNCZcM4HXk+vt3\r\n" +
				          "iir1A2BLNosWIxatCXH0SvQoULT+iBxuP2wvLwlZW6VbCzOZ4sM9iflqLO+y0wbpo4H+MIH7MB8G\r\nA1UdIwQYMBaAFK29mHo0tCb3+sQmVO8DveAky1QaMB0GA1UdDgQWBBR1cacZSBm8nZ3qQUfflMRI\r\n" +
				          "d5nTeTAOBgNVHQ8BAf8EBAMCAYYwDwYDVR0TAQH/BAUwAwEB/zARBgNVHSAECjAIMAYGBFUdIAAw\r\nSQYDVR0fBEIwQDA+oDygOoY4aHR0cDovL2NybC50cnVzdC1wcm92aWRlci5jb20vQWRkVHJ1c3RF\r\n" +
				          "eHRlcm5hbENBUm9vdC5jcmwwOgYIKwYBBQUHAQEELjAsMCoGCCsGAQUFBzABhh5odHRwOi8vb2Nz\r\ncC50cnVzdC1wcm92aWRlci5jb20wDQYJKoZIhvcNAQEMBQADggEBAB3H+i5AtlwFSw+8VTYBWOBT\r\n" +
				          "BT1k+6zZpTi4pyE7r5VbvkjI00PUIWxB7QktnHMAcZyuIXN+/46NuY5YkI78jG12yAA6nyCmLX3M\r\nF/3NmJYyCRrJZfwE67SaCnjllztSjxLCdJcBns/hbWjYk7mcJPuWJ0gBnOqUP3CYQbNzUTcp6PYB\r\n" +
				          "erknuCRR2RFo1KaFpzanpZa6gPim/a5thCCuNXZzQg+HCezF3OeTAyIal+6ailFhp5cmHunudVEI\r\nkAWvL54TnJM/ev/m6+loeYyv4Lb67psSE/5FjNJ80zXrIRKT/mZ1JioVhCb3ZsnLjbsJQdQYr7Gz\r\n" +
				          "EPUQyp2aDrV1aug\u003d\r\n-----END CERTIFICATE-----\n\"}],\"issues\":0},\"protocols\":[{\"id\":769,\"name\":\"TLS\",\"version\":\"1.0\"},{\"id\":770,\"name\":\"TLS\"" +
				          ",\"version\":\"1.1\"},{\"id\":771,\"name\":\"TLS\",\"version\":    \"1.2\"}],\"suites\":{\"list\":[{\"id\":49195,\"name\":\"TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256\"" +
				          ",\"cipherStrength\":128,\"ecdhBits\":256,\"ecdhStrength\":3072},{\"id\":49187,\"name\":\"TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256\",\"cipherStrength\":128," +
				          "\"ecdhBits\":256,\"ecdhStrength\":3072},{\"id\":49161,\"name\":\"TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA\",\"cipherStrength\":128,\"ecdhBits\":256,\"ecdhStrength\":3072}" +
				          ",{\"id\":49196,\"name\":\"TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384\",\"cipherStrength\":256,\"ecdhBits\":256,\"ecdhStrength\":3072},{\"id\":49188,\"name\":\"" +
				          "TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384\",\"cipherStrength\":256,\"ecdhBits\":256,\"ecdhStrength\":3072},{\"id\":49162,\"name\":\"TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA\"" +
				          ",\"cipherStrength\":256,\"ecdhBits\":256,\"ecdhStrength\":3072},{\"id\":49160,\"name\":\"TLS_ECDHE_ECDSA_WITH_3DES_EDE_CBC_SHA\",\"cipherStrength\":168,\"ecdhBits\":256," +
				          "\"ecdhStrength\":3072}],\"preference\":true},\"serverSignature\":\"cloudflare-nginx\",\"prefixDelegation\":true,\"nonPrefixDelegation\":true,\"vulnBeast\":true,\"" +
				          "renegSupport\":2,\"sessionResumption\":1,\"compressionMethods\":0,\"supportsNpn\":true,\"npnProtocols\":\"spdy/3.1 http/1.1\",\"sessionTickets\":1,\"ocspStapling\":true" +
				          ",\"sniRequired\":true,\"httpStatusCode\":301,\"httpForwarding\":\"http://www.ashleypoole.co.uk\",\"supportsRc4\":false,\"forwardSecrecy\":2,\"rc4WithModern\":false,\"" +
				          "sims\":{\"results\":[{\"client\":{\"id\":56,\"name\":\"Android\",\"version\":\"2.3.7\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":47}" +
				          ",{\"client\":{\"id\":58,\"name\":\"Android\",\"version\":\"4.0.4\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":59,\"name\":\"Android\",\"version\":\"4.1.1\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":60,\"name\":\"Android\",\"version\":\"4.2.2\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":61,\"name\":\"Android\",\"version\":\"4.3\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":62,\"name\":\"Android\",\"version\":\"4.4.2\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}," +
				          "{\"client\":{\"id\":88,\"name\":\"Android\",\"version\":\"5.0.0\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}," +
				          "{\"client\":{\"id\":94,\"name\":\"Baidu\",\"version\":\"Jan 2015\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":91,\"name\":\"BingPreview\",\"version\":\"Jan 2015\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}," +
				          "{\"client\":{\"id\":89,\"name\":\"Chrome\",\"platform\":\"OS X\",\"version\":\"40\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}," +
				          "{\"client\":{\"id\":84,\"name\":\"Firefox\",\"platform\":\"Win 7\",\"version\":\"31.3.0 ESR\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"" +
				          "suiteId\":49195},{\"client\":{\"id\":90,\"name\":\"Firefox\",\"platform\":\"OS X\",\"version\":\"35\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771" +
				          ",\"suiteId\":49195},{\"client\":{\"id\":72,\"name\":\"Googlebot\",\"version\":\"Jun 2014\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"" +
				          "suiteId\":49161},{\"client\":{\"id\":18,\"name\":\"IE\",\"platform\":\"XP\",\"version\":\"6\",\"isReference\":false},\"errorCode\":1,\"attempts\":1},{\"client\":" +
				          "{\"id\":19,\"name\":\"IE\",\"platform\":\"Vista\",\"version\":\"7\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":20,\"name\":\"IE\",\"platform\":\"XP\",\"version\":\"8\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":10}," +
				          "{\"client\":{\"id\":23,\"name\":\"IE\",\"platform\":\"Win 7\",\"version\":\"8-10\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}" +
				          ",{\"client\":{\"id\":95,\"name\":\"IE\",\"platform\":\"Win 7\",\"version\":\"11\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}" +
				          ",{\"client\":{\"id\":96,\"name\":\"IE\",\"platform\":\"Win 8.1\",\"version\":\"11\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}" +
				          ",{\"client\":{\"id\":64,\"name\":\"IE Mobile\",\"platform\":\"Win Phone 8.0\",\"version\":\"10\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"" +
				          "suiteId\":49161},{\"client\":{\"id\":65,\"name\":\"IE Mobile\",\"platform\":\"Win Phone 8.1\",\"version\":\"11\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"" +
				          "protocolId\":771,\"suiteId\":49195},{\"client\":{\"id\":25,\"name\":\"Java\",\"version\":\"6u45\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"" +
				          "suiteId\":47},{\"client\":{\"id\":26,\"name\":\"Java\",\"version\":\"7u25\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161}," +
				          "{\"client\":{\"id\":86,\"name\":\"Java\",\"version\":\"8u31\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195},{\"client\":" +
				          "{\"id\":27,\"name\":\"OpenSSL\",\"version\":\"0.9.8y\",\"isReference\":false},\"errorCode\":1,\"attempts\":1},{\"client\":{\"id\":28,\"name\":\"OpenSSL\",\"version\":\"1.0.1h\"" +
				          ",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195},{\"client\":{\"id\":32,\"name\":\"Safari\",\"platform\":\"OS X 10.6.8\",\"" +
				          "version\":\"5.1.9\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161},{\"client\":{\"id\":33,\"name\":\"Safari\",\"platform\":" +
				          "\"iOS 6.0.1\",\"version\":\"6\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49187},{\"client\":{\"id\":63,\"name\":\"Safari\",\"" +
				          "platform\":\"iOS 7.1\",\"version\":\"7\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49187},{\"client\":{\"id\":85,\"name\":\"" +
				          "Safari\",\"platform\":\"iOS 8.1.2\",\"version\":\"8\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49187},{\"client\":{\"id\":34," +
				          "\"name\":\"Safari\",\"platform\":\"OS X 10.8.4\",\"version\":\"6.0.4\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":769,\"suiteId\":49161},{\"client" +
				          "\":{\"id\":35,\"name\":\"Safari\",\"platform\":\"OS X 10.9\",\"version\":\"7\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49187}," +
				          "{\"client\":{\"id\":87,\"name\":\"Safari\",\"platform\":\"OS X 10.10\",\"version\":\"8\",\"isReference\":true},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"" +
				          "suiteId\":49187},{\"client\":{\"id\":92,\"name\":\"Yahoo Slurp\",\"version\":\"Jan 2015\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId" +
				          "\":49195},{\"client\":{\"id\":93,\"name\":\"YandexBot\",\"version\":\"Jan 2015\",\"isReference\":false},\"errorCode\":0,\"attempts\":1,\"protocolId\":771,\"suiteId\":49195}]}" +
				          ",\"heartbleed\":false,\"heartbeat\":false,\"openSslCcs\":1,\"poodleTls\":1}}",
				StatusCode = 200,
				StatusDescription = "Ok",
				Url = ("https://api.dev.ssllabs.com/api/fa78d5a4/getEndpoint?host=" + TestHost + "%s=" + TestIP)
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			Response = ssllService.GetEndpointData(TestHost, TestIP);
		}

		[TestMethod]
		public void then_no_security_warnings_should_be_found()
		{
			Response.hasWarnings.Should().BeFalse();
		}

		[TestMethod]
		public void then_the_progress_should_be_100_percent()
		{
			Response.progress.Should().Be(100);
		}

		[TestMethod]
		public void then_grade_should_be_A()
		{
			Response.grade.Should().Be("A");
		}

		[TestMethod]
		public void then_the_certifate_count_should_match_payload_count()
		{
			Response.Details.chain.certs.Count.Should().Be(3);
		}

		[TestMethod]
		public void then_the_key_common_names_should_contain_test_hostnames_common_name()
		{
			Response.Details.cert.altNames.Any(x => x.ToString() == TestAltName).Should().BeTrue();
		}
	}

	[TestClass]
	public class when_a_valid_request_is_made_but_no_endpoint_data_exists : NegativeTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			TestHost = "https://www.ashleypoole.co.uk";
			TestIP = "111.111.111.111";
			var webResponseModel = new WebResponseModel()
			{
				Payloay = "{\"errors\":[{\"message\":\"Endpoint not found\"}]}",
				StatusCode = 400,
				StatusDescription = "Bad Request",
				Url = ("https://api.dev.ssllabs.com/api/fa78d5a4/getEndpointData?host=" + TestHost + "s=" + TestIP)
			};

			mockedApiProvider.Setup(x => x.MakeGetRequest(It.IsAny<RequestModel>())).Returns(webResponseModel);

			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);
			Response = ssllService.GetEndpointData(TestHost, TestIP);
		}

		[TestMethod]
		public void then_the_status_code_should_reflect_endpoint_not_found()
		{
			Response.Header.statusCode.Should().Be(400);
		}
	}

	[TestClass]
	public class when_a_invalid_request_is_made_with_malformed_url_hostname : NegativeTests
	{
		[ClassInitialize]
		public static void Setup(TestContext testContext)
		{
			var mockedApiProvider = new Mock<IApiProvider>();
			var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4/", mockedApiProvider.Object);

			TestHost = "www.ashleypoole.somereallybadurl";
			TestIP = "111.111.111.111";

			Response = ssllService.GetEndpointData(TestHost, TestIP);
		}

		[TestMethod]
		public void then_preflight_error_should_be_thrown()
		{
			Response.Errors.Any(x => x.message == "Host does not pass preflight validation. No Api call has been made.").Should().BeTrue();
		}
	}

	public abstract class PositiveTests : GenericPositiveTests<Endpoint>
	{
		public static string TestHost;
		public static string TestIP;
		public static string TestAltName;

		[TestMethod]
		public void then_the_header_status_code_should_be_200()
		{
			Response.Header.statusCode.Should().Be(200);
		}
	}

	public abstract class NegativeTests : GenericNegativeTests<Endpoint>
	{
		public static string TestHost;
		public static string TestIP;
	}
}
