namespace SSLLWrapper.Models.Response.EndpointSubModels
{
	public class Key
	{
		public int size { get; set; }
		public string alg { get; set; }
		public bool debianFlaw { get; set; }
		public int strength { get; set; }
	}
}
