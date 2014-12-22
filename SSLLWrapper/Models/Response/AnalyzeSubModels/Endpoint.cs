using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLLWrapper.Models.Response.AnalyzeSubModels
{
	public class Endpoint
	{
		public string ipAddress { get; set; }
		public string statusMessage { get; set; }
		public string statusDetails { get; set; }
		public string statusDetailsMessage { get; set; }
		public int progress { get; set; }
		public int eta { get; set; }
		public int delegation { get; set; }

		// Two groups of poperities can be returned. Just seperating them out for my own reference.
		public int duration { get; set; }
		public string grade { get; set; }
		public bool hasWarnings { get; set; }
		public bool isExceptional { get; set; }
	}
}
