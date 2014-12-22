using System.Collections.Generic;

namespace SSLLWrapper.Models.Response
{
	public class AnalyzeModel : BaseModel
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
