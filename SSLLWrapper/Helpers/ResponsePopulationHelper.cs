using System.Net;
using Newtonsoft.Json;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Helpers
{
	class ResponsePopulationHelper : IResponsePopulationHelper
	{
		public JsonSerializerSettings JsonSerializerSettings;
		private readonly IHttpWebResponseHelper _webResponseHelper;

		public ResponsePopulationHelper()
		{
			// Ignoring null values when serializing json objects
			JsonSerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};

			_webResponseHelper = new HttpWebResponseHelper();
		}

		public Info InfoModel(HttpWebResponse webResponse, Info infoModel)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			infoModel = JsonConvert.DeserializeObject<Info>(webResult, JsonSerializerSettings);
			infoModel.Header.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			infoModel.Header.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return infoModel;
		}

		public Analyze AnalyzeModel(HttpWebResponse webResponse, Analyze analyzeModel)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			analyzeModel = JsonConvert.DeserializeObject<Analyze>(webResult, JsonSerializerSettings);
			analyzeModel.Header.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			analyzeModel.Header.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return analyzeModel;
		}

		public Endpoint EndpointModel(HttpWebResponse webResponse, Endpoint endpointModel)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			endpointModel = JsonConvert.DeserializeObject<Endpoint>(webResult, JsonSerializerSettings);
			endpointModel.Header.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			endpointModel.Header.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return endpointModel;
		}

		public StatusDetails StatusDetailsModel(HttpWebResponse webResponse, StatusDetails statusDetails)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			statusDetails = JsonConvert.DeserializeObject<StatusDetails>(webResult, JsonSerializerSettings);
			statusDetails.Header.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			statusDetails.Header.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return statusDetails;
		}
	}
}