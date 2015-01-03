using System.Collections.Generic;
using SSLLWrapper.Models.Response.BaseSubModels;

namespace SSLLWrapper.Models.Response
{
	public class BaseModel
	{
		public Header Header { get; set; }
		public bool HasErrorOccurred { get; set; }
		public List<Error> Errors { get; set; }

		public BaseModel()
		{
			Header = new Header();
			Errors = new List<Error>();
			this.HasErrorOccurred = false;
		}
	}
}
