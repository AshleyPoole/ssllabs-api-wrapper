namespace SSLLWrapper.Models.Response
{
	public class Info : BaseModel
	{
		public string engineVersion { get; set; }
		public string criteriaVersion { get; set; }
		public int clientMaxAssessments { get; set; }
		public string notice { get; set; }
		public bool Online { get; set; }

		public Info()
		{
			// Assigning default online status
			this.Online = false;
		}
	}
}
