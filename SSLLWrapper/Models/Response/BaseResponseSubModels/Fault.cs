using System.Collections.Generic;
using SSLLWrapper.Models.Response.ErrorsSubModels;

namespace SSLLWrapper.Models.Response.BaseResponseSubModels
{
	public class Fault
	{
		public bool HasOccurred { get; set; }
		public List<Error> Errors { get; set; }

		public Fault()
		{
			this.HasOccurred = false;
		}
	}
}
