using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLLabsApiWrapper.Models
{
	public class RequestModel
	{
		public string ApiBaseUrl;
		public string Action;
		public Dictionary<string, string> Parameters;
		public string RequestType;

		public RequestModel()
		{
			Parameters = new Dictionary<string, string>();
		}
	}
}
