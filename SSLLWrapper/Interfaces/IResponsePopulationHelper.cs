using System.Net;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Interfaces
{
	interface IResponsePopulationHelper
	{
		InfoModel InfoModel(HttpWebResponse webResponse, InfoModel infoModel);
		AnalyzeModel AnalyzeModel(HttpWebResponse webResponse, AnalyzeModel analyzeModel);
		EndpointDataModel EndpointDataModel(HttpWebResponse webResponse, EndpointDataModel endpointDataModel);
	}
}
