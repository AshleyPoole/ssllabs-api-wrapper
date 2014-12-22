using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Interfaces
{
	interface IResponsePopulationHelper
	{
		InfoModel InfoModel(InfoModel infoModel);
		AnalyzeModel AnalyzeModel(AnalyzeModel analyzeModel);
	}
}
