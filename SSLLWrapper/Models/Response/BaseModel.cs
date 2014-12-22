using SSLLWrapper.Models.Response.BaseResponseSubModels;

namespace SSLLWrapper.Models.Response
{
	public class BaseModel
	{
		public Header Headers { get; set; }
		public Fault Fault { get; set; }

		public BaseModel()
		{
			Headers = new Header();
			Fault = new Fault();
		}
	}
}
