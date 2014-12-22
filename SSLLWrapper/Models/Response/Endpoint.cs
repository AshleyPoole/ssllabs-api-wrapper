using SSLLWrapper.Models.Response.EndpointSubModels;

namespace SSLLWrapper.Models.Response
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

		public Details Details { get; set; }

		public Endpoint()
		{
			Details = new Details();
		}

	}
}
