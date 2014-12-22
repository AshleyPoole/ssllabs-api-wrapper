using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLLWrapper.Models.Response.BaseResponseSubModels
{
	public class Wrapper
	{
		public bool ErrorOccurred { get; set; }
		public string ErrorText { get; set; }
	}
}
