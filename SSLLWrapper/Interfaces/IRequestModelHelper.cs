using SSLLWrapper.Models;

namespace SSLLWrapper.Interfaces
{
	interface IRequestModelHelper
	{
		RequestModel InfoProperties(string apiBaseUrl, string action);
		RequestModel AnalyzeProperties(string apiBaseUrl, string action, string host, string publish, string clearCache, string fromCache, string all);
		RequestModel GetEndpointDataProperties(string apiBaseUrl, string action, string host, string s, string fromCache);
		RequestModel GetStatusCodeProperties(string apiBaseUrl, string action);
	}
}
