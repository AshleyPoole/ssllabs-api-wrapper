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
			requestModel.Parameters.Add("all", all);

			if (clearCache != "ignore") { requestModel.Parameters.Add("clearCache", clearCache); }
			if (fromCache != "ignore") { requestModel.Parameters.Add("fromCache", fromCache); }

			return requestModel;
		}

		public RequestModel GetEndpointDataProperties(string apiBaseUrl, string action, string host, string s, string fromCache)
		{
			var requestModel = new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};

			requestModel.Parameters.Add("host", host);
			requestModel.Parameters.Add("s", s);
			requestModel.Parameters.Add("fromCache", fromCache);

			return requestModel;
		}

		public RequestModel GetStatusCodeProperties(string apiBaseUrl, string action)
		{
			return new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};
		}
	}
}