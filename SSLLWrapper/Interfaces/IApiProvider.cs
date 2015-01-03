using System.Net;
using SSLLWrapper.Models;

namespace SSLLWrapper.Interfaces
{
	public interface IApiProvider
	{
		HttpWebResponse MakeGetRequest(RequestModel requestModel);
	}
}
