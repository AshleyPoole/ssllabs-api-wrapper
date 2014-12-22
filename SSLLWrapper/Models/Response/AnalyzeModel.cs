using System.Collections.Generic;

namespace SSLLWrapper.Models.Response
{
	public class AnalyzeModel : BaseResponseModel
	{
		public class Endpoint
		{
			public string ipAddress { get; set; }
			public string statusMessage { get; set; }
			public string statusDetails { get; set; }
			public string statusDetailsMessage { get; set; }
			public int progress { get; set; }
			public int eta { get; set; }
			public int delegation { get; set; }

			// Two groups of poperities can be returned. Just seperating them out for my own reference.
			public int duration { get; set; }
			public string grade { get; set; }
			public bool hasWarnings { get; set; }
			public bool isExceptional { get; set; }
		}

		public class RootObject
		{
			public string host { get; set; }
			public int port { get; set; }
			public string protocol { get; set; }
			public bool isPublic { get; set; }
			public string status { get; set; }
			public long startTime { get; set; }
			public string engineVersion { get; set; }
			public string criteriaVersion { get; set; }
			public List<Endpoint> endpoints { get; set; }
		}
	}
}
