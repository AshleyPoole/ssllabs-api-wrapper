using System;
using Newtonsoft.Json;
using SSLLWrapper.Helpers;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models.Response;
using SSLLWrapper.Models.Response.BaseResponseSubModels;

namespace SSLLWrapper
{
    public class ApiService
    {
	    #region construction

	    private readonly IApi _api;
	    private readonly HttpWebResponseHelper _webResponseHelper;
	    private readonly RequestModelHelper _requestModelHelper;
		private readonly UrlHelper _urlHelper;
	    public string ApiUrl { get; set; }
	    public JsonSerializerSettings JsonSerializerSettings;


	    public enum Publish
	    {
		    On,
		    Off
	    }

	    public enum ClearCache
	    {
		    On,
		    Off
	    }

	    public enum FromCache
	    {
		    On,
		    Off
	    }

	    public enum All
	    {
		    On,
		    Done
	    }

	    public ApiService(string apiUrl)
		{
			_api = new Api();
			_webResponseHelper = new HttpWebResponseHelper();
			_requestModelHelper = new RequestModelHelper();
		    _urlHelper = new UrlHelper();
			JsonSerializerSettings = new JsonSerializerSettings();

		    ApiUrl = apiUrl;

			// Ignoring null values when serializing json objects
			JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
		}

		#endregion

		public InfoModel Info()
		{
			var infoModel = new InfoModel();
		    var requestModel = _requestModelHelper.InfoProperties(ApiUrl, "info");

			try
			{
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

				// ** TO DO - Check for error before converting to model. Expand model to include error properties?
				infoModel = JsonConvert.DeserializeObject<InfoModel>(webResult, JsonSerializerSettings);

				if (infoModel.engineVersion != null)
				{
					infoModel.Online = true;
				}
			}
			catch (Exception ex)
			{
				infoModel.HasErrorOccurred = true;
				infoModel.Errors.Add(new Error { message = ex.ToString() });
			}

			return infoModel;
		}

	    public AnalyzeModel Analyze(string host)
	    {
			// overloaded method to provide a default set of options
		    return Analyze(host, Publish.Off, ClearCache.On, FromCache.Off, All.On);
	    }

		public AnalyzeModel Analyze(string host, Publish publish, ClearCache clearCache, FromCache fromCache, All all)
		{
			var analyzeModel = new AnalyzeModel();

			// Checking host is valid before continuing
			if (!_urlHelper.IsValid(host))
			{
				analyzeModel.HasErrorOccurred = true;
				analyzeModel.Errors.Add(new Error {message = "Host does not pass preflight validation. No Api call has been made."});
				return analyzeModel;
			}

			// Building request model
			var requestModel = _requestModelHelper.AnalyzeProperties(ApiUrl, "analyze", host, publish.ToString().ToLower(), clearCache.ToString().ToLower(), 
				fromCache.ToString().ToLower(), all.ToString().ToLower());

			try
			{
				// Making Api request and gathering response
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

				// Trying to bind result to model
				analyzeModel = JsonConvert.DeserializeObject<AnalyzeModel>(webResult, JsonSerializerSettings);
				analyzeModel.Headers.statusCode = _webResponseHelper.GetStatusCode(webResponse);
				analyzeModel.Headers.statusDescription = _webResponseHelper.GetStatusDescription(webResponse);
			}
			catch (Exception ex)
			{
				analyzeModel.HasErrorOccurred = true;
				analyzeModel.Errors.Add(new Error {message = ex.ToString()});
			}

			return analyzeModel;
		}

		public string GetEndpointData(string host, string s)
		{
			return GetEndpointData(host, s, "no");
		}

	    public string GetEndpointData(string host, string s, string fromCache)
	    {
		    throw new NotImplementedException();
	    }

	    public string GetStatusCodes()
	    {
		    throw new NotImplementedException();
	    }
    }
}
