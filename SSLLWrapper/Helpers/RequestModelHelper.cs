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

			requestModel.Paramaters.Add("host", host);
			requestModel.Paramaters.Add("publish", publish);
			requestModel.Paramaters.Add("clearCache", clearCache);
			requestModel.Paramaters.Add("fromCache", fromCache);
			requestModel.Paramaters.Add("all", all);

			return requestModel;
		}
	}
}