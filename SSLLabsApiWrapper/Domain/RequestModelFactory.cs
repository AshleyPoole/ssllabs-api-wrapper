using SSLLabsApiWrapper.Models;

namespace SSLLabsApiWrapper.Domain
{
	class RequestModelFactory
	{
		public RequestModel NewInfoRequestModel(string apiBaseUrl, string action)
		{
			return new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};
		}

		public RequestModel NewAnalyzeRequestModel(string apiBaseUrl, string action, string host, string publish, string clearCache,
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

		public RequestModel NewEndpointDataRequestModel(string apiBaseUrl, string action, string host, string s, string fromCache)
		{
			var requestModel = new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};

			requestModel.Parameters.Add("host", host);
			requestModel.Parameters.Add("s", s);
			requestModel.Parameters.Add("fromCache", fromCache);

			return requestModel;
		}

		public RequestModel NewStatusCodesRequestModel(string apiBaseUrl, string action)
		{
			return new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};
		}
	}
}