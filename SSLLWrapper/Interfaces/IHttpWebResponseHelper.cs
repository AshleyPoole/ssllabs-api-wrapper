using System.Net;

namespace SSLLWrapper.Interfaces
{
	public interface IHttpWebResponseHelper
	{
		string GetResponsePayload(HttpWebResponse webResponse);
		int GetStatusCode(HttpWebResponse webResponse);
		string GetStatusDescription(HttpWebResponse webResponse);
	}
}
