using System;
using Newtonsoft.Json;
using SSLLWrapper.Helpers;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models.Response;

namespace SSLLWrapper
{
    public class ApiService
    {
	    readonly IApi _api;
	    private readonly HttpWebResponseHelper _webResponseHelper;
	    private readonly RequestModelHelper _requestModelHelper;

	    public ApiService()
		{
			_api = new Api();
			_webResponseHelper = new HttpWebResponseHelper();
			_requestModelHelper = new RequestModelHelper();
			//_apiBaseUrl = WebConfigurationManager.AppSettings["SSLLabsApi"];
		}

	    public InfoModel Info()
		{
			InfoModel infoResponse;

		    var requestModel = _requestModelHelper.InfoProperties(_apiBaseUrl, "info");

			try
			{
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

				// ** TO DO - Check for error before converting to model. Expand model to include error properties?
				infoResponse = (InfoModel) JsonConvert.DeserializeObject(webResult);

				if (infoResponse.engineVersion != null)
				{
					infoResponse.Online = true;
				}
			}
			catch (Exception)
			{
				// failure getting API status
				// ** TO DO - Add logging of exception
				throw;
			}

			return infoResponse;
		}

		public AnalyzeModel Analyze(string host, string publish, string clearCache, string fromCache, string all)
		{
			// ** TO DO - Validate comsumers input. Helper.
			AnalyzeModel analyzeModel;

			var requestModel = _requestModelHelper.AnalyzeProperties(_apiBaseUrl, "analyze", host, publish, clearCache, fromCache, all);

			try
			{
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

				// ** TO DO - Check for error before converting to model. Expand model to include error properties?
				analyzeModel = (AnalyzeModel)JsonConvert.DeserializeObject(webResult);
			}
			catch (Exception)
			{
				// failure getting API status
				// ** TO DO - Add logging of exception and set status flag maybe?
				throw;
			}

			return analyzeModel;
		}
    }
}
