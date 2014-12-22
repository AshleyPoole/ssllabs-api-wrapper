using System;
using SSLLWrapper.Interfaces;

namespace SSLLWrapper.Helpers
{
	class UrlHelper : IUrlHelper
	{
		public bool IsValid(string url)
		{
			var valid = true;

			Uri uri = null;
			if (!Uri.TryCreate(url, UriKind.Absolute, out uri) || null == uri)
			{
				valid = false;
			}

			return valid;
		}
	}
}