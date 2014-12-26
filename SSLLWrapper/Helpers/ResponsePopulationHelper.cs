using System.Net;
using Newtonsoft.Json;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Helpers
{
	public class ResponsePopulationHelper : IResponsePopulationHelper
	{
		public JsonSerializerSettings JsonSerializerSettings;
		private readonly IHttpWebResponseHelper _webResponseHelper;

		public ResponsePopulationHelper()
		{
			// Ignoring null values when serializing json objects
			JsonSerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};

			_webResponseHelper = new HttpWebResponseHelper();
		}

		public InfoModel InfoModel(HttpWebResponse webResponse, InfoModel infoModel)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			infoModel = JsonConvert.DeserializeObject<InfoModel>(webResult, JsonSerializerSettings);
			infoModel.Headers.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			infoModel.Headers.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return infoModel;
		}

		public AnalyzeModel AnalyzeModel(HttpWebResponse webResponse, AnalyzeModel analyzeModel)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			analyzeModel = JsonConvert.DeserializeObject<AnalyzeModel>(webResult, JsonSerializerSettings);
			analyzeModel.Headers.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			analyzeModel.Headers.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return analyzeModel;
		}

		public EndpointDataModel EndpointDataModel(HttpWebResponse webResponse, EndpointDataModel endpointDataModel)
		{
			var webResult = _webResponseHelper.GetResponsePayload(webResponse);

			endpointDataModel = JsonConvert.DeserializeObject<EndpointDataModel>(webResult, JsonSerializerSettings);
			endpointDataModel.Headers.statusCode = _webResponseHelper.GetStatusCode(webResponse);
			endpointDataModel.Headers.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);

			return endpointDataModel;
		}
	}
}