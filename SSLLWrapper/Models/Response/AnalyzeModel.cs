using System.Collections.Generic;
using SSLLWrapper.Models.Response.AnalyzeSubModels;

namespace SSLLWrapper.Models.Response
{
	public class AnalyzeModel : BaseModel
	{
		public Endpoint Endpoint { get; set; }
		public RootObject RootObject { get; set; }

		public AnalyzeModel()
		{
			Endpoint = new Endpoint();
			RootObject = new RootObject();
		}
	}
}
