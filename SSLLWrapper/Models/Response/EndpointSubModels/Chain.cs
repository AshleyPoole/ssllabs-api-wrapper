using System.Collections.Generic;

namespace SSLLWrapper.Models.Response.EndpointSubModels
{
	public class Chain
	{
		public List<Cert2> certs { get; set; }
		public int issues { get; set; }
	}
}
