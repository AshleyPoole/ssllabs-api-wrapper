using System.Net;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;

namespace SSLLWrapper
{
	public class Api : IApi
	{
		public HttpWebResponse MakeGetRequest(RequestModel requestModel)
		{
			var url = requestModel.ApiBaseUrl + "/" + requestModel.Action; // ** TO DO - Add query string

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";

			// Make request and return response
			return (HttpWebResponse)request.GetResponse(); ;
		}
	}
}
