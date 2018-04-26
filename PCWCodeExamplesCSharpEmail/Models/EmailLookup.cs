using Newtonsoft.Json;

namespace PCWCodeExamplesCSharpEmail.Models
{
	/// <summary>
	/// For storing the results of an email lookup
	/// </summary>
	[JsonObject]
	public class EmailLookup
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool valid { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int score { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int processtime { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string alternative { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string warning { get; set; }
	}

	/// <summary>
	/// A wrapper class for returning the results of an email lookup
	/// </summary>
	[JsonObject]
	public class EmailReturn
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string error_message { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public EmailLookup email { get; set; }
	}
}