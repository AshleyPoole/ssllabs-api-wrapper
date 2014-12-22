using SSLLWrapper.Models.Response.BaseResponseSubModels;

namespace SSLLWrapper.Models.Response
{
	public class BaseModel
	{
		public Headers Headers { get; set; }
		public Wrapper Wrapper { get; set; }

		public BaseModel()
		{
			Headers = new Headers();
			Wrapper = new Wrapper();
		}
	}
}
