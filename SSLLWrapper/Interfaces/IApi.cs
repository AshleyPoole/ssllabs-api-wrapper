using System.Net;
using SSLLWrapper.Models;

namespace SSLLWrapper.Interfaces
{
	public interface IApi
	{
		HttpWebResponse MakeGetRequest(RequestModel requestModel);
	}
}
