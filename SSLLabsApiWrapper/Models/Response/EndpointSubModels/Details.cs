using System.Collections.Generic;

namespace SSLLabsApiWrapper.Models.Response.EndpointSubModels
{
	public class Details
	{
		public long hostStartTime { get; set; }
		public Key key { get; set; }
		public Cert cert { get; set; }
		public Chain chain { get; set; }
		public List<Protocol> protocols { get; set; }
		public Suites suites { get; set; }
		public string serverSignature { get; set; }
		public bool prefixDelegation { get; set; }
		public bool nonPrefixDelegation { get; set; }
		public bool vulnBeast { get; set; }
		public int renegSupport { get; set; }
		public int sessionResumption { get; set; }
		public int compressionMethods { get; set; }
		public bool supportsNpn { get; set; }
		public int sessionTickets { get; set; }
		public bool ocspStapling { get; set; }
		public bool sniRequired { get; set; }
		public int httpStatusCode { get; set; }
		public bool supportsRc4 { get; set; }
		public int forwardSecrecy { get; set; }
		public bool rc4WithModern { get; set; }
		public Sims sims { get; set; }
		public bool heartbleed { get; set; }
		public bool heartbeat { get; set; }
		public int openSslCcs { get; set; }
		public int poodleTls { get; set; }

		public Details()
		{
			key = new Key();
			cert = new Cert();
			chain = new Chain();
			protocols = new List<Protocol>();
			suites = new Suites();
			sims = new Sims();
		}
	}
}
