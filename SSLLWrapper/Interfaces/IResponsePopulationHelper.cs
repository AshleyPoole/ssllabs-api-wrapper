using System.Net;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Interfaces
{
	interface IResponsePopulationHelper
	{
		Info InfoModel(HttpWebResponse webResponse, Info infoModel);
		Analyze AnalyzeModel(HttpWebResponse webResponse, Analyze analyzeModel);
		Endpoint EndpointModel(HttpWebResponse webResponse, Endpoint endpointDataModel);
		StatusDetails StatusDetailsModel(HttpWebResponse webResponse, StatusDetails statusDetails);
	}
}
