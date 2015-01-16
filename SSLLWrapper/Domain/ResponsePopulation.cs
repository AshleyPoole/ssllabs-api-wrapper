using System.Net;
using Newtonsoft.Json;
using SSLLWrapper.Models;
using SSLLWrapper.Models.Response;
using SSLLWrapper.Models.Response.BaseSubModels;

namespace SSLLWrapper.Domain
{
	class ResponsePopulation
	{
		public JsonSerializerSettings JsonSerializerSettings;

		public ResponsePopulation()
		{
			// Ignoring null values when serializing json objects
			JsonSerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
		}

		public Info InfoModel(WebResponseModel webResponse, Info infoModel)
		{
			infoModel = JsonConvert.DeserializeObject<Info>(webResponse.Payloay, JsonSerializerSettings);
			infoModel.Header = PopulateHeader(infoModel.Header, webResponse);

			return infoModel;
		}

		public Analyze AnalyzeModel(WebResponseModel webResponse, Analyze analyzeModel)
		{
			analyzeModel = JsonConvert.DeserializeObject<Analyze>(webResponse.Payloay, JsonSerializerSettings);
			analyzeModel.Header = PopulateHeader(analyzeModel.Header, webResponse);

			return analyzeModel;
		}

		public Endpoint EndpointModel(WebResponseModel webResponse, Endpoint endpointModel)
		{
			endpointModel = JsonConvert.DeserializeObject<Endpoint>(webResponse.Payloay, JsonSerializerSettings);
			endpointModel.Header = PopulateHeader(endpointModel.Header, webResponse);

			return endpointModel;
		}

		public StatusCodes StatusCodesModel(WebResponseModel webResponse, StatusCodes statusCodes)
		{
			statusCodes = JsonConvert.DeserializeObject<StatusCodes>(webResponse.Payloay, JsonSerializerSettings);
			statusCodes.Header = PopulateHeader(statusCodes.Header, webResponse);

			return statusCodes;
		}

		public Header PopulateHeader(Header header, WebResponseModel webResponse)
		{
			header.statusCode = webResponse.StatusCode;
			header.statusDescription = webResponse.StatusDescription;

			return header;
		}
	}
}