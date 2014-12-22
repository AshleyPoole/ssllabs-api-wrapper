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
		public string ApiUrl { get; set; }

	    public ApiService()
		{
			_api = new Api();
			_webResponseHelper = new HttpWebResponseHelper();
			_requestModelHelper = new RequestModelHelper();
			ApiUrl = "https://api.dev.ssllabs.com/api/fa78d5a4/";
		}

	    public InfoModel Info()
		{
			var infoModel = new InfoModel();
		    var requestModel = _requestModelHelper.InfoProperties(ApiUrl, "info");

			try
			{
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

				// ** TO DO - Check for error before converting to model. Expand model to include error properties?
				infoModel = (InfoModel) JsonConvert.DeserializeObject(webResult);

				if (infoModel.engineVersion != null)
				{
					infoModel.Online = true;
				}
			}
			catch (Exception ex)
			{
				infoModel.Wrapper.ErrorOccurred = true;
				infoModel.Wrapper.ErrorText = ex.ToString();
			}

			return infoModel;
		}

		public AnalyzeModel Analyze(string host, string publish, string clearCache, string fromCache, string all)
		{
			// ** TO DO - Validate comsumers input. Helper.
			var analyzeModel = new AnalyzeModel();
			var requestModel = _requestModelHelper.AnalyzeProperties(ApiUrl, "analyze", host, publish, clearCache, fromCache, all);

			try
			{
				var webResponse = _api.MakeGetRequest(requestModel);
				var webResult = _webResponseHelper.GetResponsePayload(webResponse);

				// ** TO DO - Check for error before converting to model. Expand model to include error properties?
				analyzeModel = (AnalyzeModel)JsonConvert.DeserializeObject(webResult);
				
			}
			catch (Exception ex)
			{
				analyzeModel.Wrapper.ErrorOccurred = true;
				analyzeModel.Wrapper.ErrorText = ex.ToString();
			}

			return analyzeModel;
		}
    }
}
