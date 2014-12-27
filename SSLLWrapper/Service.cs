using System;
using SSLLWrapper.Helpers;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models.Response;
using SSLLWrapper.Models.Response.BaseSubModels;

namespace SSLLWrapper
{
    public class Service
    {
	    #region construction

	    private readonly IApi _api;
	    private readonly IHttpWebResponseHelper _webResponseHelper;
	    private readonly IRequestModelHelper _requestModelHelper;
	    private readonly IResponsePopulationHelper _responsePopulationHelper;
		private readonly IUrlHelper _urlHelper;
	    private string ApiUrl { get; set; }

	    public enum Publish
	    {
		    On,
		    Off
	    }

	    public enum ClearCache
	    {
		    On,
		    Off,
			Ignore
	    }

	    public enum FromCache
	    {
		    On,
		    Off,
			Ignore
	    }

	    public enum All
	    {
		    On,
		    Done
	    }

	    public Service(string apiUrl)
		{
			_api = new Api();
			_webResponseHelper = new HttpWebResponseHelper();
			_requestModelHelper = new RequestModelHelper();
		    _urlHelper = new UrlHelper();
			_responsePopulationHelper = new ResponsePopulationHelper();

		    ApiUrl = apiUrl;
		}

		#endregion

		public Info Info()
		{
			var infoModel = new Info();

			// Building new request model
		    var requestModel = _requestModelHelper.InfoProperties(ApiUrl, "info");

			try
			{
				// Making Api request and gathering response
				var webResponse = _api.MakeGetRequest(requestModel);

				// Binding result to model
				infoModel = _responsePopulationHelper.InfoModel(webResponse, infoModel);

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

			// Checking if errors have occoured either from ethier api or wrapper
			if (infoModel.Errors.Count != 0 && !infoModel.HasErrorOccurred) { infoModel.HasErrorOccurred = true;}

			return infoModel;
		}

	    public Analyze Analyze(string host)
	    {
			// overloaded method to provide a default set of options
		    return Analyze(host, Publish.Off, ClearCache.On, FromCache.Ignore, All.On);
	    }

		public Analyze Analyze(string host, Publish publish, ClearCache clearCache, FromCache fromCache, All all)
		{
			var analyzeModel = new Analyze();

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

				// Binding result to model
				analyzeModel = _responsePopulationHelper.AnalyzeModel(webResponse, analyzeModel);
			}
			catch (Exception ex)
			{
				analyzeModel.HasErrorOccurred = true;
				analyzeModel.Errors.Add(new Error {message = ex.ToString()});
			}

			// Checking if errors have occoured either from ethier api or wrapper
			if (analyzeModel.Errors.Count != 0 && !analyzeModel.HasErrorOccurred) { analyzeModel.HasErrorOccurred = true; }

			return analyzeModel;
		}

		public Endpoint GetEndpointData(string host, string s)
		{
			return GetEndpointData(host, s, FromCache.Off);
		}

		public Endpoint GetEndpointData(string host, string s, FromCache fromCache)
	    {
		    var endpointModel = new Endpoint();

			// Checking host is valid before continuing
			if (!_urlHelper.IsValid(host))
			{
				endpointModel.HasErrorOccurred = true;
				endpointModel.Errors.Add(new Error { message = "Host does not pass preflight validation. No Api call has been made." });
				return endpointModel;
			}

			// Building request model
			var requestModel = _requestModelHelper.GetEndpointDataProperties(ApiUrl, "getEndpointData", host, s,
				fromCache.ToString());

			try
			{
				// Making Api request and gathering response
				var webResponse = _api.MakeGetRequest(requestModel);

				// Binding result to model
				endpointModel = _responsePopulationHelper.EndpointModel(webResponse, endpointModel);
			}
			catch (Exception ex)
			{
				endpointModel.HasErrorOccurred = true;
				endpointModel.Errors.Add(new Error { message = ex.ToString() });
			}

			// Checking if errors have occoured either from ethier api or wrapper
			if (endpointModel.Errors.Count != 0 && !endpointModel.HasErrorOccurred) { endpointModel.HasErrorOccurred = true; }

		    return endpointModel;
	    }

	    public StatusDetails GetStatusCodes()
	    {
		    var statusDetailsModel = new StatusDetails();

			// Building request model
		    var requestModel = _requestModelHelper.GetStatusCodeProperties(ApiUrl, "getStatusCodes");

		    try
		    {
				// Making Api request and gathering response
			    var webResponse = _api.MakeGetRequest(requestModel);

				// Binding result to model
			    statusDetailsModel = _responsePopulationHelper.StatusDetailsModel(webResponse, statusDetailsModel);
		    }
		    catch (Exception ex)
		    {
				statusDetailsModel.HasErrorOccurred = true;
				statusDetailsModel.Errors.Add(new Error { message = ex.ToString() });
		    }

		    return statusDetailsModel;
	    }
    }
}
