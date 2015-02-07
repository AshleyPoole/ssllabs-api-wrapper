using System.Collections.Generic;
using SSLLWrapper.Interfaces;
using SSLLWrapper.Models.Response.BaseSubModels;

namespace SSLLWrapper.Models.Response
{
	public class BaseModel : IBaseResponse
	{
		public Header Header { get; set; }
		public bool HasErrorOccurred { get; set; }
		public List<Error> Errors { get; set; }
		public Wrapper Wrapper { get; set; }

		public BaseModel()
		{
			Header = new Header();
			Errors = new List<Error>();
			Wrapper = new Wrapper();
			this.HasErrorOccurred = false;
		}
	}
}
