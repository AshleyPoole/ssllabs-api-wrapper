using System.Collections.Generic;

namespace SSLLWrapper.Models.Response.EndpointSubModels
{
	public class Sims
	{
		public List<Result> results { get; set; }

		public Sims()
		{
			results = new List<Result>();
		}
	}
}
