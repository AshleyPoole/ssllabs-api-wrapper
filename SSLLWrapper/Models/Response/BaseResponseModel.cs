namespace SSLLWrapper.Models.Response
{
	public class BaseResponseModel
	{
		public class Headers
		{
			public string statusCode { get; set; }
			public string statusDescription { get; set; }
		}

		public class Wrapper
		{
			public bool ErrorOccurred { get; set; }
			public string ErrorText { get; set; }
		}
	}
}
