using System;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;

namespace SSLLWrapper.Helpers
{
	class RequestModelHelper : IRequestModelHelper
	{
		public RequestModel InfoProperties(string apiBaseUrl, string action)
		{
			return new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};
		}

		public RequestModel AnalyzeProperties(string apiBaseUrl, string action, string host, string publish, string clearCache,
			string fromCache, string all)
		{
			var requestModel = new RequestModel() { ApiBaseUrl = apiBaseUrl, Action = action};

			requestModel.Parameters.Add("host", host);
			requestModel.Parameters.Add("publish", publish);
			requestModel.Parameters.Add("clearCache", clearCache);
			//requestModel.Parameters.Add("fromCache", fromCache); // Temp commenting out
			requestModel.Parameters.Add("all", all);

			return requestModel;
		}
	}
}