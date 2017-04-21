using System.Collections.Generic;
using System.Web.Http;
using AzureTwitter.Models;
using AzureTwitter.Storage.Interfaces.Repositories;

namespace AzureTwitter.ApiService.Controllers
{
	[Route("api/[controller]")]
	[ServiceRequestActionFilter]
	public class TweetsController : ApiController
	{
		private readonly ITweetsRepository _tweetsRepository;

		public TweetsController(ITweetsRepository tweetsRepository)
		{
			_tweetsRepository = tweetsRepository;
		}

		[HttpGet]
		public IEnumerable<TweetModel> Get()
		{
			return _tweetsRepository.Get();
		}

		[HttpGet]
		public TweetModel Get(string id)
		{
			return _tweetsRepository.Get(id);
		}
	}
}
