using System.Net;

namespace SSLLWrapper.Interfaces
{
	public interface IHttpWebResponseHelper
	{
		string GetResponsePayload(HttpWebResponse webResponse);
		string GetStatusCode(HttpWebResponse webResponse);
		string GetStatusDescription(HttpWebResponse webResponse);
	}
}
