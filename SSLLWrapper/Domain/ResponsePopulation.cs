using System.Net;
using Newtonsoft.Json;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper.Domain
{
	class ResponsePopulation
	{
		public JsonSerializerSettings JsonSerializerSettings;
		private readonly WebResponseReader _webResponseReader;

		public ResponsePopulation()
		{
			// Ignoring null values when serializing json objects
			JsonSerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};

			_webResponseReader = new WebResponseReader();
		}

		public Info InfoModel(HttpWebResponse webResponse, Info infoModel)
		{
			var webResult = _webResponseReader.GetResponsePayload(webResponse);

			infoModel = JsonConvert.DeserializeObject<Info>(webResult, JsonSerializerSettings);
			infoModel.Header.statusCode = _webResponseReader.GetStatusCode(webResponse);
			infoModel.Header.statusDescription = _webResponseReader.GetStatusDescription(webResponse);

			return infoModel;
		}

		public Analyze AnalyzeModel(HttpWebResponse webResponse, Analyze analyzeModel)
		{
			var webResult = _webResponseReader.GetResponsePayload(webResponse);

			analyzeModel = JsonConvert.DeserializeObject<Analyze>(webResult, JsonSerializerSettings);
			analyzeModel.Header.statusCode = _webResponseReader.GetStatusCode(webResponse);
			analyzeModel.Header.statusDescription = _webResponseReader.GetStatusDescription(webResponse);

			return analyzeModel;
		}

		public Endpoint EndpointModel(HttpWebResponse webResponse, Endpoint endpointModel)
		{
			var webResult = _webResponseReader.GetResponsePayload(webResponse);

			endpointModel = JsonConvert.DeserializeObject<Endpoint>(webResult, JsonSerializerSettings);
			endpointModel.Header.statusCode = _webResponseReader.GetStatusCode(webResponse);
			endpointModel.Header.statusDescription = _webResponseReader.GetStatusDescription(webResponse);

			return endpointModel;
		}

		public StatusCodes StatusCodesModel(HttpWebResponse webResponse, StatusCodes statusCodes)
		{
			var webResult = _webResponseReader.GetResponsePayload(webResponse);

			statusCodes = JsonConvert.DeserializeObject<StatusCodes>(webResult, JsonSerializerSettings);
			statusCodes.Header.statusCode = _webResponseReader.GetStatusCode(webResponse);
			statusCodes.Header.statusDescription = _webResponseReader.GetStatusDescription(webResponse);

			return statusCodes;
		}
	}
}