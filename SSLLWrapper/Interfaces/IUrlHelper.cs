using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SSLLWrapper.Interfaces
{
	interface IUrlHelper
	{
		bool IsValid(string url);
	}
}
