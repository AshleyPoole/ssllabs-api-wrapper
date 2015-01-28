using System.Collections.Generic;
using SSLLWrapper.Models.Response.BaseSubModels;

namespace SSLLWrapper.Interfaces
{
	public interface IBaseResponse
	{
		Header Header { get; set; }
		bool HasErrorOccurred { get; set; }
		List<Error> Errors { get; set; }
	}
}