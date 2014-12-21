using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSLLWrapper.Models;

namespace SSLLWrapper.Interfaces
{
	interface IRequestModelHelper
	{
		RequestModel InfoRequestModel(string apiBaseUrl, string action);
		RequestModel AnalyzeRequestModel(string apiBaseUrl, string action, string host, string publish, string clearCache, string fromCache, string all);
	}
}
