using System.Collections.Generic;
using SSLLWrapper.Models.Response.BaseResponseSubModels;

namespace SSLLWrapper.Models.Response
{
	public class BaseModel
	{
		public Header Headers { get; set; }
		public bool HasErrorOccurred { get; set; }
		public List<Error> Errors { get; set; }

		public BaseModel()
		{
			Headers = new Header();
			Errors = new List<Error>();
			this.HasErrorOccurred = false;
		}
	}
}
