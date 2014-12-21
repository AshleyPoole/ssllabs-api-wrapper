using System;
using System.Management.Instrumentation;
using Newtonsoft.Json;
using SSLLWrapper.Helpers;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;

namespace SSLLWrapper
{
    public class ApiService
    {
	    readonly IApi _api;
	    private readonly HttpWebResponseHelper _webResponseHelper;

		public ApiService()
		{
			_api = new Api();
			_webResponseHelper = new HttpWebResponseHelper();
			//_apiBaseUrl = WebConfigurationManager.AppSettings["SSLLabsApi"];
		}

	    public InfoModel Info()
		{
			InfoModel infoResponse;
			var requestModel = new RequestModel {ApiBaseUrl = _apiBaseUrl, Action = "info"};

			try
			{
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

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

		public AnalyzeModel Analyze(string host, bool publish, bool clearCache, bool fromCache, string all)
		{
			var analyzeModel = new AnalyzeModel();
			var requestModel = new RequestModel() {ApiBaseUrl = _apiBaseUrl, Action = "analyze"};

			// ** TO DO - Move this to helper
			requestModel.Paramaters.Add("host",host);
			requestModel.Paramaters.Add("publish", "off"); // ** TO DO - Allow comsumer to set this
			requestModel.Paramaters.Add("clearCache", "on"); // ** TO DO - Allow comsumer to set this
			requestModel.Paramaters.Add("fromCache", "off"); // ** TO DO - Allow comsumer to set this
			requestModel.Paramaters.Add("all", "on"); // ** TO DO - Allow comsumer to set this

			try
			{
				var apiResult = _api.MakeGetRequest(requestModel);

				analyzeModel = (AnalyzeModel)JsonConvert.DeserializeObject(apiResult);
			}
			catch (Exception)
			{
				// failure getting API status
				// ** TO DO - Add logging of exception
				throw;
			}

			return analyzeModel;
		}
    }
}
