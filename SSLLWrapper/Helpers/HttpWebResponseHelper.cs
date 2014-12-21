using System.IO;
using System.Net;
using SSLLWrapper.Interfaces;

namespace SSLLWrapper.Helpers
{
	class HttpWebResponseHelper : IHttpWebResponseHelper
	{
		public string GetResponsePayload(HttpWebResponse webResponse)
		{
			string result = null;

			var streamReader = new StreamReader(webResponse.GetResponseStream());
			result = streamReader.ReadToEnd();

			return result;
		}

		public string GetStatusCode(HttpWebResponse webResponse)
		{
			return webResponse.StatusCode.ToString();
		}

		public string GetStatusDescription(HttpWebResponse webResponse)
		{
			return webResponse.StatusDescription;
		}

	}
}
