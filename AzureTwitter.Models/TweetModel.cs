using System;

namespace AzureTwitter.Models
{
	public class TweetModel
	{
		public string Id { get; set; }
		public string Content { get; set; }
		public string User { get; set; }
		public DateTime Created { get; set; }
	}
}
