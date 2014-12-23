using System.Net;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models;

namespace SSLLWrapper
{
	public class Api : IApi
	{
		public HttpWebResponse MakeGetRequest(RequestModel requestModel)
		{
			var url = requestModel.ApiBaseUrl + "/" + requestModel.Action;

			// ** TO DO - Refactor this
			if (requestModel.Parameters.Count >= 1)
			{
				url = url + "?";
				var iteration = 0;

				foreach(var parameter in requestModel.Parameters)
				{
					iteration++;
					url = url + parameter.Key + "=" + parameter.Value;

					if (iteration != requestModel.Parameters.Count)
						url = url + "&";
				}
			}

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";

			// Make request and return response
			return (HttpWebResponse)request.GetResponse(); ;
		}
	}
}
