using System.Net;
using SSLLWrapper.Models;

namespace SSLLWrapper.Interfaces
{
	interface IApiProvider
	{
		WebResponseModel MakeGetRequest(RequestModel requestModel);
	}
}
